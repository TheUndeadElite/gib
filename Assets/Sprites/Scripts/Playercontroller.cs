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

    //Olika "states" så jag kan växla mellan att gå och hoppa, och dashing

    enum PlayerState
    {
        Walking,
        Dashing
    }

    private PlayerState currentState = PlayerState.Walking;
    //Vector3 StartScale = Vector3.one;
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() 
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
            myRigidbody.velocity = new Vector2(speed*horizontalInput, myRigidbody.velocity.y);
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

        if (horizontalInput > 0)
        {

        }

    }
    //
    
}





