using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Playercontroller : MonoBehaviour
{
   
  
    public float speed = 5;
    private Rigidbody rb;
    //Vector3 StartScale = Vector3.one;
    private void Start()
    {
        //StartScale = transform.localScale;
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb = GetComponent<Rigidbody>();

        Vector2 Movement = new Vector2(horizontalInput, 0f);

        if (horizontalInput < 0)
        {
            FlipCharacter(-1f);
        }
        else if (horizontalInput < 0)
        {
            FlipCharacter(1f);
        }
    }
       void FlipCharacter(float direction)
       {
            Vector3 scale = transform.localScale;
            scale.x = direction;
            transform.localScale = scale;
       }
    }