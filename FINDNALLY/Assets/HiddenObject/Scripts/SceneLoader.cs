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

    public async void LoadScene(int sceneIndex)
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

        await Task.Delay(1000);    //Just For Demo
        loadNextScene.allowSceneActivation = true;
        loadingScreen.SetActive(false);
    }

    private void Update()
    {
        progressImage.fillAmount = Mathf.MoveTowards(progressImage.fillAmount, target, 5 * Time.deltaTime);
       //loadingText.text = "Loading" + loadNextScene.progress * 100f + "%";
    }
}
