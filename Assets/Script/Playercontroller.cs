using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    [SerializeField] private DiglogueUI diglogueUI;

    public DiglogueUI DiglogueUI => diglogueUI;

    public IInteractable interactabel { get; set; }

    Rigidbody2D myRigidbody;
    float horizontalInput;
    float verticalInput;



    //float Sprintspeed = 10;

    //float sprintDuration = 5.0f;
    //private float sprintTimer;
    //private bool isSprinting;

    enum PlayerState
    {
        Walking,
        Dashing
    }

    private PlayerState currentState = PlayerState.Walking;

    [SerializeField] private float exclamationMarkYOffset = 1.0f; // Serialized field to adjust the Y-axis offset

    private GameObject exclamationMarkInstance; // Reference to the instantiated exclamation mark

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log(isSprinting);

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
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isSprinting = true;
            }
            
            //else{
            //    isSprinting = false;
            //}

            if (isSprinting)
            {
                Sprint();
            }



        //Sprint lasts 5 seconds
        //Sprint();
        //StopSprint();
    
        
    }

    //void Sprint()
    //{
    //    sprintDuration -= Time.deltaTime;
    //    if (sprintDuration <= 0f)
    //    {
    //        StopSprint();
    //    }

    //    float speed = isSprinting ? Sprintspeed : 5.0f;

    //    Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;
    //    transform.Translate(movement * speed * Time.deltaTime);
    //}

    //void StopSprint()
    //{
    //    isSprinting = false;
    //    sprintDuration = 5.0f;
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
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
    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.E))
        {
          interactabel.Interact(this);    
        } 
    }
}
