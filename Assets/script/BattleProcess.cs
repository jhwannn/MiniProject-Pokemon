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
            DialogText.text = "�����Ϳ��� ��Ʋ������\n����� �� ����!";
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
        Debug.Log("Ȯ�� : " + per);
        int _rdm = Random.Range(1, 101);
        
        if (_rdm <= per)
        {
            DialogText.text = "�ų���!\n" + enemyPokemon.nameKor + "��(��) ��Ҵ�!!";
            
            SoundCtrl.PlaySound("Get_item_or_pokemon");
            playerPokemon.pokemonList.Add(new Pikachu(enemyPokemon));
            StartCoroutine(WaitForGetComplete());

        }
        else
        {
            DialogText.text = "����!\n��Ҵٰ� �����ߴµ�!";

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

        //����ó��
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
            DialogText.text = "����ĥ �� ����!";
            return;
        }
        SoundCtrl.PlaySound("Runaway");
        menuGroup.SetActive(false);
        DialogText.text = "��Ʋ���� ����ģ��!";
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
        Debug.Log("����� ü�� : " + enemyPokemon.HP);
        enemyPokemon.HP -= _Damage;
        Debug.Log("��� ü�� : [ " + enemyPokemon.HP + " / " + enemyPokemon.MAXHP + " ]");
        float _hp = (enemyPokemon.HP / enemyPokemon.MAXHP);
        enemyHPBar.SetHP(_hp);
        StartCoroutine(SkillUseWait(_nameKr));
    }


    public void TrainerAnim(string _name, string _nameKr, int _Damage)
    {
        Debug.Log("����� �ִϸ��̼� : " + _name);
        TrainerSkillEffect.SetTrigger(_name);
        //SkillMenu.SetActive(false);
        myPokemon.HP -= _Damage;
        Debug.Log("�� ü�� : [ " + myPokemon.HP + " / " + myPokemon.MAXHP + " ]");
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
        DialogText.text = "����! " + myPokemon.nameKor + "!";

    }



    IEnumerator TrainerSkillEffectWait(string _nameKr)
    {
        DialogText.text = "";
        yield return new WaitForSeconds(1f);
        DialogText.text = enemyPokemon.nameKor + "��" + _nameKr + "!";
        yield return new WaitForSeconds(1f);
        if (myPokemon.HP <= 0)
        {
            yield return new WaitForSeconds(1f);
            DialogText.text = myPokemon.nameKor + "��(��) ��������!";
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

                DialogText.text = "������ �ο� �� �ִ� ���ϸ��� ����...";
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
        DialogText.text = myPokemon.nameKor + "��" + _nameKr + "!";
        yield return new WaitForSeconds(1f);
        if (enemyPokemon.HP <= 0)
        {
            yield return new WaitForSeconds(1f);
            DialogText.text = enemyPokemon.nameKor + "��(��) ����Ʈ�ȴ�!";
            SoundCtrl.PlaySound("pokemon_die");
            enemyAnimator.SetTrigger("DIE");
            yield return new WaitForSeconds(0.5f);
            int _addExp = (int)(enemyPokemon.MAXHP * enemyPokemon.LEVEL * 3);
            DialogText.text = myPokemon.nameKor + "��(��) \n" + _addExp + " ����ġ�� �����!";
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

                    DialogText.text = currentZone.NPCNAME + "���� \n��ῡ�� �¸��ߴ�!";
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
            DialogText.text = myPokemon.nameKor + "��(��) \n����" + myPokemon.LEVEL + "(��)�� �ö���!";
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
            DialogText.text = "...... ����!?\n"+myPokemon.nameKor + "�� ���°�.....!!";

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
        DialogText.text = "�����մϴ�! "+ myPokemon.nameKor+"��(��)\n"+ myPokemon.myNextMob.nameKor+"(��)�� ��ȭ�ߴ�!";
        int e = 0;
        foreach(PoketmonType _pkm in playerPokemon.pokemonList)
        {
            if(_pkm == myPokemon)
            {
                Debug.Log("ã��");
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
            Debug.Log("��ų ����");
        }
    }










}
