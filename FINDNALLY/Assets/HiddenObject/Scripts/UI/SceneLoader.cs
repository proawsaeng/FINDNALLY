using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private AsyncOperation loadNextScene;
    public Image progressImage;
    public GameObject loadingScreen;
    public Text loadingText;
    private float target;
    
    //Energy check
    [SerializeField] public GameObject outOfEnergyPanel;
    public GameObject currentEnergy;
    private Energy energy;

    private void Start()
    {
        energy = currentEnergy.GetComponent<Energy>();
    }

    public async void LoadScene(int sceneIndex)
    {
        if (energy.currentEnergy >= 5)
        {
            target = 0;
            progressImage.fillAmount = 0;
        
            loadNextScene = SceneManager.LoadSceneAsync(sceneIndex);
            loadNextScene.allowSceneActivation = false;
        
            loadingScreen.SetActive(true);

            do
            {
                await Task.Delay(1000);
                target = loadNextScene.progress;
            } while (loadNextScene.progress < 0.9f);

            await Task.Delay(1000);
            loadNextScene.allowSceneActivation = true;
            loadingScreen.SetActive(false);
        }
        else
        {
            //Show Out of Energy Panel
            outOfEnergyPanel.SetActive(true);
        }
    }

    private void Update()
    {
        progressImage.fillAmount = Mathf.MoveTowards(progressImage.fillAmount, target, 5 * Time.deltaTime);
       //loadingText.text = "Loading" + loadNextScene.progress * 100f + "%";
    }
    
    public void CloseEnergyOutPanel()
    {
        outOfEnergyPanel.SetActive(false);
    }
    
    public async void TitlePlayGame(int sceneIndex)
    {
        target = 0;
        progressImage.fillAmount = 0;
        
        loadNextScene = SceneManager.LoadSceneAsync(sceneIndex);
        loadNextScene.allowSceneActivation = false;
        
        loadingScreen.SetActive(true);

        do
        {
            await Task.Delay(1000);
            target = loadNextScene.progress;
        } while (loadNextScene.progress < 0.9f);

        await Task.Delay(1000);
        loadNextScene.allowSceneActivation = true;
        loadingScreen.SetActive(false);
    }
}
