using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelCtrl : MonoBehaviour
{
    public Text NameObj;
    public ItemCtrl myItem;
    public int count;
    
    public void SetData(string _name, int _count, ItemCtrl _myItem)
    {
        NameObj.text = _name;
        myItem = _myItem;
        count = _count;
    }
    
    public void ShowCount()
    {
        Debug.Log("½ÇÇà");
        GameObject.Find("CountText").GetComponent<Text>().text = "X " + count;
    }
    
}
