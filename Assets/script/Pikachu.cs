using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pikachu : PoketmonType
{
    public PokemonNature pokemonNature;
    public string nature;
    public bool isRandom;


    public float settingSTR;


    private void Start() {
        if(isRandom) RandomPokeMon();
        else SettingPokemon();



    }

    public void SettingPokemon(){
        STR = settingSTR;


    }


    public void RandomPokeMon()
    {
        if (LEVEL == 0)
        {
            LEVEL = Random.Range(0, 99);
            HP = Random.Range(10, 99);
            MAXHP = HP;
            SPD = Random.Range(10, 99);
            STR = Random.Range(10, 99);
            SPECIAL = Random.Range(10, 99);
            PROTECT = Random.Range(10, 99);
            NatureEntity _nature = pokemonNature.SetNature();
            nature = _nature.natureKorName;
            Debug.Log(_nature.natureKorName);
            if (_nature.decreseTarget == "공격") STR*=0.9f;
            if (_nature.decreseTarget == "방어") PROTECT*=0.9f;
            if (_nature.decreseTarget == "특수") SPECIAL*=0.9f;
            if (_nature.decreseTarget == "스피드") SPD*=0.9f;
            if (_nature.increseTarget == "공격") STR*=1.1f;
            if (_nature.increseTarget == "방어") PROTECT*=1.1f;
            if (_nature.increseTarget == "특수") SPECIAL*=1.1f;
            if (_nature.increseTarget == "스피드") SPD*=1.1f;
        }
        Debug.Log(GetData());
    }
    


    

}
