using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    
    public PoketmonType targetMob;
    public BattleProcess battleProcess;


    void Start()
    {
        






    }

    private void OnEnable()
    {
        battleProcess.GetPokemon();
    }

}
