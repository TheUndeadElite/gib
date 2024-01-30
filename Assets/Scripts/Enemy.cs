using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;        // Hastighet f�r fienden
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
        // R�relse i den aktuella riktningen (endast h�ger eller v�nster)
        rb.velocity = new Vector2(speed, rb.velocity.y);

        // Kolla om det �r dags att stanna
        if (Time.time - startTime >= stopTime)
        {
            // Stanna fienden
            rb.velocity = Vector2.zero;

            // V�lj en ny riktning och �terst�ll starttiden
            speed = -speed;
            startTime = Time.time + Random.Range(1f, 5f);  // V�nta mellan 1 och 5 sekunder innan n�sta riktning v�ljs
        }
    }
}
