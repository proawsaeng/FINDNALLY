using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public static BGMusic bgInstance;
    public AudioSource Audio;

    private void Awake()
    {
        if (bgInstance == null)
        {
            bgInstance = this;
            DontDestroyOnLoad(bgInstance);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }
}
