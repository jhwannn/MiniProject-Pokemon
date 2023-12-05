using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleProcess : MonoBehaviour
{
    public PoketmonType enemyPokemon;
    public PoketmonType myPokemon;

    public HPCtrl enemyHPBar;
    public HPCtrl myHPBar;

    public Animator enemyAnimator;
    public Animator myAnimator;
    public Image EnemyImage;

    public Animator TrainerSkillEffect;
    public Image expBar;

    public bool _nowEnemyAnim = false;
    public Transform enemyImage;

    public Text DialogText;

    public PlayerPokemon playerPokemon;
    public GameObject SkillMenu;

    public GameObject menuGroup;
    public PanelManager battleCtrlPnm;
    public Vector3 currentPos;
    public Transform trainerImage;
    public Transform trainerPokemonImage;
    public Vector3 trainerCurrentPos;
    public Vector3 trainerPokemonCurrentPos;
    public bool nowBattle;
    public PokemonZone currentZone;
    public PanelManager pokemonListTarget;
    public Text enemyName;
    public Text myName;
    public GameObject BallObj;
    public Animator BallAnim;
    public PanelManager BagPnm;
    public GameObject Hider;
    public Vector3 currentBallPos;

    public GameObject RevolutionObj;
    public Image RevPkmImg;
    public PanelManager battleGUI;

    public AudioSource BGMPlayer;

    public AudioClip saveClip;
    public GameObject dieGuard;
    
    



    private void Start()
    {
        currentPos = enemyImage.position;
        trainerCurrentPos = trainerImage.position;
        trainerPokemonCurrentPos = trainerPokemonImage.position;
        currentBallPos = BallObj.transform.position;
    }

    public void UseBall()
    {
        if (currentZone.isNpcBattle)
        {
            BagPnm.GUIToggle(false);
            menuGroup.SetActive(false);
            DialogText.text = "마스터와의 배틀에서는\n사용할 수 없다!";
            StartCoroutine(WaitForErrorMsg());
            return;
        }
        SoundCtrl.PlaySound("Throw_ball");
        BagPnm.GUIToggle(false);
        menuGroup.SetActive(false);
        BallObj.SetActive(true);
        BallAnim.SetTrigger("Throw");

    }
    IEnumerator WaitForErrorMsg()
    {
        yield return new WaitForSeconds(0.5f);
        menuGroup.SetActive(true);

    }

    public void GetPokemon()
    {

        
        float per = 100 - enemyPokemon.HP / enemyPokemon.MAXHP * 100;
        Debug.Log("확률 : " + per);
        int _rdm = Random.Range(1, 101);
        
        if (_rdm <= per)
        {
            DialogText.text = "신난다!\n" + enemyPokemon.nameKor + "를(을) 잡았다!!";
            
            SoundCtrl.PlaySound("Get_item_or_pokemon");
            playerPokemon.pokemonList.Add(new Pikachu(enemyPokemon));
            StartCoroutine(WaitForGetComplete());

        }
        else
        {
            DialogText.text = "으으!\n잡았다고 생각했는데!";

            StartCoroutine(WaitForGetPokemon());



        }

    }
    public IEnumerator WaitForGetComplete()
    {
        yield return new WaitForSeconds(1.5f);
        battleCtrlPnm.GUIToggle(false);
        nowBattle = false;
        ResetBattle();
    }

    public IEnumerator WaitForGetPokemon()
    {
        yield return new WaitForSeconds(1.5f);
        BallObj.SetActive(false);
        BallObj.transform.position = currentBallPos;
        Hider.SetActive(false);
        BallAnim.SetTrigger("Reset");

        //종료처리
        //menuGroup.SetActive(true);
        int _rdm = Random.Range(0, enemyPokemon.skillList.Count);
        SkillType _skill = enemyPokemon.skillList[_rdm];
        TrainerAnim(_skill.NameEng.Replace(" ", "") + "_Hit", _skill.NameKR, _skill.Damage);


    }

    public void ResetBattle()
    {
        enemyHPBar.SetHP(1);
        myHPBar.SetHP(1);
        enemyPokemon = null;
        myPokemon = null;
        enemyImage.position = currentPos;
        trainerImage.position = trainerCurrentPos;
        trainerPokemonImage.position = trainerPokemonCurrentPos;
        BallObj.SetActive(false);
        BallObj.transform.position = currentBallPos;
        Hider.SetActive(false);
        BallAnim.SetTrigger("Reset");
        RevolutionObj.SetActive(false);
        menuGroup.SetActive(false);
        nowBattle = false;
        BGMEndBattle();


    }
    public void RunBattle()
    {
        if (currentZone.isNpcBattle)
        {
            DialogText.text = "도망칠 수 없다!";
            return;
        }
        SoundCtrl.PlaySound("Runaway");
        menuGroup.SetActive(false);
        DialogText.text = "배틀에서 도망친다!";
        StartCoroutine(WaitSoundRun());

    }

    public void BGMEndBattle()
    {
        BGMPlayer.clip = saveClip;
        BGMPlayer.Play();
    }

    IEnumerator WaitSoundRun()
    {
        yield return new WaitForSeconds(1f);
        battleGUI.GUIToggle(false);
        nowBattle = false;
        ResetBattle();
    }

    public void EnemyAnim(string _name, string _nameKr, int _Damage)
    {
        _nowEnemyAnim = true;
        enemyAnimator.SetTrigger(_name);
        SkillMenu.SetActive(false);
        Debug.Log("디버그 체력 : " + enemyPokemon.HP);
        enemyPokemon.HP -= _Damage;
        Debug.Log("상대 체력 : [ " + enemyPokemon.HP + " / " + enemyPokemon.MAXHP + " ]");
        float _hp = (enemyPokemon.HP / enemyPokemon.MAXHP);
        enemyHPBar.SetHP(_hp);
        StartCoroutine(SkillUseWait(_nameKr));
    }


    public void TrainerAnim(string _name, string _nameKr, int _Damage)
    {
        Debug.Log("사용한 애니메이션 : " + _name);
        TrainerSkillEffect.SetTrigger(_name);
        //SkillMenu.SetActive(false);
        myPokemon.HP -= _Damage;
        Debug.Log("내 체력 : [ " + myPokemon.HP + " / " + myPokemon.MAXHP + " ]");
        float _hp = (myPokemon.HP / myPokemon.MAXHP);
        myHPBar.SetHP(_hp);
        StartCoroutine(TrainerSkillEffectWait(_nameKr));


    }
    public void ResetHP()
    {
        float _hp = (myPokemon.HP / myPokemon.MAXHP);
        myHPBar.SetHP(_hp);

    }
    public void ResetEHP()
    {
        float _hp = (enemyPokemon.HP / enemyPokemon.MAXHP);
        enemyHPBar.SetHP(_hp);

    }


    public void ChangePokemon(int i)
    {
        playerPokemon.pokemon = playerPokemon.pokemonList[i];
        pokemonListTarget.GUIToggle(false);
        currentZone.ResetPokemon(true, true);
        DialogText.text = "가랏! " + myPokemon.nameKor + "!";

    }



    IEnumerator TrainerSkillEffectWait(string _nameKr)
    {
        DialogText.text = "";
        yield return new WaitForSeconds(1f);
        DialogText.text = enemyPokemon.nameKor + "의" + _nameKr + "!";
        yield return new WaitForSeconds(1f);
        if (myPokemon.HP <= 0)
        {
            yield return new WaitForSeconds(1f);
            DialogText.text = myPokemon.nameKor + "이(가) 쓰러졌다!";
            SoundCtrl.PlaySound("pokemon_die");
            myAnimator.SetTrigger("DieTrainer");
            yield return new WaitForSeconds(1.3f);
            int i = 0;
            bool isChange = false;
            foreach(PoketmonType _pkm in playerPokemon.pokemonList)
            {
                if(playerPokemon.pokemonList[i].HP > 0)
                {
                    isChange = true;
                    Debug.Log(playerPokemon.pokemonList[i].nameKor + " : " + playerPokemon.pokemonList[i].HP);
                    playerPokemon.pokemon = playerPokemon.pokemonList[i];
                    currentZone.ResetPokemon(false, true);
                    
                    break;
                }
                i++;

            }
            if (!isChange)
            {

                DialogText.text = "심향은 싸울 수 있는 포켓몬이 없다...";
                yield return new WaitForSeconds(1f);
                battleCtrlPnm.GUIToggle(false);
                ResetBattle();
            }


        }
        else
        {
            menuGroup.SetActive(true);
        }

    }

    IEnumerator SkillUseWait(string _nameKr)
    {

        DialogText.text = "";
        yield return new WaitForSeconds(1f);
        DialogText.text = myPokemon.nameKor + "의" + _nameKr + "!";
        yield return new WaitForSeconds(1f);
        if (enemyPokemon.HP <= 0)
        {
            yield return new WaitForSeconds(1f);
            DialogText.text = enemyPokemon.nameKor + "을(를) 쓰러트렸다!";
            dieGuard.SetActive(true);
            SoundCtrl.PlaySound("pokemon_die");
            enemyAnimator.SetTrigger("DIE");
            yield return new WaitForSeconds(0.5f);
            int _addExp = (int)(enemyPokemon.MAXHP * 1);
            DialogText.text = myPokemon.nameKor + "은(는) \n" + _addExp + " 경험치를 얻었다!";
            expBar.fillAmount = myPokemon.EXP / myPokemon.MAXEXP;
            
            if (currentZone.myNPC != null)
            {
                if (currentZone.myNPC.isLeader)
                if (!(currentZone.pokemons.Count - 1 > currentZone.battleCursor))  SoundCtrl.PlaySound("Gym_leader_battle_victory");
            }
            else
            {
                SoundCtrl.PlaySound("Wild_Pokemon_victory");
            }


            myPokemon.EXP += _addExp;
            yield return new WaitForSeconds(0.7f);

            EnemyImage.sprite = null;
            if (currentZone.isNpcBattle)
            {
                if(currentZone.pokemons.Count-1 > currentZone.battleCursor)
                {
                    yield return new WaitForSeconds(1f);
                    currentZone.NPCBattlePokemonSpawn(+1);

                }
                else
                {

                    DialogText.text = currentZone.NPCNAME + "과의 \n대결에서 승리했다!";
                    if (currentZone.myNPC.isEnding)
                    {
                        LoadingSceneManager.LoadScene("Ending");


                    }

                    currentZone.myNPC.isBlock = true;
                    yield return new WaitForSeconds(1f);
                    if (myPokemon.EXP >= myPokemon.MAXEXP)
                    {
                        StartCoroutine(CalcExp());
                    }
                    else
                    {

                        battleCtrlPnm.GUIToggle(false);
                        ResetBattle();
                    }

                }

            }
            else
            {

                if (myPokemon.EXP >= myPokemon.MAXEXP)
                {

                    StartCoroutine(CalcExp());
                }
                else
                {

                    battleCtrlPnm.GUIToggle(false);
                    ResetBattle();
                }



            }


            

        }
        else
        {
            int _rdm = Random.Range(0, enemyPokemon.skillList.Count);
            SkillType _skill = enemyPokemon.skillList[_rdm];
            TrainerAnim(_skill.NameEng.Replace(" ", "") + "_Hit", _skill.NameKR, _skill.Damage);


        }



    }

    public IEnumerator CalcExp()
    {
        BGMPlayer.Stop();
        while(myPokemon.EXP >= myPokemon.MAXEXP)
        {
            myPokemon.EXP -= myPokemon.MAXEXP;
            myPokemon.MAXEXP = (int)(myPokemon.MAXEXP * 1.1f);
            myPokemon.LEVEL++;
            expBar.fillAmount = myPokemon.EXP / myPokemon.MAXEXP;
            myName.text = myPokemon.nameKor + ":L" + myPokemon.LEVEL;
            DialogText.text = myPokemon.nameKor + "은(는) \n레벨" + myPokemon.LEVEL + "(으)로 올랐다!";
            SoundCtrl.PlaySound("Level_Up");

            yield return new WaitForSeconds(1f);
            if (myPokemon.EXP >= myPokemon.MAXEXP)
            {
                
                
            }
            else
            {
                break;
            }

        }
        if(myPokemon.LEVEL >= 16 && myPokemon.myNextMob != null)
        {
            SoundCtrl.PlaySound("Evolution");
            RevolutionObj.SetActive(true);
            RevPkmImg.sprite = myPokemon.myCharImg_Front;
            StartCoroutine(RevPkmImgIenum());
            DialogText.text = "...... 오잉!?\n"+myPokemon.nameKor + "의 상태가.....!!";

        }
        else
        {
            yield return new WaitForSeconds(1.3f);
            battleCtrlPnm.GUIToggle(false);
            ResetBattle();

        }


    }
    IEnumerator RevPkmImgIenum()
    {
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            if(i%2 ==0) RevPkmImg.sprite = myPokemon.myCharImg_Front;
            else RevPkmImg.sprite = myPokemon.myNextMob.myCharImg_Front;
        }
        yield return new WaitForSeconds(0.5f);
        DialogText.text = "축하합니다! "+ myPokemon.nameKor+"는(은)\n"+ myPokemon.myNextMob.nameKor+"(으)로 진화했다!";
        int e = 0;
        foreach(PoketmonType _pkm in playerPokemon.pokemonList)
        {
            if(_pkm == myPokemon)
            {
                Debug.Log("찾음");
                break;
                
            }
            e++;

        }
        Pikachu _new = new Pikachu(myPokemon.myNextMob);
        _new.EXP = myPokemon.EXP;
        _new.MAXEXP = myPokemon.MAXEXP;
        _new.LEVEL = myPokemon.LEVEL;
        _new.skillList = myPokemon.skillList;
        playerPokemon.pokemonList[e] = _new;
        myPokemon = _new;

        yield return new WaitForSeconds(2f);
        SoundCtrl.StopSound();
        battleCtrlPnm.GUIToggle(false);
        ResetBattle();


    }



    private void Update()
    {


        if (_nowEnemyAnim && enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdle"))
        {
            _nowEnemyAnim = false;
            Debug.Log("스킬 종료");
        }
    }










}
