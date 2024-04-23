using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

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
    float Sprintspeed = 10;

    //float sprintDuration = 5.0f;
    //private float sprintTimer;
    //private bool isSprinting;


    //public void SetVerticalInput(float aValue)
    //{ verticalInput = aValue; }

    [SerializeField] private float exclamationMarkYOffset = 1.0f; // Serialized field to adjust the Y-axis offset

    private GameObject exclamationMarkInstance; // Reference to the instantiated exclamation mark

    [SerializeField] Gamemanager s_gameManager;
     
    private void Awake()
    {
        s_gameManager = FindAnyObjectByType<Gamemanager>();

        myRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponentInChildren<Animator>();

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
        //Skada
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<DamageTaker>().TakeDamage(1);
        }
        yield return null;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Attack();
        }

        void Attack()
        {
            //Animation
            characterAnimator.SetTrigger("isAttacking");
            StartCoroutine(AttackHitboxTrigger());
            //Hitta enemies som finns i rangen
        }

        void OnDrawGizmos()
        {
            if (attackPoint = null)
            {
                return;
            }
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        }



        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        if(isDashing)
        {
            if (gameObject.transform.localScale.x == 1)
            {
                myRigidbody.velocity = transform.right * 20;
            }   else
            {
                myRigidbody.velocity = -transform.right * 20;
            }
            
        }   else
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

        

        

        if (!is_gameManager.gameIsPaused)
        {
            if (horizontalInput > 0)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }

            if (horizontalInput < 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
      
        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Sprint();
        }
        else
        {
            speed = speedAtStart;
        }
        
        //dashing

        if ((Input.GetKeyDown(KeyCode.LeftControl) && !isDashing && canDash))
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

    IEnumerator Attack()
    {
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            Debug.Log("sign");
            // Check if an exclamation mark instance doesn't already exist
            if (exclamationMarkInstance == null)
            {
                // Attempt to load the prefab from the Resources folder
                GameObject exclamationMarkPrefab = Resources.Load<GameObject>("Utroptstecken");
                if (exclamationMarkPrefab != null)
                {
                    Vector3 spawnPosition = other.transform.position + new Vector3(0, exclamationMarkYOffset, 0); // Use the serialized Y-axis offset
                    exclamationMarkInstance = Instantiate(exclamationMarkPrefab, spawnPosition, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("Failed to load exclamation mark prefab.");
                }
            }
        }
    }
    void Sprint()
    {
        speed = sprintSpeed;
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            // Destroy the exclamation mark instance if it exists
            if (exclamationMarkInstance != null)
            {
                Destroy(exclamationMarkInstance);
                exclamationMarkInstance = null; // Reset the reference
            }
        }
    }

}
