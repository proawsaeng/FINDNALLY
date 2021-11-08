using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TweenText : MonoBehaviour
{
    public float tweenTime;
    
    void Start()
    {
        Tween();
    }

    public void Tween()
    {
        LeanTween.cancel(gameObject);
        
        transform.localScale = Vector3.one;

        LeanTween.scale(gameObject, Vector3.one * 0.5f, tweenTime).setEasePunch().setLoopCount(10);

        //LeanTween.value(gameObject, 0, 1, tweenTime).setEasePunch().setOnUpdate(setText);
    }
}
