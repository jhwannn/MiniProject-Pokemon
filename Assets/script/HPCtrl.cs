using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPCtrl : MonoBehaviour
{
    public Image hpImage;
    public Dictionary<int, Color> hpData = new Dictionary<int, Color>();

    private void Start()
    {
        hpData.Add(20, new Color32(179, 0, 7, 255));
        hpData.Add(50, new Color32(255, 116, 0, 255));
        hpData.Add(100, new Color32(2, 118, 0, 255));

    }

    public void SetHP(float per)
    {
        hpImage.fillAmount = per;
        foreach (KeyValuePair<int, Color> item in hpData)
        {
            if(per*100 <= item.Key)
            {
                hpImage.color = item.Value;
                return;
            }
        }


    }
    
}
