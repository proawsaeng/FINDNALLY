using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public void BuyHint()
    {
        if (PlayerPrefs.GetInt("Coin") >= 50)
        {
            PlayerPrefs.SetInt("Hint", PlayerPrefs.GetInt("Hint") + 1);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 50);
        }
        
        else
        {
            Debug.Log("Not enough money!!");
        }
    }

    public void BuyAddTime()
    {
        if (PlayerPrefs.GetInt("Coin") >= 50)
        {
            PlayerPrefs.SetInt("AddTime", PlayerPrefs.GetInt("AddTime") + 1);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 50);
        }
        
        else
        {
            Debug.Log("Not enough money!!");
        }
    }
}
