using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public DamageTaker DamageTaker;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisioEnter2D(Collision2D collision)
    {
        if(CompareTag("Enemy"))
        {
            Debug.Log("Enemy take damage ");
            DamageTaker = collision.gameObject.GetComponent<DamageTaker>();
            if (DamageTaker != null )
            {
                DamageTaker.TakeDamage (10);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
