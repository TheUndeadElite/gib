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

        Flip();
    }
    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime); 
    }
    private void 
    private void Flip()
    {
        if (transform.position.x > Player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
