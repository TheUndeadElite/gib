using UnityEditor.Rendering;
using UnityEngine;

public class EnemyPostrolling : MonoBehaviour
{
    public float speed = 5f;
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    private float stopTime;
    private float stopTimer;
    private float nextRandomX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;

        ChooseNextRandomX();
    }

    void Update()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        Vector2 direction = (currentPoint.position - transform.position).normalized;

        // Check if it's time to stop
        if (stopTimer > 0)
        {
            stopTimer -= Time.deltaTime;
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = direction * speed;

            // Check if arrived at the current point
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {
               

                // Choose a new random X position
                ChooseNextRandomX();

                // Switch to the other point with the new X position
                if (currentPoint == pointA.transform)
                {
                    currentPoint = new GameObject().transform; // Dummy GameObject for a new position
                }
                else
                {
                    currentPoint = new GameObject().transform; // Dummy GameObject for a new position
                    currentPoint.position = new Vector2(nextRandomX, transform.position.y);
                }
                // Start the stop timer for 2 seconds
                stopTimer = 2f;
            }
        }
    }

    

    void ChooseNextRandomX()
    {
        nextRandomX = Random.Range(pointA.transform.position.x, pointB.transform.position.x);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
