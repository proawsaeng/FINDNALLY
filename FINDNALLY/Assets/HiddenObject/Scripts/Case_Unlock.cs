using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case_Unlock : MonoBehaviour
{
    public int levelToUnlock = 2;
    
    public void WinLevel()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
    }
}
