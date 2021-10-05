using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingMunu;

    public void Setting()
    {
        settingMunu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Back()
    {
        settingMunu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
    }
    
    
}
