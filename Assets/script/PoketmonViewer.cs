using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoketmonViewer : MonoBehaviour
{
    public Image _image;
    public PoketmonType type;



    public void SetPokemon(PoketmonType _type)
    {
        type = _type;
       // _image.sprite = type.myCharImg;
        gameObject.SetActive(true);
    }



}
