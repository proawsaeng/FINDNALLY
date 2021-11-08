using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Energy : MonoBehaviour
{
    [SerializeField] private Text energyText;
    [SerializeField] private Text energyTimerText;
    [SerializeField] private Slider energyBar;
    private int maxEnergy = 30;
    private int currentEnergy;
    private int restoreDuration = 5;
    private DateTime nextEnergyTime;
    private DateTime lastEnergyTime;
    private bool isRestoring = false;

    void Start()
    {
        if (!PlayerPrefs.HasKey("currentEnergy"))
        {
            PlayerPrefs.SetInt("currentEnergy", 30);
            Load();
            StartCoroutine(RestoreEnergy());
        }

        else
        {
            Load();
            StartCoroutine(RestoreEnergy());
        }
    }

    public void UseEnergy()
    {
        if (currentEnergy >= 5)
        {
            currentEnergy -= 5;
            UpdateEnergy();
            
            Debug.Log("current energy : " + currentEnergy);

            if (isRestoring == false)
            {
                if (currentEnergy == maxEnergy)
                {
                    nextEnergyTime = AddDuration(DateTime.Now, restoreDuration);
                }

                StartCoroutine(RestoreEnergy());
            }
        }
        else
        {
            Debug.Log("Out of energy!");
        }
    }

    private IEnumerator RestoreEnergy()
    {
        UpdateEnergyTimer();
        isRestoring = true;

        while (currentEnergy < maxEnergy)
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime nextDateTime = nextEnergyTime;
            bool isEnergyAdding = false;

            while (currentDateTime > nextDateTime)
            {
                if (currentEnergy < maxEnergy)
                {
                    isEnergyAdding = true;
                    currentEnergy++;
                    UpdateEnergy();

                    DateTime timeToAdd = lastEnergyTime > nextDateTime ? lastEnergyTime : nextDateTime;
                    nextDateTime = AddDuration(timeToAdd, restoreDuration);
                }
                else
                {
                    break;
                }
            }

            if (isEnergyAdding == true)
            {
                lastEnergyTime = DateTime.Now;
                nextEnergyTime = nextDateTime;
            }
            
            UpdateEnergyTimer();
            UpdateEnergy();
            Save();
            yield return null;
        }

        isRestoring = false;
    }

    private DateTime AddDuration(DateTime datetime, int duration)
    {
        return datetime.AddSeconds(duration);
        //return datetime.AddMinutes(duration);
    }

    private void UpdateEnergyTimer()
    {
        if (currentEnergy >= maxEnergy)
        {
            energyTimerText.text = "Full";
            return;
        }

        TimeSpan time = nextEnergyTime - DateTime.Now;
        string timeValue = String.Format("{0:D2}:{1:D1}", time.Minutes, time.Seconds);
        energyTimerText.text = timeValue;
    }

    private void UpdateEnergy()
    {
        energyText.text = currentEnergy.ToString() + "/" + maxEnergy.ToString();

        energyBar.maxValue = maxEnergy;
        energyBar.value = currentEnergy;
    }

    private DateTime StringToDate(string datetime)
    {
        if (String.IsNullOrEmpty(datetime))
        {
            return DateTime.Now;
        }

        else
        {
            return DateTime.Parse(datetime);
        }
    }

    private void Load()
    {
        currentEnergy = PlayerPrefs.GetInt("currentEnergy");
        nextEnergyTime = StringToDate(PlayerPrefs.GetString("nextEnergyTime"));
        lastEnergyTime = StringToDate(PlayerPrefs.GetString("lastEnergyTime"));
    }

    private void Save()
    {
        PlayerPrefs.SetInt("currentEnergy", currentEnergy);
        PlayerPrefs.SetString("nextEnergyTime", nextEnergyTime.ToString());
        PlayerPrefs.SetString("lastEnergyTime", lastEnergyTime.ToString());
    }
}
