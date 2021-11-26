using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCheat : MonoBehaviour
{
    public static EnergyCheat energyCheatInstance;

    public bool energyCheatToggle = true;
    
    private void Awake()
    {
        if (energyCheatInstance == null)
        {
            energyCheatInstance = this;
            DontDestroyOnLoad(energyCheatInstance);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
