using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalCtrl : MonoBehaviour
{
    public Transform myTarget;
    public bool isDown;
    public Animator Flash;

    IEnumerator Delay(Collider2D collision)
    {
        yield return new WaitForSeconds(0.1f);
        float arrow = 0.5f;
        if (isDown) arrow *= -1;
        collision.transform.position = new Vector3(myTarget.transform.position.x, myTarget.transform.position.y + arrow, myTarget.transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Flash.SetTrigger("Flash");
            StartCoroutine(Delay(collision));


        }

    }

}
