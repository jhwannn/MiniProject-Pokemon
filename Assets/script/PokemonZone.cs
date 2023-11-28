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
    public GameObject EnemyPosCtrl;
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
    public Image trainerExpBar;
    public Animator enemyLeaderAnim;
    public Animator enemyPokemonAnim;
    public bool isNpcBattle;
    public bool isBattleStart = false;
    public int battleCursor = 0;
    Vector3 currentTrainerPokemonPos;
    public string NPCNAME;
    public bool isEnter;
    public NPCCtrl myNPC;
    

    public float num;
    

    public int percent;




    private void Start()
    {
        currentTrainerPokemonPos = PlayerPokemonImg.gameObject.transform.position;
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



    public void ResetPokemon(bool isChange, bool isCall)
    {

        TrainerNameText.text = playerPokemonCtrl.pokemon.nameKor+":L"+ playerPokemonCtrl.pokemon.LEVEL;
        
        foreach (Transform child in skillGroup.transform)
        {
            Destroy(child.gameObject);
        }
        skillGroup.GetComponent<KeyboardMenuCtrl>()._panel.Clear();
        GameObject.Find("BattleProcess").GetComponent<BattleProcess>().myPokemon = playerPokemonCtrl.pokemon;
        GameObject.Find("BattleProcess").GetComponent<BattleProcess>().ResetHP();
        trainerExpBar.fillAmount = playerPokemonCtrl.pokemon.EXP / playerPokemonCtrl.pokemon.MAXEXP;
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
        
        if (isChange)
        {
            StartCoroutine(ChangeImgWait());
            trainerPokemonCtrl.SetTrigger("ChangePokemon");
        }
        else
        {
            PlayerPokemonImg.sprite = playerPokemonCtrl.pokemon.myCharImg_Back;
            if(isCall)StartCoroutine(SpawnPokemonWait());
            else StartCoroutine(MenuView());

        }

    }
    public IEnumerator SpawnPokemonWait()
    {
        yield return new WaitForSeconds(1.5f);
        trainerPokemonCtrl.SetTrigger("Move");
        StartCoroutine(SpawnPokemonWai2t());
    }

    public IEnumerator SpawnPokemonWai2t()
    {
        yield return new WaitForSeconds(0.3f);
        DiagText.text = "가랏! " + playerPokemonCtrl.pokemon.nameKor + "!!";
        StartCoroutine(MenuView());
    }
    public IEnumerator ChangeImgWait()
    {
        yield return new WaitForSeconds(0.15f);
        PlayerPokemonImg.sprite = playerPokemonCtrl.pokemon.myCharImg_Back;
        StartCoroutine(MenuView());
    }


    public void CallView()
    {
        battleMenuCtrl.ResetMenu();
        StartCoroutine(MenuView());
        StartCoroutine(TrainerMove(true));
    }

    public void NPCBattle(string _name, NPCCtrl npc)
    {
        myNPC = npc;
        myNPC.TextCursor = 0;
        NPCNAME = _name;
        isNpcBattle = true;
        PlayerPokemonImg.gameObject.transform.position = currentTrainerPokemonPos;
        EnemyPosCtrl.transform.position = enemyLeaderAnim.gameObject.transform.position;
        battleCtrlPnm.GUIToggle(true);
        enemyLeaderAnim.SetTrigger("Move");
        DiagText.text = _name+"\n이가 승부를 걸어왔다!";
        StartCoroutine(CheckDown());
        StartCoroutine(TrainerMove(false));

    }
    public bool isWait = false;
    private void Update()
    {
        if (isNpcBattle)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!isBattleStart)
                {
                    GameObject.Find("BattleProcess").GetComponent<BattleProcess>().currentZone = gameObject.GetComponent<PokemonZone>();
                    NPCBattlePokemonSpawn(0);
                }

            }
        }
    }

    IEnumerator CheckDown()
    {
        yield return new WaitForSeconds(1.5f);
        isWait = true;
    }

    public void NPCBattlePokemonSpawn(int _num)
    {
        if (!isWait) return;
        if(!isBattleStart)enemyLeaderAnim.SetTrigger("Return");
        isBattleStart = true;
        battleCursor += _num;
        pokemons[battleCursor].RandomPokeMon();
        DiagText.text = NPCNAME + "(은)\n" + pokemons[battleCursor].nameKor + "(을)차례로 꺼냈다!";
        enemyPokemonAnim.SetTrigger("Move");
        EnemyImg.sprite = pokemons[battleCursor].myCharImg_Front;
        NameText.text = pokemons[battleCursor].nameKor + ":L" + pokemons[battleCursor].LEVEL;
        int i = 0;
        PoketmonType _pkmTemp = null;
        bool isSet = false;
        foreach (PoketmonType _pkm in playerPokemonCtrl.pokemonList)
        {
            if (_pkm.HP > 0)
            {
                if (isSet == false)
                {
                    isSet = true;
                    _pkmTemp = _pkm;
                };
                i++;
            }
        }
        playerPokemonCtrl.pokemon = _pkmTemp;
        GameObject.Find("BattleProcess").GetComponent<BattleProcess>().myPokemon = playerPokemonCtrl.pokemon;
        ResetPokemon(false, (battleCursor == 0));
        GameObject.Find("BattleProcess").GetComponent<BattleProcess>().enemyPokemon = pokemons[battleCursor];
        GameObject.Find("BattleProcess").GetComponent<BattleProcess>().ResetEHP();
        GameObject.Find("BattleProcess").GetComponent<BattleProcess>().ResetHP();
        GameObject.Find("BattleProcess").GetComponent<BattleProcess>().nowBattle = true;
        //if(battleCursor == 0)StartCoroutine(SpawnPokemonWait());

        //CallView();



    }


    public void CheckZone(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            int i = 0;
            PoketmonType _pkmTemp = null;
            bool isSet = false;
            foreach (PoketmonType _pkm in playerPokemonCtrl.pokemonList)
            {
                if (_pkm.HP > 0)
                {
                    if (isSet == false) 
                    {
                        isSet = true;
                        _pkmTemp = _pkm;
                    };
                    i++;
                }
            }
            if (i == 0) return;

            int _per = Random.Range(1, 101);
            if (_per <= percent)
            {
                //포켓몬 등장!
                int _rdm = Random.Range(0, pokemons.Count);
                pokemons[_rdm].RandomPokeMon();
                battleCtrlPnm.GUIToggle(true);
                DiagText.text = "야생의 " + pokemons[_rdm].nameKor + "이(가) 나타났다!";
                EnemyImg.sprite = pokemons[_rdm].myCharImg_Front;
                NameText.text = pokemons[_rdm].nameKor+":L"+ pokemons[_rdm].LEVEL;


                GameObject.Find("BattleProcess").GetComponent<BattleProcess>().currentZone = gameObject.GetComponent<PokemonZone>();
                playerPokemonCtrl.pokemon = _pkmTemp;
                GameObject.Find("BattleProcess").GetComponent<BattleProcess>().myPokemon = playerPokemonCtrl.pokemon;
                ResetPokemon(false, true);
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
        battleMenuObj.SetActive(false);
        skillGroup.GetComponent<KeyboardMenuCtrl>().OpenSet();

        SkillMenu.SetActive(true);
    }


    private IEnumerator TrainerMove(bool _type)
    {
        yield return new WaitForSeconds(1.5f);
        trainerCtrl.SetTrigger("Move");
        if(_type)trainerPokemonCtrl.SetTrigger("Move");
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
