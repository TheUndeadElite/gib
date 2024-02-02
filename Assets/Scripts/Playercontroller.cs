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

    float speed = 10;
    //Vector3 StartScale = Vector3.one;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //StartScale = transform.localScale;
    }
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        if (horizontalInput != 0)
        {
            rb.AddForce (new Vector2(horizontalInput * speed, 0f));
        }
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





