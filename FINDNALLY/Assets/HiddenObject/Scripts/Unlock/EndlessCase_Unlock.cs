using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessCase_Unlock : MonoBehaviour
{
    public int endlessLevelToUnlock;
    
    public void WinEndlessLevel()
    {
        PlayerPrefs.SetInt("endlessLevelReached", endlessLevelToUnlock);
    }
}
