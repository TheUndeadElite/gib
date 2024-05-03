using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed;
    public bool chase = false;
    public Transform startingPoint;
    private GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Knight");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
            return;
        if (chase == null)
            Chase();
        else
        ReturnStartPoint();
        Flip();
    }
    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime); 
    }
    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }
    
    private void Flip()
    {
        if (transform.position.x > Player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
