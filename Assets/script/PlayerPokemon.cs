using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPokemon : MonoBehaviour
{
    public PoketmonType pokemon;
    public List<PoketmonType> pokemonList = new List<PoketmonType>();
    public GameObject parentObject;
    public GameObject _prefab;



    public void RemoveAllChild()
    {
        foreach (Transform child in parentObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
    

    public void ListReset()
    {
        RemoveAllChild();
        parentObject.GetComponent<KeyboardMenuCtrl>()._panel = new List<KeyboardMenuPanel>();
        int i = 0;
        foreach (PoketmonType pkm in pokemonList)
        {
            GameObject _temp = Instantiate(_prefab);
            _temp.GetComponent<PokemonRowPanel>().SetRow(pkm.nameKor, pkm.HP, pkm.MAXHP, pkm.LEVEL);
            _temp.transform.SetParent(parentObject.transform);
            parentObject.GetComponent<KeyboardMenuCtrl>()._panel.Add(_temp.GetComponent<KeyboardMenuPanel>());
            _temp.GetComponent<KeyboardMenuPanel>().PokemonSel = i;
            i++;

        }


    }




}
