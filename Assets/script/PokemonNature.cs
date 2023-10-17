using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonNature : MonoBehaviour
{
    string[,] natureKorList = {
        {"노력", "외로움", "고집", "용감"},
        {"대담","온순","장난","무사태평"},
        {"조심","의젓","수줍음","냉정"},

        {"겁쟁이","성급","명랑","성실"}};
    string[] natureType = {"공격", "방어", "특수", "스피드"}; 

    // private void Start() {
    //     NatureEntity _temp = SetNature();
    //     Debug.Log("상승 : " + _temp.increseTarget);
    //     Debug.Log("감소 : " + _temp.decreseTarget);
    //     Debug.Log("성격 : " + _temp.natureKorName);
    // }

    public NatureEntity SetNature(){
        int _rdmNumRow = Random.Range(0, natureType.Length);
        int _rdmNumColumn = Random.Range(0, natureType.Length);
        NatureEntity _nature = new NatureEntity();
        _nature.increseTarget = natureType[_rdmNumRow];
        _nature.decreseTarget = natureType[_rdmNumColumn];
        _nature.natureKorName = natureKorList[_rdmNumRow,_rdmNumColumn];

        return _nature;

    }



}
