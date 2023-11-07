using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleProcess : MonoBehaviour
{
    public PoketmonType enemyPokemon;
    public PoketmonType myPokemon;

    public Image enemyHPBar;
    public Image myHPBar;

    public Animator enemyAnimator;
    public Animator myAnimator;

    public bool _nowEnemyAnim = false;

    public Text DialogText;

    public GameObject SkillMenu;

    public GameObject menuGroup;

    


    public void EnemyAnim(string _name, string _nameKr)
    {
        _nowEnemyAnim = true;
        enemyAnimator.SetTrigger(_name);
        SkillMenu.SetActive(false);
        StartCoroutine(SkillUseWait(_nameKr));
    }

    IEnumerator SkillUseWait(string _nameKr)
    {

        DialogText.text = "";
        yield return new WaitForSeconds(1f);
        DialogText.text = myPokemon.nameKor + "의" + _nameKr + "!";
        yield return new WaitForSeconds(1f);
        menuGroup.SetActive(true);



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
