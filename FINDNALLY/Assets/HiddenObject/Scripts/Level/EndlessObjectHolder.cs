using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndlessObjectHolder : MonoBehaviour
{
    [SerializeField] private List<EndlessLevelManager_.EndlessHiddenObjectData> endlessHiddenObjectList;    //list of all the hiddenObjects available in the scene


    public List<EndlessLevelManager_.EndlessHiddenObjectData> EndlessHiddenObjectList
    {
        get { return endlessHiddenObjectList; }
    }
    
    
    public void EndlessArrangeList()
    {
        endlessHiddenObjectList = new List<EndlessLevelManager_.EndlessHiddenObjectData>();

        for (int i = 0; i < transform.childCount; i++)
        {
            EndlessLevelManager_.EndlessHiddenObjectData endlessHiddenObjectData = new EndlessLevelManager_.EndlessHiddenObjectData();
            endlessHiddenObjectData.hiddenObj = transform.GetChild(i).gameObject;
            endlessHiddenObjectData.name = transform.GetChild(i).name;
            endlessHiddenObjectData.makeObjectHidden = false;

            endlessHiddenObjectList.Add(endlessHiddenObjectData);
        }
    }

}
