using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 8.0f;
    [SerializeField] float sprintSpeed = 12.0f;

    float dashForce = 500f;
    bool isDashing = false;

    float speedAtStart;


    [SerializeField] Animator characterAnimator;
    

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

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        speedAtStart = speed;
    }
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
         //Debug.Log("rotating");

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

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

        if (horizontalInput > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        if (horizontalInput < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
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

        //if (Input.GetKey(KeyCode.Q) && !IsDashing)
        //{
        //    StartCorountine(Dash());
        //}
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
