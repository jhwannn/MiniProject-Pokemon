using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySoundPlayer : MonoBehaviour
{
    public AudioSource player;
    public AudioClip myClip;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.clip = myClip;
            player.Play();
        }

    }
}
