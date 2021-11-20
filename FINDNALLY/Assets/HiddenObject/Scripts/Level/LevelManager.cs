using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private int currentLevel;                                 //What is this level
    [SerializeField] private float timeLimit = 0;                       //เวลานับถอยหลัง
    [SerializeField] private int maxHiddenObjectToFound = 6;            //maximum hidden objects ในแต่ละ level
    [SerializeField] private ObjectHolder objectHolderPrefab;           //Prefabลิสต์ของทั้งหมด
    [SerializeField] private GameObject reduceTimePrefab;
    [SerializeField] private GameObject ringEffectPrefab;
    [SerializeField] private GameObject addTime;
    public Transform foundEffect;
    public Transform hintEffect;
    public Transform confettiEffect;

    private UIManager button;
    public Case_Unlock caseUnlock;
    public Collection_Unlock collectionUnlock;
    public GameObject[] stars;
    private LevelSelection levelSelection;

    //Coin
    public Text coinText;
    public int rewardCoins = 0;
    public int totalCoin = 0;
    
    //Item
    public Text hintText;
    public Text addTimeText;
    public int rewardHint = 0;
    public int rewardAddTime = 0;
    public int totalHint = 0;
    public int totalAddTime = 0;


    [HideInInspector] public GameStatus gameStatus = GameStatus.NEXT;   //keep track of Game Status
    private List<HiddenObjectData> activeHiddenObjectList;              //ลิสต์ไอเทมที่ต้องหา
    public float currentTime;                                          //เวลาที่เหลือ
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
        activeHiddenObjectList = new List<HiddenObjectData>();          
        AssignHiddenObjects();
    }

    void AssignHiddenObjects()  //Method กำหนดไอเทมที่ต้องหา
    {
        ObjectHolder objectHolder = Instantiate(objectHolderPrefab, new Vector3(0,0,1), Quaternion.identity);
        objectHolderPrefab.gameObject.SetActive(true);
        totalHiddenObjectsFound = 0;                                        
        activeHiddenObjectList.Clear();                                    
        gameStatus = GameStatus.PLAYING;                                    
        UIManager.instance.TimerText.text = "" + timeLimit;                 
        currentTime = timeLimit;                                            

        for (int i = 0; i < objectHolder.HiddenObjectList.Count; i++)       
        {
            //deacivate all collider
            objectHolder.HiddenObjectList[i].hiddenObj.GetComponent<Collider2D>().enabled = false; 
        }

        int k = 0; 
        while (k < maxHiddenObjectToFound)
        {
            int randomNo = UnityEngine.Random.Range(0, objectHolder.HiddenObjectList.Count);
            if (!objectHolder.HiddenObjectList[randomNo].makeHidden)
            {
                objectHolder.HiddenObjectList[randomNo].hiddenObj.name = "" + k;    //set their name to index because we use index to identify tapped object

                objectHolder.HiddenObjectList[randomNo].makeHidden = true;          //เซทไอเทมที่ต้องหา
                                                                                    
                objectHolder.HiddenObjectList[randomNo].hiddenObj.GetComponent<Collider2D>().enabled = true;   //activate collider
                activeHiddenObjectList.Add(objectHolder.HiddenObjectList[randomNo]);   //เพิ่มลิสต์ไอเทมที่ต้องหา
                k++;                                                                
            }
        }

        UIManager.instance.PopulateHiddenObjectIcons(activeHiddenObjectList);   
        gameStatus = GameStatus.PLAYING;                                        
    }

    private void Update()
    {

        ShowItemAmount();
        
        if (gameStatus == GameStatus.PLAYING)                               
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
                                        
                        UIManager.instance.CheckSelectedHiddenObject(hit.collider.gameObject.name); //send the name of hit object to UIManager
                    
                        for (int i = 0; i < activeHiddenObjectList.Count; i++)
                        {
                            if (activeHiddenObjectList[i].hiddenObj.name == hit.collider.gameObject.name)
                            {
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
                        
                        totalHiddenObjectsFound++;                              
                        
                        if (totalHiddenObjectsFound >= maxHiddenObjectToFound)  
                        {
                            //SFXwin
                            if (SfxManager.sfxInstance.musicToggle == true)
                            {
                                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.winSound);
                            }
                            
                            Debug.Log("Level Complete");
                            Debug.Log("Time left : " + currentTime);
                            StarsAcheived();
                            caseUnlock.WinLevel();
                            //Reward coins
                            RewardCoins();

                            Instantiate(confettiEffect, new Vector3(0,-2,-1f),confettiEffect.rotation);
                            UIManager.instance.GameCompleteObj.SetActive(true);

                            if (GameObject.FindGameObjectWithTag("SpecialLevel"))
                            {
                                StarForSpecial();
                                RewardItem();
                                collectionUnlock.DttUnlock();
                            }
                            
                            gameStatus = GameStatus.NEXT;
                        }
                    }
                    
                    else
                    {
                        //SFXwrong
                        if (SfxManager.sfxInstance.musicToggle == true)
                        {
                            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.wrongSound);
                        }
                        
                        currentTime -= 5;        //กดมั่วเวลาลด 5 วิ
                        GameObject clone = (GameObject)Instantiate(reduceTimePrefab, new Vector3(pos.x, pos.y, 0f), Quaternion.identity);
                        Destroy(clone,1.0f);
                                        
                    }
                }
                 
            }

            currentTime -= Time.deltaTime;  

            time = TimeSpan.FromSeconds(currentTime);                       
            UIManager.instance.TimerText.text = time.ToString("mm':'ss");   
            if (currentTime <= 0)                                           
            {
                //SFXlose
                if (SfxManager.sfxInstance.musicToggle == true)
                {
                    SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.loseSound);
                }
                
                Debug.Log("Time Up");                                       
                UIManager.instance.TimeUpObj.SetActive(true);         
                gameStatus = GameStatus.NEXT;                               
            }
        }
    }

    public IEnumerator HintObject() 
    {
        //SFXhint
        if (SfxManager.sfxInstance.musicToggle == true)
        {
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.hintSound);
        }
        
        int randomValue = UnityEngine.Random.Range(0, activeHiddenObjectList.Count);
        Vector3 originalScale = activeHiddenObjectList[randomValue].hiddenObj.transform.localScale;
        GameObject ringEffect = (GameObject)Instantiate(ringEffectPrefab, activeHiddenObjectList[randomValue].hiddenObj.transform.position, hintEffect.rotation);
        activeHiddenObjectList[randomValue].hiddenObj.transform.localScale = originalScale * 1.5f;
        Destroy(ringEffect, 4f);
        
        yield return new WaitForSeconds(3f);
        activeHiddenObjectList[randomValue].hiddenObj.transform.localScale = originalScale;
    }

    public IEnumerator AddTimeObject()
    {
        //SFXaddtime
        if (SfxManager.sfxInstance.musicToggle == true)
        {
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.addTimeSound);
        }
        
        currentTime += 5;
        Vector3 originalScale = addTime.transform.localScale;
        addTime.SetActive(true);
        addTime.transform.localScale = originalScale;
        addTime.transform.localScale = originalScale * 2f;
        yield return new WaitForSeconds(0.5f);
        addTime.transform.localScale = originalScale;
        addTime.SetActive(false);
    }

    public void RewardCoins()
    {
        totalCoin += rewardCoins;
        PlayerPrefs.SetInt("Coin", totalCoin);
        coinText.text = rewardCoins.ToString();
    }

    public void RewardItem()
    {
        totalAddTime = PlayerPrefs.GetInt("AddTime");
        totalHint = PlayerPrefs.GetInt("Hint");
        
        Debug.Log("AddTime : " + PlayerPrefs.GetInt("AddTime"));
        Debug.Log("Hint : " + PlayerPrefs.GetInt("Hint"));

        totalHint += rewardHint;
        PlayerPrefs.SetInt("Hint", totalHint);
        hintText.text = rewardHint.ToString();
        
        totalAddTime += rewardAddTime;
        PlayerPrefs.SetInt("AddTime", totalAddTime);
        addTimeText.text = rewardAddTime.ToString();
    }

    public void ShowItemAmount()
    {
        int currentHint = PlayerPrefs.GetInt("Hint");
        hintText.text = currentHint.ToString();
        
        int currentAddTime = PlayerPrefs.GetInt("AddTime");
        addTimeText.text = currentAddTime.ToString();
    }

    public void StarsAcheived()
    {
        Debug.Log("StarsAcheived");
        
        float rate = currentTime;
        if (rate >= 80)
        {
            //three stars
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
            
            SetStar(currentLevel,3);
            Debug.Log("Level: " + currentLevel + " Got"+GetStar(currentLevel)+"Stars");
        }
        else if (rate >= 40 && rate <= 79)
        {
            //two stars
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            
            SetStar(currentLevel,2);
            Debug.Log("Level: " + currentLevel + " Got"+GetStar(currentLevel)+"Stars");
        }
        else if(rate <= 39 && rate !=0)
        {
            //one star
            stars[0].SetActive(true);
            
            SetStar(currentLevel,1);
            Debug.Log("Level: " + currentLevel + " Got"+GetStar(currentLevel)+"Star");
        }
        else if(rate == 0)
        {
            SetStar(currentLevel,0);
            Debug.Log("Level: " + currentLevel + " Got"+GetStar(currentLevel)+"Star"); 
        }
    }

    public void StarForSpecial()
    {
        Debug.Log("StarsAcheived");

        //three stars
        stars[0].SetActive(true);
        stars[1].SetActive(true);
        stars[2].SetActive(true);
            
        SetStar(currentLevel,3);
        Debug.Log("Level: " + currentLevel + " Got"+GetStar(currentLevel)+"Stars");
        
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

[System.Serializable]
public class HiddenObjectData
{
    public string name;
    public GameObject hiddenObj;
    public bool makeHidden = false;
}

public enum GameStatus
{
    PLAYING,
    NEXT
}
