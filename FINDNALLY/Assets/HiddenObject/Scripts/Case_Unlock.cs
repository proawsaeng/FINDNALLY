using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case_Unlock : MonoBehaviour
{
    public int levelToUnlock = 2;
    
    public void WinLevel()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        
        int currentLevel = 1;
        if (currentLevel >= PlayerPrefs.GetInt("levelReached"))
        {
            PlayerPrefs.SetInt("levelReached", currentLevel + 1);
            Debug.Log("PlayerPrefsAdded");
        }
    }
}
