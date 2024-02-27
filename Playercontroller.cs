using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Playercontroller : MonoBehaviour
{
    Rigidbody2D myRigidbody;

    float horizontalInput;
    float verticalInput;


    float speed = 7;

    float sprintDuration = 5.0f;
    float sprintSpeed = 7;
    private float sprintTimer;
    private float sprintMax = 5.0f;
    private bool isSprinting;

    //Olika "states" så jag kan växla mellan att gå och hoppa, och dashing

    //Vector3 StartScale = Vector3.one;
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        isSprinting = false;
    }

    void FixedUpdate()
    {

        {


            //Walking
            // WASD to move
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            if (horizontalInput == 0)
            {      //La till add velocity ist�llet f�r add force
                myRigidbody.velocity = new Vector2(0.0f, myRigidbody.velocity.y);
            }
            if (horizontalInput != 0)
            {
                myRigidbody.velocity = new Vector2(speed * horizontalInput, myRigidbody.velocity.y);
            }
            //Flip sprite
            if (horizontalInput > 0)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }

            if (horizontalInput < 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }

            //Sprint
            {
                if (Input.GetKeyDown(KeyCode.LeftShift)&& !isSprinting)
                {
                    isSprinting = true;
                }
                if (isSprinting)
                {
                    Sprint();
                }
                
                void Sprint()
                {
                    sprintDuration -= Time.deltaTime;
                    if (sprintDuration <= 0f)
                        StopSprint();

                    if (Input.GetKey(KeyCode.LeftShift) && isSprinting)
                    {
                        speed = speed * 2;
                    }
                    else
                    {
                        speed = 7;
                    }

                }

                void StopSprint()
                {
                    isSprinting = false;
                    sprintDuration = 0;
                }

                //Sprint lasts 5 seconds



            }

        }


    }
}





