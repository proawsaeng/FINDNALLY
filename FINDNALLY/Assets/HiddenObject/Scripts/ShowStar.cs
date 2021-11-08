using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStar : MonoBehaviour
{
    [SerializeField] private int level;
    
    [SerializeField] public Button levelButton;

    public GameObject levelStarPrefab;

    private LevelStar levelStar;

    void Start()
    {
        //if (levelButton.interactable == true)
        //{
            Debug.Log("Level: " + level + "Got"+GetStar(level)+"Stars");
            levelStar = Instantiate(levelStarPrefab, levelButton.transform).GetComponent<LevelStar>();
            GetStar(level);

            int star = GetStar(level);
            //star = Mathf.Clamp(star + 1, 0, 3);
            //SetStar(i,star);
            levelStar.SetStarSprite(star);
        //}
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
        //"Level_1_Star"
        return "Level_" + level + "_Star";
    }
}
