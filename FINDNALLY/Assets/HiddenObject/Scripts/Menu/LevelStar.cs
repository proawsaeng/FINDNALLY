using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStar : MonoBehaviour
{
    public Sprite starSprite0;
    public Sprite starSprite1;
    public Sprite starSprite2;
    public Sprite starSprite3;
    public Image image;

    public void SetStarSprite(int starAmount)
    {
        switch (starAmount)
        {
            case 0:
                image.sprite = starSprite0;
                break;
            
            case 1:
                image.sprite = starSprite1;
                break;
            
            case 2:
                image.sprite = starSprite2;
                break;
            
            case 3:
                image.sprite = starSprite3;
                break;
        }
    }
}
