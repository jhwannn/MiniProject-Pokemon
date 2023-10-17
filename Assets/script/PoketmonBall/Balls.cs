using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{
    public string nameKor;
    public string nameEng;
    public int capturePercent;
    public Sprite myBallImg;
    
    public GameManager gameManager;

    private void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void CaptureMob(){
        

    }


}
