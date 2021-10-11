﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject hiddenObjectIconHolder;     
    [SerializeField] private GameObject hiddenObjectIconPrefab;     
    [SerializeField] private GameObject gameCompleteObj;
    [SerializeField] private GameObject timeUpObj;
    [SerializeField] private Text timerText;
    [SerializeField] private Text hintText;
    [SerializeField] private Text timeAddText;
    private int hintAmount;
    private int addTimeAmount;
    [SerializeField] public Button hintButton;
    [SerializeField] public Button timeAddButton;
    

    private List<GameObject> hiddenObjectIconList;                  

    public GameObject GameCompleteObj { get => gameCompleteObj; }

    public GameObject TimeUpObj { get { return timeUpObj; } }

    public Text TimerText { get => timerText; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        hiddenObjectIconList = new List<GameObject>();              
    }

    private void Start()
    {
        hintAmount = PlayerPrefs.GetInt("Hint");
        addTimeAmount = PlayerPrefs.GetInt("AddTime");
    }


    public void PopulateHiddenObjectIcons(List<HiddenObjectData> hiddenObjectData)
    {
        hiddenObjectIconList.Clear();                               
        for (int i = 0; i < hiddenObjectData.Count; i++)            
        {
                                                                    
            GameObject icon = Instantiate(hiddenObjectIconPrefab, hiddenObjectIconHolder.transform);
            icon.name = hiddenObjectData[i].hiddenObj.name;         
            Image childImg = icon.transform.GetChild(0).GetComponent<Image>();  
            Text childText = icon.transform.GetChild(1).GetComponent<Text>();  

            childImg.sprite = hiddenObjectData[i].hiddenObj.GetComponent<SpriteRenderer>().sprite; 
            childText.text = hiddenObjectData[i].name;                          
            hiddenObjectIconList.Add(icon);                                     
        }
    }

    
    public void CheckSelectedHiddenObject(string index)
    {
        for (int i = 0; i < hiddenObjectIconList.Count; i++)                    
        {
            if (index == hiddenObjectIconList[i].name)                          
            {
                hiddenObjectIconList[i].SetActive(false);                       
                break;                                                          
            }
        }
    }


    public void NextButton()                                                    
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       
    }

    public void TryAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HintButton()
    {
        //ลดจำนวนตอนกด + แสดงจำนวน
        StartCoroutine(LevelManager.instance.HintObject());
        
        PlayerPrefs.SetInt("Hint", PlayerPrefs.GetInt("Hint") - 1);
        
        hintText.text = "" + hintAmount;
        if (hintAmount <= 0)
        {
            hintButton.interactable = false;
        }
    }

    public void AddTimeButton()
    {
        StartCoroutine(LevelManager.instance.AddTimeObject());
        
        PlayerPrefs.SetInt("AddTime", PlayerPrefs.GetInt("AddTime") - 1);
        
        timeAddText.text = "" + addTimeAmount;
        if (addTimeAmount <= 0)
        {
            timeAddButton.interactable = false;
        }
        
    }

    public void CaseButton()
    {
        SceneManager.LoadScene("CaseScene");
    }
    
    public void OfficeButton()
    {
        SceneManager.LoadScene("DttHomeScene");
    }
    
    public void CollectionButton()
    {
        SceneManager.LoadScene("CollectionScene");
    }

    public void StoreButton()
    {
        SceneManager.LoadScene("Store");
        Debug.Log("STORE");
    }

    public void CaseOne()
    {
        SceneManager.LoadScene("01Case_Kitchen");
    }
    
    public void CaseTwo()
    {
        SceneManager.LoadScene("02Case_Livingroom");
    }
    
    public void CaseThree()
    {
        SceneManager.LoadScene("03Case_Bedroom");
    }
    
    public void CaseFour()
    {
        SceneManager.LoadScene("04Special_Birds");
    }
    
    public void CaseFive()
    {
        SceneManager.LoadScene("05Case_Bathroom");
    }
    
    public void CaseSix()
    {
        SceneManager.LoadScene("06Case_Space");
    }
    
    public void CaseSeven()
    {
        SceneManager.LoadScene("07Special_Birds");
    }
}
