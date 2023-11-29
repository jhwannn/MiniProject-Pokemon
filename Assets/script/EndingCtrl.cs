using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndingWait());
        
    }

    IEnumerator EndingWait()
    {
        yield return new WaitForSeconds(14f);
        LoadingSceneManager.LoadScene("Title");

    }
}
