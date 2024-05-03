using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject coinPrefab;
    public Transform spawnPosition;

    void Update()
    {
        if (currentHealth <= 0)
        {
            Instantiate(coinPrefab, spawnPosition.position, Quaternion.identity);

            
            Destroy(gameObject);
        }
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            
            Die();
        }
    }

    void Die()
    {
        // Implementera logik n�r fienden d�r, till exempel att f�rst�ra objektet
       
        Destroy(gameObject);
    }
}
