using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;        // Hastighet för fienden
    public float stopTime = 5f;     // Tid att stanna i sekunder

    private Rigidbody2D rb;
    private float startTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    void Update()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        // Rörelse i den aktuella riktningen (endast höger eller vänster)
        rb.velocity = new Vector2(speed, rb.velocity.y);

        // Kolla om det är dags att stanna
        if (Time.time - startTime >= stopTime)
        {
            // Stanna fienden
            rb.velocity = Vector2.zero;

            // Välj en ny riktning och återställ starttiden
            speed = -speed;
            startTime = Time.time + Random.Range(1f, 5f);  // Vänta mellan 1 och 5 sekunder innan nästa riktning väljs
        }
    }
}
