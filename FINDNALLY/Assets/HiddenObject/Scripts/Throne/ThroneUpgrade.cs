using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThroneUpgrade : MonoBehaviour
{
    public GameObject upgradeButton;
    public GameObject nextButton;
    public GameObject backButton;
    public GameObject doneButton;
    [SerializeField] private GameObject throne;

    public void ShowThroneSelection()
    {
        if (PlayerPrefs.GetInt("Coin") >= 50)
        {
            upgradeButton.SetActive(false);
            throne.SetActive(true);
            nextButton.SetActive(true);
            backButton.SetActive(true);
            doneButton.SetActive(true);
            
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 50);
        }

        else
        {
            Debug.Log("Not enough money!!");
        }
    }
}
