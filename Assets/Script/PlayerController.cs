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
    public int numberOfType1ToSpawn = 5;
    public int numberOfType2ToSpawn = 5;

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
            Debug.Log("Hej dennis");
            DamageTaker damageTaker = enemy.GetComponent<DamageTaker>();
            if (damageTaker != null)
            {
                damageTaker.TakeDamage(1);
            }
        }
        isAttacking = false;
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
    public void Die()
    {
        Debug.Log("Player has died!");
        SceneManager.LoadScene("DeathScreen");
    }

    private void Update()
    {
        if (s_gameManager != null && s_gameManager.gameIsPaused) return;

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
            myRigidbody.velocity = new Vector2(transform.localScale.x * 20, myRigidbody.velocity.y);
        }
        else
        {
            myRigidbody.velocity = new Vector2(speed * horizontalInput, myRigidbody.velocity.y);
            characterAnimator.SetBool("isWalking", horizontalInput != 0);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemySpawnTrigger"))
        {
            EnemySpawner spawner = other.GetComponent<EnemySpawner>();
            if (spawner != null)
            {
                spawner.ActivateSpawner(numberOfType1ToSpawn, numberOfType2ToSpawn);
                Debug.Log("Player triggered spawner: " + spawner.name);
            }
        }
        else if (other.CompareTag("Interactable"))
        {
            if (exclamationMarkInstance == null)
            {
                GameObject exclamationMarkPrefab = Resources.Load<GameObject>("Utroptstecken");
                if (exclamationMarkPrefab != null)
                {
                    Vector3 spawnPosition = other.transform.position + new Vector3(0, exclamationMarkYOffset, 0);
                    exclamationMarkInstance = Instantiate(exclamationMarkPrefab, spawnPosition, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("Failed to load exclamation mark prefab.");
                }
            }
        }
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
}
