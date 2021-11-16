using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip foundSound, wrongSound, hintSound, addTimeSound, winSound, loseSound, unlockSound, buySound, clickSound;
    public static SfxManager sfxInstance;

    public bool musicToggle = true;
    
    private void Awake()
    {
        if (sfxInstance == null)
        {
            sfxInstance = this;
            DontDestroyOnLoad(sfxInstance);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
