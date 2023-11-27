using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryCtrl : MonoBehaviour
{
    public List<ItemCtrl> InvList = new List<ItemCtrl>();

    public Dictionary<ItemCtrl, int> InvDic = new Dictionary<ItemCtrl, int>();

    public GameObject _targetInv;

    public GameObject itemPrefab;

    public KeyboardMenuCtrl keyboardCtrl;
    public UnityEvent BallTrigger;
    public UnityEvent NoBallTrigger;
    public PanelManager BallMenu;

    public void SetDisable()
    {
        BallMenu.GUIToggle(false);

    }


    public void Selectball()
    {
        if (GameObject.Find("BattleProcess").GetComponent<BattleProcess>().nowBattle)
        {
            BallMenu.GUIToggle(true);
        }

    }
    public void DeSelectBall()
    {
        BallMenu.GUIToggle(false);
    }

    private void Start()
    {
        LoadInv();
    }


    public void LoadInv()
    {
        InvDic = null;
        InvDic = new Dictionary<ItemCtrl, int>();
        foreach (ItemCtrl _item in InvList)
        {
            if (!InvDic.ContainsKey(_item))
            {
                InvDic.Add(_item, 1);
            }
            else
            {
                InvDic[_item] += 1;
            }

        }
        ViewInv();
    }
    public void ViewInv()
    {
        foreach (KeyValuePair<ItemCtrl, int> _item in InvDic)
        {
            //Console.WriteLine("Key: {0}, Value: {1}", kv.Key, kv.Value);
            GameObject _chatsLine = Instantiate(itemPrefab);
            _chatsLine.transform.SetParent(_targetInv.transform);
            _chatsLine.GetComponent<ItemPanelCtrl>().SetData(_item.Key.ItemNameKR, _item.Value, _item.Key);
            keyboardCtrl._panel.Add(_chatsLine.GetComponent<KeyboardMenuPanel>());
            _chatsLine.GetComponent<KeyboardMenuPanel>().ClickTrigger = BallTrigger;

        }


    }






}
