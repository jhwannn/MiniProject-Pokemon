using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TitleVideoCtrl : MonoBehaviour
{
    bool isOpenFin = false;
    public RawImage TargetImage;
    public Texture OpenningTex;
    public Texture BeforeStartTex;
    public VideoPlayer BeforePlayer;

    IEnumerator cor;
    
    private void Start()
    {
        cor = WaitForOpenning();
        StartCoroutine(cor);
        
    }
    IEnumerator WaitForOpenning()
    {
        yield return new WaitForSeconds(38f);
        BeforePlayer.Play();
        isOpenFin = true;
        TargetImage.texture = BeforeStartTex;

    }

    private void Update()
    {
        if (Input.anyKey && isOpenFin)
        {
            LoadingSceneManager.LoadScene("GameScene");
            return;
        }
        if (Input.anyKey && !isOpenFin)
        {
            isOpenFin = true;
            TargetImage.texture = BeforeStartTex;
            StopCoroutine(cor);
            return;
        }

        
    }

}
