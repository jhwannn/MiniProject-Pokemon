using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GUIManager : MonoBehaviour
{

    [SerializeField]
    public GameObject[] GUIList;




    private GameObject nowOpened;

    void Start()
    {
       GUIList = GameObject.FindGameObjectsWithTag("GUI");


    }


    public GameObject NowOpened { get => nowOpened; set => nowOpened = value; }

}
