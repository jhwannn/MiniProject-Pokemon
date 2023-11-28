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
    public string NpcName;
    public Text TextBox;
    public float TalkDelay = 1f;

    public int TalkFrame = 30;

    private bool isSkip = false;
    public int TextCursor = 0;
    public bool isBattle;
    public PokemonZone myZone;

    public bool ForceTalk;

    private bool isFinish = false;

    [Header("NPC Talk Finish Trigger")]
    public UnityEvent FinishTrigger;

    bool isCheckEnter;

    public bool isBlock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (isBlock) return;
            isEnter = true;
            isCheckEnter = false;
            if (ForceTalk && !DialogPnm.GUIStatus && !isFinish)
            {
                DialogPnm.GUIToggle(true);
                isFinish = true;
                //StartCoroutine(TalkIenum());
                TextBox.text = TalkText[TextCursor].Replace("<มู>", "\n");

            }

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
        if (isBlock) return;

        if (isEnter && Input.GetKeyDown(KeyCode.Return) && !DialogPnm.GUIStatus && !ForceTalk && !isCheckEnter)
        {
            DialogPnm.GUIToggle(true);
            //StartCoroutine(TalkIenum());
            TextBox.text = TalkText[TextCursor].Replace("<มู>", "\n");
            return;
        }
        if (isEnter && Input.GetKeyDown(KeyCode.Return)&& DialogPnm.GUIStatus && !isCheckEnter)
        {
            if(TalkText.Count > ++TextCursor)
            {
                TextBox.text = TalkText[TextCursor].Replace("<มู>", "\n");
            }
            else
            {
                isCheckEnter = true;
                DialogPnm.GUIToggle(false);
                if (isBattle) myZone.NPCBattle(NpcName, gameObject.GetComponent<NPCCtrl>());
                if (FinishTrigger != null)FinishTrigger.Invoke();
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
