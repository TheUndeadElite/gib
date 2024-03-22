using UnityEngine;

public class ShadowBall : MonoBehaviour
{
    public float speed = 5.0f;
    public int damageAmount = 10;
    public float destroyAfterTime = 4.0f; // Tid innan Shadow Ball f�rst�rs om den inte tr�ffar n�got

    private float timer;

    void Start()
    {
        // Starta timern n�r Shadow Ball skapas
        timer = 0f;
    }

    void Update()
    {
        // R�relse i fram�triktningen
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Uppdatera timern
        timer += Time.deltaTime;

        // F�rst�r Shadow Ball om tiden har �verskridits
        if (timer >= destroyAfterTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Om Shadow Ball tr�ffar ett objekt med HealthController, applicera skada
        HealthController healthController = other.GetComponent<HealthController>();
        if (healthController != null)
        {
            healthController.TakeDamage(damageAmount);
        }

        // F�rst�r Shadow Ball n�r den tr�ffar ett annat objekt
        Destroy(gameObject);
    }
}
