using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBallCtrl : MonoBehaviour
{
    public bool isBall;
    public GameObject useMenu;
    
    

    public void CheckSelect()
    {
        if (GameObject.Find("BattleProcess").GetComponent<BattleProcess>().nowBattle)
        {

        }

    }
}
