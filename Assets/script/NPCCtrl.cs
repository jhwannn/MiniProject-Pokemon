using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NPCCtrl : MonoBehaviour
{

    public bool isEnter;
    public List<string> TalkText;
    public PanelManager DialogPnm;
    public Text TextBox;
    public float TalkDelay = 1f;

    public int TalkFrame = 30;

    private bool isSkip = false;
    private int TextCursor = 0;

    [Header("NPC Talk Finish Trigger")]
    public UnityEvent FinishTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isEnter = true;

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isEnter = false;
            DialogPnm.GUIToggle(false);
        }
    }
    private void Update()
    {
        if (isEnter && Input.GetKeyDown(KeyCode.Return) && !DialogPnm.GUIStatus)
        {
            DialogPnm.GUIToggle(true);
            //StartCoroutine(TalkIenum());
            TextBox.text = TalkText[TextCursor].Replace("<มู>", "\n");
            return;
        }
        if (isEnter && Input.GetKeyDown(KeyCode.Return)&& DialogPnm.GUIStatus)
        {
            if(TalkText.Count > ++TextCursor)
            {
                TextBox.text = TalkText[TextCursor].Replace("<มู>", "\n");
            }
            else
            {
                DialogPnm.GUIToggle(false);
                FinishTrigger.Invoke();
            }
        }
        
    }
/*    IEnumerator TalkIenum()
    {
        foreach(string _talk in TalkText)
        {
            TextBox.text = _talk;
            for(int i = 0; i < TalkFrame; i++)
            {
                if (isSkip)
                {
                    isSkip = false;
                    Debug.Log("Check");
                    continue;
                }
                yield return new WaitForSeconds(TalkDelay / TalkFrame);
            }
        }
        DialogPnm.GUIToggle(false);
    }*/

}
