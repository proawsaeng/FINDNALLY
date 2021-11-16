using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private GameObject boughtHintObj;
    [SerializeField] private GameObject boughtAddtimeObj;
    [SerializeField] public GameObject noMoneyText;
    public float tweenTime;


    public void BuyHint()
    {
        if (PlayerPrefs.GetInt("Coin") >= 50)
        {
            //SFXbuy
            if (SfxManager.sfxInstance.musicToggle == true)
            {
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.buySound);
            }
            
            PlayerPrefs.SetInt("Hint", PlayerPrefs.GetInt("Hint") + 1);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 50);
            boughtHintObj.SetActive(true);
        }
        
        else
        {
            //SFXwrong
            if (SfxManager.sfxInstance.musicToggle == true)
            {
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.wrongSound);
            }
            
            Debug.Log("Not enough money!!");
            StartCoroutine(ShowNoMoneyText());
        }
    }

    public void BuyAddTime()
    {
        if (PlayerPrefs.GetInt("Coin") >= 50)
        {
            //SFXbuy
            if (SfxManager.sfxInstance.musicToggle == true)
            {
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.buySound);
            }
            
            PlayerPrefs.SetInt("AddTime", PlayerPrefs.GetInt("AddTime") + 1);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 50);
            boughtAddtimeObj.SetActive(true);
        }
        
        else
        {
            //SFXwrong
            if (SfxManager.sfxInstance.musicToggle == true)
            {
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.wrongSound);
            }
            
            Debug.Log("Not enough money!!");
            StartCoroutine(ShowNoMoneyText());
        }
    }

    public void SkipButton()
    {
        boughtAddtimeObj.SetActive(false);
        boughtHintObj.SetActive(false);
    }

    public void ShowNoMoneyText01()
    {
        noMoneyText.SetActive(true);
        LeanTween.scale(noMoneyText, noMoneyText.transform.localScale * 0.2f, tweenTime).setEasePunch();
    }

    public IEnumerator ShowNoMoneyText()
    {
        noMoneyText.SetActive(true);
        LeanTween.scale(noMoneyText, noMoneyText.transform.localScale * 1.5f, tweenTime).setEasePunch();
        
        yield return new WaitForSeconds(1f);
        noMoneyText.SetActive(false);
    }
}
