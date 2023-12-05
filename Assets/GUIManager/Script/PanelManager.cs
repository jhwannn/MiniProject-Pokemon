using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PanelManager : MonoBehaviour
{
    [Header("GUI Key Setting")]
    public KeyCode GUIKey;
    [Header("GUI Option Setting")]
    public bool AutoEnable;
    public bool ShowCursor;
    public bool AlwaysShowCursor;
    


    [Header("GUI Child Object Group")]
    [SerializeField]
    private GameObject GUIBackground;
    [SerializeField]
    private GameObject GUIButtonGroup;



    [Header("Exception Object")]
    [SerializeField]
    private string ExceptionObject;
    [Header("GUI Enable Trigger")]
    public UnityEvent EnableTrigger;
    [Header("GUI Disable Trigger")]
    public UnityEvent DisableTrigger;


    [Header("Key Input Close")]
    public bool KeyInputClose;
    public KeyCode CloseKey;

    public bool GUIStatus = false;
    private GameObject GUIPanel;
    public AudioClip OpenSound;
    public AudioClip CloseSound;
    private AudioSource guiAudio;








    void Start()
    {
        guiAudio = GameObject.Find("GUIManager").GetComponent<AudioSource>();
        GUIPanel = gameObject;
        if (AutoEnable) GUIStatus = AutoEnable;
        GUIBackground.SetActive(GUIStatus);
        if (AutoEnable && ShowCursor) Cursor.visible = true;

        

    }

    void Update()
    {
        if(KeyInputClose && Input.GetKeyDown(CloseKey) && GUIStatus)
        {
            GUIToggle(false);
            return;
        }

        if (Input.GetKeyDown(GUIKey))
        {
            if (ExceptionObject != null && GameObject.Find(ExceptionObject) == true) return;
            GUIStatus = !GUIStatus;
            PlaySound(GUIStatus);
            if (!GUIStatus) DisableTrigger.Invoke();
            GUIBackground.SetActive(GUIStatus);

            if (GUIStatus) EnableTrigger.Invoke();
            if (ShowCursor)
            {
                Cursor.visible = GUIStatus;
                Cursor.lockState = GUIStatus ? CursorLockMode.None : CursorLockMode.Locked;
            }


        }
    }
    

    private void PlaySound(bool _status)
    {
        if (_status)
        {
            if (OpenSound == null) return;
            guiAudio.clip = OpenSound;
        }else
        {
            if (CloseSound == null) return;
            guiAudio.clip = CloseSound;
        }
        guiAudio.Play();

    }
 
    
    public void GUIToggle()
    {
        if (AutoEnable) return;
        if (ExceptionObject != null && GameObject.Find(ExceptionObject) == true) return;
        GUIStatus = !GUIStatus;
        PlaySound(GUIStatus);
        if (!GUIStatus) DisableTrigger.Invoke();
        GUIBackground.SetActive(GUIStatus);
        if (GUIStatus) EnableTrigger.Invoke();
        bool _status = GUIStatus;
        if (AlwaysShowCursor) _status = true;

        if (ShowCursor)
        {
            Cursor.visible = _status;
            Cursor.lockState = _status ? CursorLockMode.None : CursorLockMode.Locked;
        }


    }
    public void GUIToggle(bool _updateStatus)
    {
        GUIBackground.SetActive(_updateStatus);
        GUIStatus = _updateStatus;
        PlaySound(GUIStatus);
        if (!GUIStatus) DisableTrigger.Invoke();
        GUIBackground.SetActive(GUIStatus);
        if (GUIStatus) EnableTrigger.Invoke();
        bool _status = GUIStatus;

        if (AlwaysShowCursor) _status = true;
        if (ShowCursor)
        {
            Cursor.visible = _status;
            Cursor.lockState = _status ? CursorLockMode.None : CursorLockMode.Locked;
        }

    }




}
