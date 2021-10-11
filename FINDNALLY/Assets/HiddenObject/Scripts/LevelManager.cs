using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private float timeLimit = 0;                       //เวลานับถอยหลัง
    [SerializeField] private int maxHiddenObjectToFound = 6;            //maximum hidden objects ในแต่ละ level
    [SerializeField] private ObjectHolder objectHolderPrefab;           //Prefabลิสต์ของทั้งหมด
    [SerializeField] private GameObject reduceTimePrefab;
    [SerializeField] private GameObject addTime;
   
    private UIManager button;
    public int levelToUnlock = 2;
    public Case_Unlock caseUnlock;
    public Collection_Unlock collectionUnlock;

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
    private float currentTime;                                          //เวลาที่เหลือ
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
        totalHint = PlayerPrefs.GetInt("Hint");
        totalAddTime = PlayerPrefs.GetInt("AddTime");
        activeHiddenObjectList = new List<HiddenObjectData>();          
        AssignHiddenObjects();                                  
    }

    void AssignHiddenObjects()  //Method กำหนดไอเทมที่ต้องหา
    {
        ObjectHolder objectHolder = Instantiate(objectHolderPrefab, Vector3.zero, Quaternion.identity);
        objectHolderPrefab.gameObject.SetActive(false);
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
                                activeHiddenObjectList.RemoveAt(i);
                                break;
                            }
                        }
                        
                        totalHiddenObjectsFound++;                              
                        
                        if (totalHiddenObjectsFound >= maxHiddenObjectToFound)  
                        {
                            Debug.Log("Level Complete");
                            //Reward coins
                            RewardCoins();

                            UIManager.instance.GameCompleteObj.SetActive(true);

                            if (GameObject.FindGameObjectWithTag("SpecialLevel"))
                            {
                                RewardItem();
                                collectionUnlock.DttUnlock();
                            }
                            
                            caseUnlock.WinLevel();
                            gameStatus = GameStatus.NEXT;
                        }
                    }
                    
                    else
                    {
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
                Debug.Log("Time Up");                                       
                UIManager.instance.TimeUpObj.SetActive(true);         
                gameStatus = GameStatus.NEXT;                               
            }
        }
    }

    public IEnumerator HintObject() 
    {
        int randomValue = UnityEngine.Random.Range(0, activeHiddenObjectList.Count);
        Vector3 originalScale = activeHiddenObjectList[randomValue].hiddenObj.transform.localScale;
        activeHiddenObjectList[randomValue].hiddenObj.transform.localScale = originalScale * 1.25f;
        yield return new WaitForSeconds(0.5f);
        activeHiddenObjectList[randomValue].hiddenObj.transform.localScale = originalScale;
    }

    public IEnumerator AddTimeObject()
    {
        currentTime += 5;
        addTime.SetActive(true);
        yield return new WaitForSeconds(0.5f);
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
