using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text totalPlayerCoinsText;
    public int totalPlayerCoins;

    public void Start()
    {
        totalPlayerCoins = PlayerPrefs.GetInt("Coin");
    }

    public void Update()
    {
        ShowCoin();
    }

    public void ShowCoin()
    {
        int currentCoin = PlayerPrefs.GetInt("Coin");
        totalPlayerCoinsText.text = currentCoin.ToString();
    }
}
