 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    public TMP_Text text;
    public int coins = 100;

    public static CurrencyController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    //Singleton Workflow

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.SetText(coins.ToString());
    }

    public void ExchangeCoins(int amount)
    {
        coins += amount;
    }
}
