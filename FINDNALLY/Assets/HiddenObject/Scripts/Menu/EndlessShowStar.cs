using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessShowStar : MonoBehaviour
{
    [SerializeField] private int endlessLevel;

    [SerializeField] public Button levelButton;

    public GameObject levelStarPrefab;

    private LevelStar levelStar;

    void Start()
    {
        //Debug.Log("Level: " + level + "Got"+GetStar(level)+"Stars");
        levelStar = Instantiate(levelStarPrefab, levelButton.transform).GetComponent<LevelStar>();

        GetStar(endlessLevel);
        int star = GetStar(endlessLevel);
        levelStar.SetStarSprite(star);
    }


    public void SetStar(int level, int starAmount)
    {
        PlayerPrefs.SetInt(GetKey(level), starAmount);
    }

    public int GetStar(int level)
    {
        return PlayerPrefs.GetInt(GetKey(level));
    }

    public string GetKey(int level)
    {
        //ไว้ตั้งชื่อ PlayerPrefs
        //"EndlessLevel_1_Star"
        return "EndlessLevel_" + level + "_Star";
    }
}