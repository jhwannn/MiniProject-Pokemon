using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCtrl : MonoBehaviour
{

    public static AudioSource mySource;

    public List<AudioClip> ClipList;

    private static Dictionary<string, AudioClip> AudioList = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        mySource = GetComponent<AudioSource>();
        for (int i = 0; i < ClipList.Count; i++)
        {
            AudioList.Add(ClipList[i].name, ClipList[i]);
        }
    }

    public static void PlaySound(string _SoundName)
    {
        mySource.Stop();
        mySource.clip = AudioList[_SoundName];
        mySource.Play();
    }
    public static void StopSound()
    {
        mySource.Stop();
    }
}
