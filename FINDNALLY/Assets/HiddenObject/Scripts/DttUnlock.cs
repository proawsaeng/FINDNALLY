using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DttUnlock : MonoBehaviour
{
    public Image original;
    public Sprite reveal;

    public void UnlockDtt()
    {
        Destroy(gameObject);
        original.sprite = reveal;
    }
}
