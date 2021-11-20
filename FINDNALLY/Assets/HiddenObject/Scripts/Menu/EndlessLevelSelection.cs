using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessLevelSelection : MonoBehaviour
{
    public Button[] levelButtons;

    void Start()
    {
        int endlessLevelReached = PlayerPrefs.GetInt("endlessLevelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i + 1 > endlessLevelReached)
                levelButtons[i].interactable = false;

            if (i < endlessLevelReached)
            {
                levelButtons[i].interactable = true;
            }
        }

    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
