using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonZone : MonoBehaviour
{
    public List<PoketmonType> pokemons;
    private PokemonBattleCtrl battleCtrl;
    private PanelManager battleCtrlPnm;
    public Text DiagText;
    public Text NameText;
    public Image EnemyImg;


    public int percent;


    private void Start()
    {
        battleCtrl = GameObject.Find("BattleCtrl").GetComponent<PokemonBattleCtrl>();
        battleCtrlPnm = GameObject.Find("BattleCtrl").GetComponent<PanelManager>();
    }

    /*    private void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.tag =="Player")
            {

            }

        }*/



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("충돌");
            int _per = Random.Range(1, 101);
            if(_per <= percent)
            {
                //포켓몬 등장!
                int _rdm = Random.Range(0, pokemons.Count);
                battleCtrlPnm.GUIToggle(true);
                DiagText.text = "야생의 " + pokemons[_rdm].nameKor + "이(가) 나타났다!";
                EnemyImg.sprite = pokemons[_rdm].myCharImg_Front;
                NameText.text = pokemons[_rdm].nameKor;


            }


        }
        
    }

}
