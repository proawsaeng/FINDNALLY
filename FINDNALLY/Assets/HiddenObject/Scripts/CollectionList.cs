using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class CollectionList : MonoBehaviour
{
    
    public Button[] SpeciallevelButtons;

    void Start()
    {
        int speciallevelReached = PlayerPrefs.GetInt("DTTUnlock", 1);

        for (int i = 0; i < SpeciallevelButtons.Length; i++)
        {
            if(i + 1 > speciallevelReached)
                SpeciallevelButtons[i].interactable = false;

            if (i < speciallevelReached)
            {
                SpeciallevelButtons[i].interactable = true;
                //DTTUnlockCHECK
                Debug.Log("DTTUnlock : " + speciallevelReached);

                //collectButtonToShow = PlayerPrefs.GetInt("CollectButton", collectButtonToShow);
                //showCollectionButton.ShowCollectButton();
            }
        }
    }
}
