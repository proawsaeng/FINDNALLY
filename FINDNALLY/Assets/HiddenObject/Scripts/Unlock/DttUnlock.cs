using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class DttUnlock : MonoBehaviour
{
    public Image original;
    public Sprite reveal;
    private bool click01 = false;
    private bool click02 = false;
    private bool click03 = false;
    private bool click04 = false;
    
    private void Start()
    {
        if (!PlayerPrefs.HasKey("clicked01") || !PlayerPrefs.HasKey("clicked02") || !PlayerPrefs.HasKey("clicked03") || !PlayerPrefs.HasKey("clicked04"))
        {
            PlayerPrefs.SetInt("clicked01", 0);
            PlayerPrefs.SetInt("clicked02", 0);
            PlayerPrefs.SetInt("clicked03", 0);
            PlayerPrefs.SetInt("clicked04", 0);
            Load();
        }

        else
        {
            Load();
        }
    }

    public void UnlockDtt01()
    {
        //SFXunlock
        if (SfxManager.sfxInstance.musicToggle == true)
        {
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.unlockSound);
        }
        
        //change image
        Destroy(gameObject);
        original.sprite = reveal;

        //เก็บค่า click ของ Dtt01 เป็น true
        click01 = true;
        Save();
    }
    
    public void UnlockDtt02()
    {
        //SFXunlock
        if (SfxManager.sfxInstance.musicToggle == true)
        {
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.unlockSound);
        }
        
        Destroy(gameObject);
        original.sprite = reveal;
        
        click02 = true;
        Save();
    }
    
    public void UnlockDtt03()
    {
        //SFXunlock
        if (SfxManager.sfxInstance.musicToggle == true)
        {
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.unlockSound);
        }
        
        Destroy(gameObject);
        original.sprite = reveal;
        
        click03 = true;
        Save();
    }
    
    public void UnlockDtt04()
    {
        //SFXunlock
        if (SfxManager.sfxInstance.musicToggle == true)
        {
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.unlockSound);
        }
        
        Destroy(gameObject);
        original.sprite = reveal;
        
        click04 = true;
        Save();
    }
    
    

    private void Load()
    {
        click01 = PlayerPrefs.GetInt("clicked01") == 1;
        click02 = PlayerPrefs.GetInt("clicked02") == 1;
        click03 = PlayerPrefs.GetInt("clicked03") == 1;
        click04 = PlayerPrefs.GetInt("clicked04") == 1;
    }
    
    private void Save()
    {
        PlayerPrefs.SetInt("clicked01", click01 ? 1 : 0);
        PlayerPrefs.SetInt("clicked02", click02 ? 1 : 0);
        PlayerPrefs.SetInt("clicked03", click03 ? 1 : 0);
        PlayerPrefs.SetInt("clicked04", click04 ? 1 : 0);
    }
}
