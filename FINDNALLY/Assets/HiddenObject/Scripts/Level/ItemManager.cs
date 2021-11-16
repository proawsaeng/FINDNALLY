using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public Text totalHintText;
    public int totalHint;

    public Text totalAddTimeText;
    public int totalAddTime;

    public void Start()
    {
        //totalHint = PlayerPrefs.GetInt("Hint");
        //totalAddTime = PlayerPrefs.GetInt("AddTime");
        
        if (!PlayerPrefs.HasKey("Hint") || !PlayerPrefs.HasKey("AddTime"))
        {
            PlayerPrefs.SetInt("Hint", 2);
            PlayerPrefs.SetInt("AddTime", 2);
            Load();
        }

        else
        {
            Load();
        }
    }

    public void Update()
    {
        ShowHintAmount();
        ShowAddTimeAmount();
    }

    public void ShowHintAmount()
    {
        int currentHint = PlayerPrefs.GetInt("Hint");
        totalHintText.text = currentHint.ToString();
    }

    public void ShowAddTimeAmount()
    {
        int currentAddTime = PlayerPrefs.GetInt("AddTime");
        totalAddTimeText.text = currentAddTime.ToString();
    }
    
    private void Load()
    {
        totalHint = PlayerPrefs.GetInt("Hint");
        totalAddTime = PlayerPrefs.GetInt("AddTime");
    }
}
