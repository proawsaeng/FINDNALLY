using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantManager : MonoBehaviour
{
    public PlantDatabase plantDB;
    public Image artworkImage;
    private int selectedOption = 0;

    public PlantUpgrade hideButton;
    
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
        
        if (!PlayerPrefs.HasKey("Plantclick"))
        {
            PlayerPrefs.SetInt("Plantclick", 0);
            Load();
        }

        else
        {
            Load();
        }
        
        UpdatePlant(selectedOption);
        Load();
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= plantDB.PlantCount)
        {
            selectedOption = 0;
        }
        
        UpdatePlant(selectedOption);
        Save();
    }

    public void BackOption()
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = plantDB.PlantCount - 1;
        }
        
        UpdatePlant(selectedOption);
        Save();
    }

    private void UpdatePlant(int selectedOption)
    {
        Plant plant = plantDB.GetPlant(selectedOption);
        artworkImage.sprite = plant.plantSprite;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    public void DoneSelection()
    {
        //ShowObjectSelected
        Plant plant = plantDB.GetPlant(selectedOption);
        artworkImage.sprite = plant.plantSprite;
        
        hideButton.HideButtons();
    }
    
}
