using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndlessLevelManager_ : MonoBehaviour
{
    public static EndlessLevelManager_ instance;

    [SerializeField] private int currentLevel;                                 //What is this level
    [SerializeField] private int maxHiddenObjectToFound = 0;            //maximum hidden objects ในแต่ละ level
    [SerializeField] private EndlessObjectHolder objectHolderPrefab;           //Prefabลิสต์ของทั้งหมด
    public Transform foundEffect;
    public Transform confettiEffect;

    private EndlessUiManager button;
    public EndlessCase_Unlock endlessCaseUnlock;
    public Collection_Unlock collectionUnlock;
    public GameObject[] stars;
    private LevelSelection levelSelection;

    //Coin
    public Text coinText;
    public int rewardCoins = 0;
    public int totalCoin = 0;


    [HideInInspector] public EndlessGameStatus gameStatus = EndlessGameStatus.NEXT;   //keep track of Game Status
    private List<EndlessHiddenObjectData> activeHiddenObjectList;              //ลิสต์ไอเทมที่ต้องหา
    private int totalHiddenObjectsFound = 0;                            //ไอเทมที่เจอ
    private TimeSpan time;                                              
    private RaycastHit2D hit;
    private Vector3 pos;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        totalCoin = PlayerPrefs.GetInt("Coin");
        activeHiddenObjectList = new List<EndlessHiddenObjectData>();          
        AssignHiddenObjects();
    }

    void AssignHiddenObjects()  //Method กำหนดไอเทมที่ต้องหา
    {
        EndlessObjectHolder endlessObjectHolder = Instantiate(objectHolderPrefab, new Vector3(0,0,1), Quaternion.identity);
        objectHolderPrefab.gameObject.SetActive(false);
        totalHiddenObjectsFound = 0;                                        
        activeHiddenObjectList.Clear();                                    
        gameStatus = EndlessGameStatus.PLAYING;

        for (int i = 0; i < endlessObjectHolder.EndlessHiddenObjectList.Count; i++)       
        {
            //deacivate all collider
            endlessObjectHolder.EndlessHiddenObjectList[i].hiddenObj.GetComponent<Collider2D>().enabled = false; 
        }

        int k = 0; 
        while (k < maxHiddenObjectToFound)
        {
            int randomNo = UnityEngine.Random.Range(0, endlessObjectHolder.EndlessHiddenObjectList.Count);
            if (!endlessObjectHolder.EndlessHiddenObjectList[randomNo].makeObjectHidden)
            {
                endlessObjectHolder.EndlessHiddenObjectList[randomNo].hiddenObj.name = "" + k;    //set their name to index because we use index to identify tapped object

                endlessObjectHolder.EndlessHiddenObjectList[randomNo].makeObjectHidden = true;          //เซทไอเทมที่ต้องหา
                                                                                    
                endlessObjectHolder.EndlessHiddenObjectList[randomNo].hiddenObj.GetComponent<Collider2D>().enabled = true;   //activate collider
                activeHiddenObjectList.Add(endlessObjectHolder.EndlessHiddenObjectList[randomNo]);   //เพิ่มลิสต์ไอเทมที่ต้องหา
                k++;                                                                
            }
        }

        EndlessUiManager.instance.PopulateHiddenObjectIcons(activeHiddenObjectList);   
        gameStatus = EndlessGameStatus.PLAYING;                                        
    }//AssignHiddenObjects

    private void Update()
    {
        if (gameStatus == EndlessGameStatus.PLAYING)                               
        {
            if (Input.GetMouseButtonDown(0))                                
            {
                pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  
                hit = Physics2D.Raycast(pos, Vector3.zero);

                if (!EventSystem.current.IsPointerOverGameObject())            //ไม่ให้ซ้อนกัน
                {
                    if (hit && hit.collider != null)                            
                    {
                        hit.collider.gameObject.SetActive(false);               //deactivate collider that clicked
                                        
                        EndlessUiManager.instance.CheckSelectedHiddenObject(hit.collider.gameObject.name); //send the name of hit object to EndlessUiManager
                    
                        for (int i = 0; i < activeHiddenObjectList.Count; i++)
                        {
                            if (activeHiddenObjectList[i].hiddenObj.name == hit.collider.gameObject.name)
                            {
                                totalHiddenObjectsFound++;
                                
                                //SFXfound
                                if (SfxManager.sfxInstance.musicToggle == true)
                                {
                                    SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.foundSound);
                                }
                                
                                //FoundEffect
                                Instantiate(foundEffect, hit.collider.gameObject.transform.position,foundEffect.rotation);

                                //Delay 1 sec before disappear
                                activeHiddenObjectList.RemoveAt(i);
                                break;
                            }
                        }
                        
                        //totalHiddenObjectsFound++;                              
                        
                        if (totalHiddenObjectsFound >= maxHiddenObjectToFound)  
                        {
                            //SFXwin
                            if (SfxManager.sfxInstance.musicToggle == true)
                            {
                                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.winSound);
                            }
                            
                            Debug.Log("Level Complete");
                            StarsAcheived();
                            endlessCaseUnlock.WinEndlessLevel();
                            //Reward coins
                            RewardCoins();

                            Instantiate(confettiEffect, new Vector3(0,-2,-1f),confettiEffect.rotation);
                            EndlessUiManager.instance.GameCompleteObj.SetActive(true);

                            gameStatus = EndlessGameStatus.NEXT;
                        }
                    }
                }
            }
        }
    }//Update

    public void RewardCoins()
    {
        totalCoin += rewardCoins;
        PlayerPrefs.SetInt("Coin", totalCoin);
        coinText.text = rewardCoins.ToString();
    }
    
    public void StarsAcheived()
    {
        Debug.Log("StarsAcheived");

        //three stars
        stars[0].SetActive(true);
        stars[1].SetActive(true);
        stars[2].SetActive(true);
            
        SetStar(currentLevel,3);
        Debug.Log("EndlessLevel: " + currentLevel + " Got"+GetStar(currentLevel)+"Stars");
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
    
    [System.Serializable]
    public class EndlessHiddenObjectData
    {
        public string name;
        public GameObject hiddenObj;
        public bool makeObjectHidden = false;
    }

    public enum EndlessGameStatus
    {
        PLAYING,
        NEXT
    }


}
