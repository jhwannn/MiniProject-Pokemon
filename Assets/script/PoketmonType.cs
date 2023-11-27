﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoketmonType : MonoBehaviour
{
    
    public string nameKor;
    public string nameEng;
    public int LEVEL = 0;
    public float HP;
    public float MAXHP;
    public float STR;
    public float SPD;
    public float SPECIAL;
    public float PROTECT;
    public int EXP;
    public int MAXEXP;
    public Sprite myCharImg_Front;
    public Sprite myCharImg_Back;
    public List<SkillType> skillList = new List<SkillType>();


    public PoketmonType myNextMob;







    public void RandomPokeMon()
    {

        LEVEL = Random.Range(1, 10);
        HP = Random.Range(10, 99) + 50;
        MAXHP = HP;
        SPD = Random.Range(10, 99);
        EXP = 0;
        MAXEXP = 200;
        STR = Random.Range(10, 99);
        SPECIAL = Random.Range(10, 99);
        PROTECT = Random.Range(10, 99);
        Debug.Log(GetData());
    }


    public virtual string GetData()
    {
        return nameKor + " : " + nameEng + " HP : " + HP + " LV : " + LEVEL;
    }

    public virtual void Revolution()
    {
        nameKor = myNextMob.nameKor;
        nameEng = myNextMob.nameEng;
        myCharImg_Front = myNextMob.myCharImg_Front;
        myCharImg_Back = myNextMob.myCharImg_Back;

    }





}
