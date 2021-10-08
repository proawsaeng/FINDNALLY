using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantUpgrade : MonoBehaviour
{
    public GameObject upgradeButton;
    public GameObject nextButton;
    public GameObject backButton;
    public GameObject doneButton;
    [SerializeField] private GameObject plant;
    
    //for upgrade button
    private bool plantUpgradeButton = false;
    
    //Show selected object
    private int selectedOption = 0;

    void Start()
    {
        if (!PlayerPrefs.HasKey("PlantUpgradeClick") || !PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
            PlayerPrefs.SetInt("PlantUpgradeClick", 0);
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
        if (plantUpgradeButton == false)
        {
            Debug.Log("plantUpgradeButton : " + plantUpgradeButton);
            upgradeButton.SetActive(true);
        }

        if (plantUpgradeButton == true)
        {
            Debug.Log("plantUpgradeButton : " + plantUpgradeButton);
            upgradeButton.SetActive(false);
            
            Debug.Log("Show Plant Selected");

            GameObject showPlant = Instantiate(plant,new Vector3(249, -157, 0f),Quaternion.identity) as GameObject;
            showPlant.transform.SetParent(GameObject.FindGameObjectWithTag("Plant").transform,false);
            showPlant.SetActive(true);
        }
    }
    
    public void ShowPlantSelection()
    {
        if (PlayerPrefs.GetInt("Coin") >= 50)
        {
            plantUpgradeButton = true;
            Save();
            
            upgradeButton.SetActive(false);
            plant.SetActive(true);
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
        plantUpgradeButton = PlayerPrefs.GetInt("PlantUpgradeClick") == 1;
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
    
    private void Save()
    {
        PlayerPrefs.SetInt("PlantUpgradeClick", plantUpgradeButton ? 1 : 0);
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }
}
