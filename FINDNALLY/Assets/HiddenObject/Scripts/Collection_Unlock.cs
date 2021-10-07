using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Collection_Unlock : MonoBehaviour
{
    public int dttToUnlock = 2;
    public int collectButtonWillShow = 2;


    public void DttUnlock()
    {
        PlayerPrefs.SetInt("DTTUnlock", dttToUnlock);
        PlayerPrefs.SetInt("Collected", collectButtonWillShow);
        //Debug.Log("WinSpecialLevel & DttUnlock : " + dttToUnlock);
        //Debug.Log("Collection Unlock : " + collectButtonWillShow);
    }
}
