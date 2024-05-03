using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter Instance;

    public TMP_Text coinText;
    public int currentCoins = 0;
    [SerializeField] Animator canvasAnimator;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        coinText.text = currentCoins.ToString();
    }
    public void IncreaseCoins(int v)
    {
    currentCoins += v;
        coinText.text =  currentCoins.ToString();
        canvasAnimator.SetTrigger("CoinPickUp");
    }
}
