using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMenuCtrl : MonoBehaviour
{
    public List<KeyboardMenuPanel> _panel;


    public int nowSel = 0;

    public GameObject TargetObj;
    public GameObject _checkObjTemp;





    public void OpenSet()
    {

        
        if(_panel.Count-1< nowSel) nowSel = 0;
        AllDeSel();
        _panel[nowSel].SetStatus(true);
    }
    private void AllDeSel()
    {
        foreach (KeyboardMenuPanel _menu in _panel) _menu.SetStatus(false);
    }

    private void Update()
    {
        if (gameObject.name == "BAGSystem")
        {
            if(TargetObj.active == true)
            {
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (nowSel + 1 < _panel.Count)
            {

                nowSel++;
                AllDeSel();
                _panel[nowSel].SetStatus(true);
                _panel[nowSel].ChooseTriggerAction();

            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (nowSel - 1 >= 0)
            {
                nowSel--;
                AllDeSel();
                _panel[nowSel].SetStatus(true);
                _panel[nowSel].ChooseTriggerAction();


            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SoundCtrl.PlaySound("button");
            _panel[nowSel].SelectTrigger();
        }

    }

}