using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [System.NonSerialized]
    public int value;


    void Start()
    {
        value = Random.Range(1, 15); 
        Debug.Log("Coin value: " + value); 
    }


    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Knight"))
        {
            Destroy(gameObject);
            CoinCounter.Instance.IncreaseCoins(value);
            Debug.Log("Total coins collected: " + CoinCounter.Instance.currentCoins); // Add debug log
        }
    }
}
