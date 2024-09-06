using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Launcher_Tool : MonoBehaviour, IPointerClickHandler
{
    public bool Inst = false;
    public bool Inst_Save = false;

    public int Select = -1;

    public bool Inst_Change_Up;
    public bool Inst_Change_Down;

    public bool BGM = false;
    public bool BGM_Close = false;
    public bool BGM_Change_Next = false;
    public bool BGM_Change_Prev = false;
    public bool BGM_Play = false;

    private GameLauncher_ICT Launcher;

    void Start()
    {
        Launcher = GameObject.Find("VirtualController").GetComponent<GameLauncher_ICT>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Inst)
            Manager_Tool.instance.Active_Inst();

        if (Inst_Save)
            Manager_Tool.instance.Save_Inst();

        if (Inst_Change_Up && Select != -1)
            Manager_Tool.instance.Change_Inst_Next(Select);

        if (Inst_Change_Down && Select != -1)
            Manager_Tool.instance.Change_Inst_Prev(Select);


        if (BGM)
            Manager_Tool.instance.Active_BGM();

        if (BGM_Close)
            Manager_Tool.instance.Inactive_BGM();

        if (BGM_Change_Next)
            Manager_Tool.instance.Change_BGM_Next();

        if (BGM_Change_Prev)
            Manager_Tool.instance.Change_BGM_Prev();

        if (BGM_Play)
            Manager_Tool.instance.Play_BGM();
    }
}
