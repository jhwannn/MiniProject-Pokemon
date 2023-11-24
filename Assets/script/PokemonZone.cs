using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonZone : MonoBehaviour
{
    public List<PoketmonType> pokemons;
    private PokemonBattleCtrl battleCtrl;
    public PanelManager battleCtrlPnm;
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
    public GameObject EnemyEffectGroup;
    public GameObject TrainerEffectGroup;

    public float num;
    

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



    public void ResetPokemon(bool isChange)
    {

        TrainerNameText.text = playerPokemonCtrl.pokemon.nameKor;
        
        foreach (Transform child in skillGroup.transform)
        {
            Destroy(child.gameObject);
        }
        skillGroup.GetComponent<KeyboardMenuCtrl>()._panel.Clear();
        GameObject.Find("BattleProcess").GetComponent<BattleProcess>().myPokemon = playerPokemonCtrl.pokemon;
        GameObject.Find("BattleProcess").GetComponent<BattleProcess>().ResetHP();
        foreach (SkillType _skill in playerPokemonCtrl.pokemon.skillList)
        {
            GameObject _temp = Instantiate(skillRow);
            _temp.GetComponent<SkillRowCtrl>().SkillName.text = _skill.NameKR;
            _temp.GetComponent<SkillRowCtrl>()._mySkill = _skill;
            _temp.transform.SetParent(skillGroup.transform);
            _temp.GetComponent<KeyboardMenuPanel>().ClickTrigger = _skill.SkillTrigger;
            skillGroup.GetComponent<KeyboardMenuCtrl>()._panel.Add(_temp.GetComponent<KeyboardMenuPanel>());
        }
        battleMenuCtrl.ResetMenu();
        StartCoroutine(MenuView());
        if (isChange)
        {
            StartCoroutine(ChangeImgWait());
            trainerPokemonCtrl.SetTrigger("ChangePokemon");
        }
        else
        {
            PlayerPokemonImg.sprite = playerPokemonCtrl.pokemon.myCharImg_Back;
            trainerPokemonCtrl.SetTrigger("Move");
        }

    }
    public IEnumerator ChangeImgWait()
    {
        yield return new WaitForSeconds(0.15f);
        PlayerPokemonImg.sprite = playerPokemonCtrl.pokemon.myCharImg_Back;
    }


    public void CallView()
    {
        battleMenuCtrl.ResetMenu();
        StartCoroutine(MenuView());
        StartCoroutine(TrainerMove());
    }
    
    public void CheckZone(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            int i = 0;
            foreach (PoketmonType _pkm in playerPokemonCtrl.pokemonList)
            {
                if (_pkm.HP > 0)
                {
                    i++;
                }
            }
            if (i == 0) return;

            Debug.Log("충돌");
            int _per = Random.Range(1, 101);
            if (_per <= percent)
            {
                //포켓몬 등장!
                int _rdm = Random.Range(0, pokemons.Count);
                pokemons[_rdm].RandomPokeMon();
                battleCtrlPnm.GUIToggle(true);
                DiagText.text = "야생의 " + pokemons[_rdm].nameKor + "이(가) 나타났다!";
                EnemyImg.sprite = pokemons[_rdm].myCharImg_Front;
                NameText.text = pokemons[_rdm].nameKor;
                ResetPokemon(false);


                GameObject.Find("BattleProcess").GetComponent<BattleProcess>().currentZone = gameObject.GetComponent<PokemonZone>();
                GameObject.Find("BattleProcess").GetComponent<BattleProcess>().myPokemon = playerPokemonCtrl.pokemon;
                GameObject.Find("BattleProcess").GetComponent<BattleProcess>().ResetHP();
                GameObject.Find("BattleProcess").GetComponent<BattleProcess>().nowBattle = true;
                GameObject.Find("BattleProcess").GetComponent<BattleProcess>().enemyPokemon = pokemons[_rdm];

                //skillGroup.GetComponent<KeyboardMenuCtrl>().OpenSet();
                //StartCoroutine(SkillView());
                CallView();
            }
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        num += Time.deltaTime;
        if(num >= 1)
        {
            num = 0;
            CheckZone(collision);
            return;
        }
    }

    public void ViewSkillMenu()
    {
        Debug.Log("1");
        battleMenuObj.SetActive(false);
        Debug.Log("2");
        skillGroup.GetComponent<KeyboardMenuCtrl>().OpenSet();
        Debug.Log("3");

        SkillMenu.SetActive(true);
    }


    private IEnumerator TrainerMove()
    {
        yield return new WaitForSeconds(0.0f);
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
