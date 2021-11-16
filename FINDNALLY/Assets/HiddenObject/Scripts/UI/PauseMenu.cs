using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMunu;

    public void Pause()
    {
        pauseMunu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMunu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("CaseMap");
    }

    public void EndlessHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("EndlessCase");
    }

}
