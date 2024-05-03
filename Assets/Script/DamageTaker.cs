using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaker : MonoBehaviour
{
    public GameObject player;

    public float AttackRange = 2.0f;
    public int MaxHealth = 3;
    public int currentHealth;

    [SerializeField] Animator snokenAnimator;

    public int damageAmount = 1;

    private object collision;

    public GameObject loot = null;
    public float ChanceToDrop = 0.5f;

    void Start()
    {
        currentHealth = MaxHealth;
    }


    void Update() 
    { 
        if (player != null)
        {
            Vector3 scale = transform.localScale;

            if (player.transform.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x);
            }
            else
            {
                scale.x = -Mathf.Abs(scale.x);
            }
            if(AttackRange > (player.transform.position - transform.position).magnitude)
            {
                //if (!snokenAnimator.GetBool("isAttacking"))
                //{
                    snokenAnimator.SetBool("isAttacking", true);


                //}
            }
            else
            {
                snokenAnimator.SetBool("isAttacking", false);
            }
        }
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

        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damageAmount);
            Debug.Log("Player Damaged " + damageAmount);
        }
    }

    private void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        HealthController healthComponent = collision.gameObject.GetComponent<HealthController>();

        if (healthComponent != null)
        {

        }

    }
    //private void OnTriggerEnter2D(Collider2D amount)
    //{
    //    if (amount.CompareTag("Knight"))
    //    {
    //        TakeDamage(10);
    //    }
    //}

    void Die()
    {
        if (Random.Range(0.0f, 1.0f) > ChanceToDrop && loot != null)
        {
            GameObject.Instantiate(loot,transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    //var DirectionX = Mathf.Sign(collision.transform.position.x - transform.position.x);
    //var scaleLocal = transform.localScale;

    //scaleLocal.x *= DirectionX;
    //        transform.localScale = scaleLocal;
}
