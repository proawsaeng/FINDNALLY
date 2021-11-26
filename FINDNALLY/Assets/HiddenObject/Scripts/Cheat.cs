using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheat : MonoBehaviour
{
    [SerializeField] public GameObject cheatCodePanel;
    [SerializeField] public GameObject energy;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("Cheat Code");
            
            cheatCodePanel.SetActive(true);
        }
    }

    public void StopTime()
    {
        Debug.Log("Stop Time");
        
        Time.timeScale = 0f;
    }

    public void TurnOffStopTime()
    {
        Time.timeScale = 1f;
    }

    public void ClosePanel()
    {
        cheatCodePanel.SetActive(false);
    }
}
