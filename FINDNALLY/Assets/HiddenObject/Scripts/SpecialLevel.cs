using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialLevel : MonoBehaviour
{
    
    [SerializeField] private GameObject specialPanel;
    
    void Start()
    {
        specialPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Play()
    {
        specialPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
