using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaker : MonoBehaviour
{
    public int MaxHealth = 3;
    public int currentHealth;

    public int damageAmount = 1;


    void Start()
    {
        currentHealth = MaxHealth;
    }


    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        HealthController healthComponent = collision.gameObject.GetComponent<HealthController>();

        if (healthComponent != null )
        {
            
            healthComponent.TakeDamage(damageAmount);
        }

    }

    private void OnTriggerEnter2D(Collider2D amount)
    {
        if (amount.CompareTag("Knight"))
        {
            TakeDamage(10);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
