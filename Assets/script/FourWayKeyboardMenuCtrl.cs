using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourWayKeyboardMenuCtrl : MonoBehaviour
{
    public List<FourWayKeyboardMenuPanel> _menu;
    int nowIndex = 0;
    

    public void ResetMenu()
    {
        nowIndex = 0;
        foreach(FourWayKeyboardMenuPanel _panel in _menu)
        {
            _panel.myCursor.SetActive(false);
        }
        _menu[nowIndex].myCursor.SetActive(true);
    }
    public void MoveMenu(int _dir)
    {
        if(nowIndex+_dir >= 0 && nowIndex+ _dir < _menu.Count)
        {
            _menu[nowIndex].myCursor.SetActive(false);
            nowIndex += _dir;
            _menu[nowIndex].myCursor.SetActive(true);
        }
        else
        {
            return;
        }

    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

            _menu[nowIndex].SelectTrigger();
            return;

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveMenu(+1);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveMenu(+2);

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveMenu(-1);

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveMenu(-2);
        }

    }


}
