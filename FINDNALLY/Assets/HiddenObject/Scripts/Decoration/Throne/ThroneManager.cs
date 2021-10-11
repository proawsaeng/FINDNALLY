﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThroneManager : MonoBehaviour
{
    public ThroneDB throneDB;
    public Image artworkImage;
    
    public ThroneUpgrade hideButton;

    private int throneSelectedOption = 0;
    void Start()
    {
        if (!PlayerPrefs.HasKey("throneSelectedOption"))
        {
            throneSelectedOption = 0;
        }
        else
        {
            Load();
        }
        
        UpdateThrone(throneSelectedOption);
    }

    public void NextOption()
    {
        throneSelectedOption++;

        if (throneSelectedOption >= throneDB.ThroneCount)
        {
            throneSelectedOption = 0;
        }
        
        UpdateThrone(throneSelectedOption);
        Save();
    }

    public void BackOption()
    {
        throneSelectedOption--;

        if (throneSelectedOption < 0)
        {
            throneSelectedOption = throneDB.ThroneCount - 1;
        }
        
        UpdateThrone(throneSelectedOption);
        Save();
    }

    private void UpdateThrone(int throneSelectedOption)
    {
        Throne throne = throneDB.GetThrone(throneSelectedOption);
        artworkImage.sprite = throne.throneSprite;
    }

    private void Load()
    {
        throneSelectedOption = PlayerPrefs.GetInt("throneSelectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("throneSelectedOption", throneSelectedOption);
    }

    public void DoneSelection()
    {
        //ShowObjectSelected
        Throne throne = throneDB.GetThrone(throneSelectedOption);
        artworkImage.sprite = throne.throneSprite;
        
        hideButton.HideButtons();
    }
}
