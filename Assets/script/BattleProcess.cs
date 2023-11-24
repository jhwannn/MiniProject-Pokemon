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

    public Animator TrainerSkillEffect;

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

    private void Start()
    {
        currentPos = enemyImage.position;
        trainerCurrentPos = trainerImage.position;
        trainerPokemonCurrentPos = trainerPokemonImage.position;
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
        nowBattle = false;

    }
    public void RunBattle()
    {
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


    public void ChangePokemon(int i)
    {
        playerPokemon.pokemon = playerPokemon.pokemonList[i];
        pokemonListTarget.GUIToggle(false);
        currentZone.ResetPokemon(true);
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
                    currentZone.ResetPokemon(false);
                    
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
            enemyAnimator.SetTrigger("DIE");
            yield return new WaitForSeconds(1.5f);
            battleCtrlPnm.GUIToggle(false);
            ResetBattle();
        }
        else
        {
            int _rdm = Random.Range(0, enemyPokemon.skillList.Count);
            SkillType _skill = enemyPokemon.skillList[_rdm];
            TrainerAnim(_skill.NameEng.Replace(" ", "") + "_Hit", _skill.NameKR, _skill.Damage);


        }



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
