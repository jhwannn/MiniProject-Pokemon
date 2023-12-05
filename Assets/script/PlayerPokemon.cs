using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPokemon : MonoBehaviour
{
    public PoketmonType pokemon;
    public List<PoketmonType> pokemonList = new List<PoketmonType>();
    public GameObject parentObject;
    public GameObject _prefab;
    public GameObject isExitObj;



    public void RemoveAllChild()
    {
        foreach (Transform child in parentObject.transform)
        {
            if(child.name != "ExitRow")
            Destroy(child.gameObject);
        }
    }
    
    public void HealPokemon()
    {
        foreach (PoketmonType pkm in pokemonList)
        {
            pkm.HP = pkm.MAXHP;
        }
        SoundCtrl.PlaySound("healed_pokemon");
    }

    public void ListReset()
    {
        RemoveAllChild();
        parentObject.GetComponent<KeyboardMenuCtrl>()._panel = new List<KeyboardMenuPanel>();
        parentObject.GetComponent<KeyboardMenuCtrl>()._panel.Add(isExitObj.GetComponent<KeyboardMenuPanel>());
        int i = 0;
        foreach (PoketmonType pkm in pokemonList)
        {
            GameObject _temp = Instantiate(_prefab);
            _temp.GetComponent<PokemonRowPanel>().SetRow(pkm.nameKor, pkm.HP, pkm.MAXHP, pkm.LEVEL, pkm.myCharImg_Front);
            _temp.transform.SetParent(parentObject.transform);
            _temp.transform.localScale = new Vector3(1, 1, 1);
            parentObject.GetComponent<KeyboardMenuCtrl>()._panel.Add(_temp.GetComponent<KeyboardMenuPanel>());
            _temp.GetComponent<KeyboardMenuPanel>().PokemonSel = i;
            _temp.GetComponent<KeyboardMenuPanel>().isPkm = true;
            i++;

        }


    }




}
