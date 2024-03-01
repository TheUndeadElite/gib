using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int MaxHealth = 3;
    public int CurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = CurrentHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            //player dies
        }
    }
    
        
    


    // Update is called once per frame
    void Update()
    {
        
    }
}
