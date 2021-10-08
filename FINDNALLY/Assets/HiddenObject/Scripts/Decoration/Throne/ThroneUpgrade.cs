using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThroneUpgrade : MonoBehaviour
{
    public GameObject upgradeButton;
    public GameObject nextButton;
    public GameObject backButton;
    public GameObject doneButton;
    [SerializeField] private GameObject throne;
    
    //for upgrade button
    private bool throneUpgradeButton = false;
    
    //Show selected object
    private int selectedOption = 0;
    
    void Start()
    {
        if (!PlayerPrefs.HasKey("ThroneUpgradeClick") || !PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
            PlayerPrefs.SetInt("ThroneUpgradeClick", 0);
            Load();
            
            ShowUpgradeButton();
        }

        else
        {
            Load();
            ShowUpgradeButton();
        }
    }
    
    public void ShowUpgradeButton()
    {
        //Check if button clicked
        if (throneUpgradeButton == false)
        {
            Debug.Log("throneUpgradeButton : " + throneUpgradeButton);
            upgradeButton.SetActive(true);
        }

        if (throneUpgradeButton == true)
        {
            Debug.Log("throneUpgradeButton : " + throneUpgradeButton);
            upgradeButton.SetActive(false);
            
            Debug.Log("Show Throne Selected");

            GameObject showThrone = Instantiate(throne,new Vector3(101, -163, 0f),Quaternion.identity) as GameObject;
            showThrone.transform.SetParent(GameObject.FindGameObjectWithTag("Throne").transform,false);
            showThrone.SetActive(true);
        }
    }

    public void ShowThroneSelection()
    {
        if (PlayerPrefs.GetInt("Coin") >= 50)
        {
            throneUpgradeButton = true;
            Save();
            
            upgradeButton.SetActive(false);
            throne.SetActive(true);
            nextButton.SetActive(true);
            backButton.SetActive(true);
            doneButton.SetActive(true);
            
            //PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 50);
        }

        else
        {
            Debug.Log("Not enough money!!");
        }
    }
    
    public void HideButtons()
    {
        nextButton.SetActive(false);
        backButton.SetActive(false);
        doneButton.SetActive(false);
    }
    
    private void Load()
    {
        throneUpgradeButton = PlayerPrefs.GetInt("PlantUpgradeClick") == 1;
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
    
    private void Save()
    {
        PlayerPrefs.SetInt("PlantUpgradeClick", throneUpgradeButton ? 1 : 0);
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }
}
