using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [System.NonSerialized]
    public int value;
    [SerializeField] GameObject pickUpEffect;

    public float dropChance = 0.5f; // Probability of dropping the coin, e.g., 50%

    void Start()
    {
        if (Random.value < dropChance)
        {
            value = Random.Range(1, 3);
            Debug.Log("Coin value: " + value);
        }
        else
        {
            // If the drop chance fails, destroy the coin
            Destroy(gameObject);
        }
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Knight"))
        {
            Instantiate(pickUpEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            CoinCounter.Instance.IncreaseCoins(value);
            Debug.Log("Total coins collected: " + CoinCounter.Instance.currentCoins); // Add debug log
        }
    }
}
