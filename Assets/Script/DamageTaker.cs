using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaker : MonoBehaviour
{
    public int damageAmount = 1;

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        HealthController healthComponent = collision.gameObject.GetComponent<HealthController>();

        if (healthComponent != null )
        {
            
            healthComponent.TakeDamage(damageAmount);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
