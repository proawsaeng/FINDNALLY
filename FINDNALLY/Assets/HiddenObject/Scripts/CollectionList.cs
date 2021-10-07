using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class CollectionList : MonoBehaviour
{
    
    public Button[] DttList;
    [SerializeField] private GameObject[] collectButton;
    [SerializeField] private GameObject[] dttPrefab;
    private bool click01 = false;
    private bool click02 = false;
    private bool click03 = false;
    private bool click04 = false;
    

    void Start()
    {
        int speciallevelReached = PlayerPrefs.GetInt("DTTUnlock", 1);

        Debug.Log("DTTUnlock : " + speciallevelReached);
        //Debug.Log("CollectButtonShow : " + collectButtonClicked);

        for (int i = 0; i < DttList.Length; i++)
        {
            if(i + 1 > speciallevelReached)
                DttList[i].interactable = false;

            if (i < speciallevelReached)
            {
                DttList[i].interactable = true;
                ForCollectButton();
            }
        }
    }

    public void ForCollectButton()
    {
        int collectButtonShowing = PlayerPrefs.GetInt("Collected", 1);
        
        //เพิ่มโค้ดโชว์ปุ่ม ในเงื่อนไข
        for (int j = 0; j < collectButton.Length; j++)
        {
            if (j + 1 > collectButtonShowing)
            {
                Debug.Log("show button : " + collectButtonShowing);
                collectButton[j].SetActive(false);
            }
            
            if (j < collectButtonShowing)
            {
                Load();
                
                //click01
                if (click01 == false)
                {
                    collectButton[0].SetActive(true);
                }

                if (click01 == true)
                {
                    collectButton[0].SetActive(false);
                    
                    Debug.Log("Show Detective 01");
                    dttPrefab[0].SetActive(true);
                }

                //click02
                if (click02 == false)
                {
                    collectButton[1].SetActive(true);
                }

                if (click02 == true)
                {
                    collectButton[1].SetActive(false);
                    
                    Debug.Log("Show Detective 02");
                    dttPrefab[1].SetActive(true);
                }
                
                //click03
                if (click03 == false)
                {
                    collectButton[2].SetActive(true);
                }

                if (click03 == true)
                {
                    collectButton[2].SetActive(false);
                    
                    Debug.Log("Show Detective 03");
                    dttPrefab[2].SetActive(true);
                }
                
                //click04
                if (click04 == false)
                {
                    collectButton[3].SetActive(true);
                }

                if (click04 == true)
                {
                    collectButton[3].SetActive(false);
                    
                    Debug.Log("Show Detective 04");
                    dttPrefab[3].SetActive(true);
                }
            }
        }
    }

    public void Load()
    {
        click01 = PlayerPrefs.GetInt("clicked01") == 1;
        click02 = PlayerPrefs.GetInt("clicked02") == 1;
        click03 = PlayerPrefs.GetInt("clicked03") == 1;
        click04 = PlayerPrefs.GetInt("clicked04") == 1;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("clicked01", click01 ? 1 : 0);
        PlayerPrefs.SetInt("clicked02", click02 ? 1 : 0);
        PlayerPrefs.SetInt("clicked03", click03 ? 1 : 0);
        PlayerPrefs.SetInt("clicked04", click04 ? 1 : 0);
    }
    
}
