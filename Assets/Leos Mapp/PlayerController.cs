using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 8.0f;
    [SerializeField] float sprintSpeed = 12.0f;

    [SerializeField] private DiglogueUI diglogueUI;

    float speedAtStart;

    public DiglogueUI DiglogueUI => diglogueUI;

    public IInteractable interactabel { get; set; }

    Rigidbody2D myRigidbody;
    float horizontalInput;
    float verticalInput;
    float Sprintspeed = 10;

    //float sprintDuration = 5.0f;
    //private float sprintTimer;
    //private bool isSprinting;

    enum PlayerState
    {
        Walking,
        Dashing
    }
    //public void SetVerticalInput(float aValue)
    //{ verticalInput = aValue; }
    private PlayerState currentState = PlayerState.Walking;

    [SerializeField] private float exclamationMarkYOffset = 1.0f; // Serialized field to adjust the Y-axis offset

    private GameObject exclamationMarkInstance; // Reference to the instantiated exclamation mark

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        speedAtStart = speed;
    }

    void Update()
    {
        // Debug.Log(isSprinting);

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput == 0)
        {
            myRigidbody.velocity = new Vector2(0.0f, myRigidbody.velocity.y);
        }
        if (horizontalInput != 0)
        {
            myRigidbody.velocity = new Vector2(speed * horizontalInput, myRigidbody.velocity.y);
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
            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    isSprinting = true;
            //}
            
            //else{
            //    isSprinting = false;
            //}

            //if (isSprinting)
            //{
            //    Sprint();
            //}

            //void Sprint()
            //{
            //    sprintDuration -= Time.deltaTime;
            //    if (sprintDuration <= 0f)
            //        StopSprint();

            //    float speed = isSprinting ? Sprintspeed : 5.0f;

            //    Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;
            //    transform.Translate(movement * speed * Time.deltaTime);
            //}

            //void StopSprint()
            //{
            //    isSprinting = false;
            //    sprintDuration = 5.0f;
            //}

            //Sprint lasts 5 seconds

        


        //Sprint lasts 5 seconds
        //Sprint();
        //StopSprint();

       // if (diglogueUI.IsOpen) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            interactabel.Interact(this);
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
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            Debug.Log("INTERAVNAWFJE");
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
