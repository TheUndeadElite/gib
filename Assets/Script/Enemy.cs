using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveDirection;

    void Start()
    {
        // Set a random initial movement direction
        moveDirection = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
        // Move the enemy
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}
