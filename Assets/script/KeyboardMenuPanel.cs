using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardMenuPanel : MonoBehaviour
{
    [Header("Select Enter Script")]
    public UnityEvent ClickTrigger;
    public UnityEvent ChooseTrigger;

    [SerializeField]
    private GameObject mySelIcon;

    public int PokemonSel;
    public bool isPkm;



    public void SetStatus(bool _type)
    {
        mySelIcon.SetActive(_type);
    }
    public void SelectTrigger()
    {
        if(isPkm && GameObject.Find("BattleProcess").GetComponent<BattleProcess>().nowBattle)
        {
            Debug.Log("select 2");
            GameObject.Find("BattleProcess").GetComponent<BattleProcess>().ChangePokemon(PokemonSel);
        }
        else
        {

        }
 
        ClickTrigger.Invoke();


    }
    public void ChooseTriggerAction()
    {
        if (ChooseTrigger != null)
        {
            ChooseTrigger.Invoke();
        }

    }

}