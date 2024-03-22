using UnityEngine;

public class ShadowBall : MonoBehaviour
{
    public float speed = 5.0f;
    public int damageAmount = 10;
    public float destroyAfterTime = 4.0f; // Tid innan Shadow Ball förstörs om den inte träffar något

    private float timer;

    void Start()
    {
        // Starta timern när Shadow Ball skapas
        timer = 0f;
    }

    void Update()
    {
        // Rörelse i framåtriktningen
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Uppdatera timern
        timer += Time.deltaTime;

        // Förstör Shadow Ball om tiden har överskridits
        if (timer >= destroyAfterTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Om Shadow Ball träffar ett objekt med HealthController, applicera skada
        HealthController healthController = other.GetComponent<HealthController>();
        if (healthController != null)
        {
            healthController.TakeDamage(damageAmount);
        }

        // Förstör Shadow Ball när den träffar ett annat objekt
        Destroy(gameObject);
    }
}
