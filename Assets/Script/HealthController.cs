using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    public int MaxHealth = 3;
    public int CurrentHealth;

    // Name of the death screen scene
    public string deathScreenSceneName = "DeathScreen";

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Switch to the death screen scene
        SceneManager.LoadScene(deathScreenSceneName);
    }
}
