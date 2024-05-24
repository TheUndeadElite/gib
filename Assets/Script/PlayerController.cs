using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 8.0f;
    [SerializeField] float sprintSpeed = 12.0f;
    [SerializeField] bool isDashing = false;
    private bool canDash;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private bool canAttack;
    [SerializeField] bool isAttacking = false;

    float speedAtStart;
    Animator characterAnimator;
    Rigidbody2D myRigidbody;
    float horizontalInput;
    float verticalInput;

    [SerializeField] private float exclamationMarkYOffset = 1.0f;
    private GameObject exclamationMarkInstance;

    [SerializeField] GameManager s_gameManager;

    // Lägg till publika variabler för att kontrollera antalet fiender
    

    private void Awake()
    {
        s_gameManager = FindObjectOfType<GameManager>();
        myRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponentInChildren<Animator>();

        if (characterAnimator == null)
        {
            Debug.LogError("Character Animator is not assigned correctly!");
        }

        speedAtStart = speed;
        canDash = true;
        canAttack = true;
    }

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    IEnumerator AttackHitboxTrigger()
    {
        yield return new WaitForSeconds(0.553f);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<DamageTaker>().TakeDamage(1);
        }
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;

        yield return null;
    }

    IEnumerator AttackRoutine()
    {
        yield return null;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Attack()
    {
        isAttacking = true;
        characterAnimator.SetTrigger("isAttacking");
        StartCoroutine(AttackHitboxTrigger());
    }

    private void Update()
    {

        if (s_gameManager.gameIsPaused) return;

        if (Input.GetKey(KeyCode.Mouse0) && !isAttacking)
        {
            Attack();
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }

        if (isDashing)
        {
            if (gameObject.transform.localScale.x == 1)
            {
                myRigidbody.velocity = transform.right * 20;
            }
            else
            {
                myRigidbody.velocity = -transform.right * 20;
            }
        }
        else
        {
            if (horizontalInput == 0)
            {
                myRigidbody.velocity = new Vector2(0.0f, myRigidbody.velocity.y);
                characterAnimator.SetBool("isWalking", false);
            }
            if (horizontalInput != 0)
            {
                myRigidbody.velocity = new Vector2(speed * horizontalInput, myRigidbody.velocity.y);
                characterAnimator.SetBool("isWalking", true);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Sprint();
        }
        else
        {
            speed = speedAtStart;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isDashing && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        yield return new WaitForSeconds(0.15f);
        isDashing = false;
        yield return new WaitForSeconds(2);
        canDash = true;
    }

    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (exclamationMarkInstance != null)
            {
                Destroy(exclamationMarkInstance);
                exclamationMarkInstance = null;
            }
        }
    }

    void Sprint()
    {
        speed = sprintSpeed;
    }

    public void Die()
    {
        Debug.Log("Player has died!");
        SceneManager.LoadScene("DeathScreen");
    }
}
