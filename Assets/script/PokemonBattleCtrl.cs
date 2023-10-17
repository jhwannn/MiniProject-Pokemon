using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonBattleCtrl : MonoBehaviour
{
    public Image BlackBox;


    public void ViewUI()
    {
        BlackBox.fillAmount = 1;
        StartCoroutine(ChangeFill());
    }

    IEnumerator ChangeFill()
    {
        while(BlackBox.fillAmount > 0)
        {
            BlackBox.fillAmount = BlackBox.fillAmount - 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

}
