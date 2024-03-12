using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordController : MonoBehaviour
{
    public DamageTaker DamageTaker;
    bool isDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        isDamage = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DamageTaker = collision.gameObject.GetComponent<DamageTaker>();

            if ((DamageTaker != null))
            {
                DamageTaker.TakeDamage(1);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDamage = true;
        }
        else
        {
            isDamage = false;
        }
        Debug.Log(isDamage);
    }
}
