using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Playercontroller : MonoBehaviour
{


    public float speed = 5;
    //Vector3 StartScale = Vector3.one;
    private void Start()
    {
        //StartScale = transform.localScale;
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 Movement = new Vector2(horizontalInput, 0f);

        transform.Translate(Movement * speed * Time.deltaTime);
    }
}





