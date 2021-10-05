using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Collection_Unlock : MonoBehaviour
{
    public int dttToUnlock = 2;


    public void DttUnlock()
    {
        PlayerPrefs.SetInt("DTTUnlock", dttToUnlock);

        int currentLevel = 1;
        if (currentLevel >= PlayerPrefs.GetInt("DTTUnlock"))
        {
            
            PlayerPrefs.SetInt("DTTUnlock", currentLevel + 1);
            Debug.Log("New detective unlocked.");
        }
    }
}
