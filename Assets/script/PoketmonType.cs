using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoketmonType : MonoBehaviour
{
    
    public string nameKor;
    public string nameEng;
    public int LEVEL = 0;
    public int HP;
    public float STR;
    public float SPD;
    public float SPECIAL;
    public float PROTECT;
    public Sprite myCharImg_Front;
    public Sprite myCharImg_Back;
    public List<SkillType> skillList = new List<SkillType>();


    public PoketmonType myNextMob;







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
