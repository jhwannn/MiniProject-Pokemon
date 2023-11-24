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
    public bool canSkip = false;

    IEnumerator cor;
    
    private void Start()
    {
        cor = WaitForOpenning();
        StartCoroutine(cor);
    }

    IEnumerator WaitForOpenning()
    {
        yield return new WaitForSeconds(25f);
        BeforePlayer.Play();
        isOpenFin = true;
        canSkip = true;
        TargetImage.texture = BeforeStartTex;

    }

    IEnumerator WaitForSceneSkip()
    {
        yield return new WaitForSeconds(0.1f);
        canSkip = true;
    }

    private void Update()
    {
        if (Input.anyKey && isOpenFin && canSkip)
        {
            LoadingSceneManager.LoadScene("GameScene");
            return;
        }
        if (Input.anyKey && !isOpenFin)
        {
            StartCoroutine(WaitForSceneSkip());
            isOpenFin = true;
            BeforePlayer.Play();
            TargetImage.texture = BeforeStartTex;
            StopCoroutine(cor);
            return;
        }

        
    }

}
