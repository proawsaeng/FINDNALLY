using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    
    public Button[] levelButtons;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        
        Debug.Log("levelReached : " + levelReached);
        
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i + 1 > levelReached)
                levelButtons[i].interactable = false;
            
            if (i < levelReached)
                levelButtons[i].interactable = true;
        }

    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
