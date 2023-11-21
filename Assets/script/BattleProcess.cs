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

    public bool _nowEnemyAnim = false;
    public Transform enemyImage;

    public Text DialogText;

    public GameObject SkillMenu;

    public GameObject menuGroup;
    public PanelManager battleCtrlPnm;
    public Vector3 currentPos;
    public Transform trainerImage;
    public Transform trainerPokemonImage;
    public Vector3 trainerCurrentPos;
    public Vector3 trainerPokemonCurrentPos;
    public bool nowBattle;

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
            menuGroup.SetActive(true);
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
