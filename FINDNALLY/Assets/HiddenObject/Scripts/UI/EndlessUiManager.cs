using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndlessUiManager : MonoBehaviour
{
    public static EndlessUiManager instance;

    [SerializeField] private GameObject hiddenObjectIconHolder;     
    [SerializeField] private GameObject hiddenObjectIconPrefab;     
    [SerializeField] private GameObject gameCompleteObj;
    private List<GameObject> hiddenObjectIconList;    
    
    //Energy check
    [SerializeField] public GameObject outOfEnergyPanel;
    public GameObject currentEnergy;
    private Energy energy;

    public GameObject GameCompleteObj { get => gameCompleteObj; }

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
        energy = currentEnergy.GetComponent<Energy>();
    }

    public void PopulateHiddenObjectIcons(List<EndlessLevelManager_.EndlessHiddenObjectData> endlessHiddenObjectData)
    {
        hiddenObjectIconList.Clear();                               
        for (int i = 0; i < endlessHiddenObjectData.Count; i++)            
        {
                                                                    
            GameObject icon = Instantiate(hiddenObjectIconPrefab, hiddenObjectIconHolder.transform);
            icon.name = endlessHiddenObjectData[i].hiddenObj.name;         
            Image childImg = icon.transform.GetChild(0).GetComponent<Image>();  
            Text childText = icon.transform.GetChild(1).GetComponent<Text>();  

            childImg.sprite = endlessHiddenObjectData[i].hiddenObj.GetComponent<SpriteRenderer>().sprite; 
            childText.text = endlessHiddenObjectData[i].name;                          
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


    public void HomeButton()
    {
        SceneManager.LoadScene("EndlessCase");
    }
    
    public void NextButton()                                                    
    {
        if (energy.currentEnergy >= 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            //Show Out of Energy Panel
            outOfEnergyPanel.SetActive(true);
        }
    }

    public void TryAgainButton()
    {
        if (energy.currentEnergy >= 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            //Show Out of Energy Panel
            outOfEnergyPanel.SetActive(true);
        }
    }
    
    public void CloseEnergyOutPanel()
    {
        outOfEnergyPanel.SetActive(false);
    }
    
    public void CaseButton()
    {
        SceneManager.LoadScene("CaseMap");
    }
    
    public void OfficeButton()
    {
        SceneManager.LoadScene("EndlessCase");
    }
    
    public void CollectionButton()
    {
        SceneManager.LoadScene("CollectionScene");
    }

    public void StoreButton()
    {
        SceneManager.LoadScene("Store");
    }

    public void EndlessCaseOne()
    {
        SceneManager.LoadScene("Level01");
    }
}
