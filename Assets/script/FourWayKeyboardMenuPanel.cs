using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FourWayKeyboardMenuPanel : MonoBehaviour
{
    public int menuIndex;
    public GameObject myCursor;
    [Header("Select Enter Script")]
    public UnityEvent ClickTrigger;
    public void SelectTrigger()
    {
        ClickTrigger.Invoke();
    }


}
