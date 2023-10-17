using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<PoketmonType> pokemonList = new List<PoketmonType>();
    public PoketmonViewer viewer;
    

    

    public void GetRandomMob()
    {
        int _random = Random.Range(0, pokemonList.Count);
        viewer.SetPokemon(pokemonList[_random]);




    }
    
}
