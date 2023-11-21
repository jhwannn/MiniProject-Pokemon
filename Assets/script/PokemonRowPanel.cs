using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonRowPanel : MonoBehaviour
{
    public Text _name;
    public Text _hpText;
    public Image _hpImage;
    public Text _LV;

    public void SetRow(string name, float nowHp, float maxHp, int lv)
    {
        _name.text = name;
        _hpImage.fillAmount = nowHp / maxHp;
        _hpText.text = "[" + nowHp + "/" + maxHp + "]";
        _LV.text = ":L" + lv;


    }

}
