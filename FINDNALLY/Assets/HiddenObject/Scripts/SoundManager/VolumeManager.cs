using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] public Image soundOnButton;
    [SerializeField] public Image soundOffButton;
    
    [SerializeField] public Image sfxOnButton;
    [SerializeField] public Image sfxOffButton;
    
    private bool muted = false;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }

        else
        {
            Load();
        }
        
        UpdateMusicButtonIcon();
        UpdateSfxButtonIcon();
    }
    
    

    public void MusicToggle()
    {
        if (BGMusic.bgInstance.Audio.isPlaying)
        {
            muted = true;
            BGMusic.bgInstance.Audio.Pause();
            soundOnButton.enabled = true;
            soundOffButton.enabled = false;
        }
        else
        {
            muted = false;
            BGMusic.bgInstance.Audio.Play();
            soundOnButton.enabled = false;
            soundOffButton.enabled = true;
        }
        
        Save();
        UpdateMusicButtonIcon();
    }

    public void SfxToggle()
    {
        if (SfxManager.sfxInstance.musicToggle == true)
        {
            SfxManager.sfxInstance.musicToggle = false;
        }
        else
        {
            SfxManager.sfxInstance.musicToggle = true;
        }
        
        UpdateSfxButtonIcon();
    }
    
    private void UpdateMusicButtonIcon()
    {
        if (muted == false)
        {
            soundOnButton.enabled = true;
            soundOffButton.enabled = false;
        }

        else
        {
            soundOnButton.enabled = false;
            soundOffButton.enabled = true;
        }
    }
    
    private void UpdateSfxButtonIcon()
    {
        if (SfxManager.sfxInstance.musicToggle == true)
        {
            sfxOnButton.enabled = true;
            sfxOffButton.enabled = false;
        }

        else
        {
            sfxOnButton.enabled = false;
            sfxOffButton.enabled = true;
        }
    }
    
    private void Load()
    {
        //Debug.Log("muted : " + muted);
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
    
}
