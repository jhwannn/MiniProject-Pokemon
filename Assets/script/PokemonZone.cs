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
    public Text TrainerNameText;
    public Image EnemyImg;
    public Image PlayerPokemonImg;
    public Animator trainerCtrl;
    public Animator trainerPokemonCtrl;
    public PlayerPokemon playerPokemonCtrl;
    public GameObject skillRow;
    public GameObject skillGroup;
    public GameObject SkillMenu;
    public BattleProcess battleProcess;
    public FourWayKeyboardMenuCtrl battleMenuCtrl;
    public GameObject battleMenuObj;

    public int percent;


    private void Start()
    {
        battleCtrl = GameObject.Find("BattleCtrl").GetComponent<PokemonBattleCtrl>();
        battleCtrlPnm = GameObject.Find("BattleCtrl").GetComponent<PanelManager>();
        playerPokemonCtrl = GameObject.Find("PlayerPokemon").GetComponent<PlayerPokemon>();
        battleProcess = GameObject.Find("BattleProcess").GetComponent<BattleProcess>();

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
                TrainerNameText.text = playerPokemonCtrl.pokemon.nameKor;
                PlayerPokemonImg.sprite = playerPokemonCtrl.pokemon.myCharImg_Back;




                foreach (SkillType _skill in playerPokemonCtrl.pokemon.skillList)
                {
                    GameObject _temp = Instantiate(skillRow);
                    _temp.GetComponent<SkillRowCtrl>().SkillName.text = _skill.NameKR;
                    _temp.GetComponent<SkillRowCtrl>()._mySkill = _skill;
                    _temp.transform.SetParent(skillGroup.transform);
                    _temp.GetComponent<KeyboardMenuPanel>().ClickTrigger = _skill.SkillTrigger;
                    skillGroup.GetComponent<KeyboardMenuCtrl>()._panel.Add(_temp.GetComponent<KeyboardMenuPanel>());
                }
                GameObject.Find("BattleProcess").GetComponent<BattleProcess>().myPokemon = playerPokemonCtrl.pokemon;
                //skillGroup.GetComponent<KeyboardMenuCtrl>().OpenSet();
                //StartCoroutine(SkillView());
                battleMenuCtrl.ResetMenu();
                StartCoroutine(MenuView());
                StartCoroutine(TrainerMove());
            }
        }
        
    }

    public void ViewSkillMenu()
    {
        battleMenuObj.SetActive(false);
        skillGroup.GetComponent<KeyboardMenuCtrl>().OpenSet();
        SkillMenu.SetActive(true);
    }


    private IEnumerator TrainerMove()
    {
        yield return new WaitForSeconds(0.7f);
        trainerCtrl.SetTrigger("Move");
        trainerPokemonCtrl.SetTrigger("Move");
    }
    private IEnumerator MenuView()
    {
        yield return new WaitForSeconds(1f);
        battleMenuObj.SetActive(true);
    }

    private IEnumerator SkillView()
    {
        yield return new WaitForSeconds(1.5f);
        SkillMenu.SetActive(true);
    }

}
