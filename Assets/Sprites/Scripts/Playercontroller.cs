using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Playercontroller : MonoBehaviour
{
    Rigidbody2D rb;

    float horizontalInput;
    float verticalInput;

    float speed = 7;
    //Vector3 StartScale = Vector3.one;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate() 
    {
        //Walking

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput == 0)
        {      //La till add velocity istället för add force
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
        if (horizontalInput != 0)
        {
            rb.velocity = new Vector2(speed*horizontalInput, rb.velocity.y);
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
    }
}





