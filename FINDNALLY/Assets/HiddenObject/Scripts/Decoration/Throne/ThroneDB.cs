using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ThroneDB : ScriptableObject
{
    public Throne[] throne;

    public int ThroneCount
    {
        get
        {
            return throne.Length;
        }
    }

    public Throne GetThrone(int index)
    {
        return throne[index];
    }
}
