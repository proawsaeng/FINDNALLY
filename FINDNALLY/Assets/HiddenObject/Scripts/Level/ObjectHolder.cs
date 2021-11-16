using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectHolder : MonoBehaviour
{
    [SerializeField] private List<HiddenObjectData> hiddenObjectList;   //list of all the hiddenObjects available in the scene
    [SerializeField] private List<EndlessHiddenObjectData> endlessHiddenObjectList;

    public List<HiddenObjectData> HiddenObjectList
    {
        get { return hiddenObjectList; }
    }

    public List<EndlessHiddenObjectData> EndlessHiddenObjectList
    {
        get { return endlessHiddenObjectList; }
    }

    public void ArrangeList()
    {
        hiddenObjectList = new List<HiddenObjectData>();

        for (int i = 0; i < transform.childCount; i++)
        {
            HiddenObjectData hiddenObjectData = new HiddenObjectData();
            hiddenObjectData.hiddenObj = transform.GetChild(i).gameObject;
            hiddenObjectData.name = transform.GetChild(i).name;
            hiddenObjectData.makeHidden = false;

            hiddenObjectList.Add(hiddenObjectData);
        }
    }
    
    public void EndlessArrangeList()
    {
        endlessHiddenObjectList = new List<EndlessHiddenObjectData>();

        for (int i = 0; i < transform.childCount; i++)
        {
            EndlessHiddenObjectData endlessHiddenObjectData = new EndlessHiddenObjectData();
            endlessHiddenObjectData.hiddenObj = transform.GetChild(i).gameObject;
            endlessHiddenObjectData.name = transform.GetChild(i).name;
            endlessHiddenObjectData.makeHidden = false;

            endlessHiddenObjectList.Add(endlessHiddenObjectData);
        }
    }

}