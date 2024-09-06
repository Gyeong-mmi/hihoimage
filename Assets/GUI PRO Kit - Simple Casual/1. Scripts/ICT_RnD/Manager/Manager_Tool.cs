using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Tool : MonoBehaviour
{
    public static Manager_Tool instance = null;

    public GameObject Tool_BGM_UI;
    public GameObject Tool_Inst_UI;

    private int[] Inst_num = new int[3] { 0, 1, 0 };
    public GameObject[] Inst_group = new GameObject[3];
    public GameObject Tool_TestObject;
    public Text Text_Music;
    public GameObject Message_Tool_SC;

    public GameObject drum1;
    public GameObject drum2;
    public GameObject drum3;
    public GameObject drum4;

    public Transform ldrum;
    public Transform rdrum;

    public Transform drum_anim1;

    public GameObject anim201;
    public GameObject anim202;
    public GameObject anim203;
    public GameObject anim204;

    public GameObject anim301;
    public GameObject anim302;
    public GameObject anim303;
    public GameObject anim304;

    public GameObject anim401;
    public GameObject anim402;
    public GameObject anim403;
    public GameObject anim404;

    public GameObject anim501;
    public GameObject anim502;
    public GameObject anim503;
    public GameObject anim504;

    private int BGM_num;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }
    void Start()
    {

    }

    void Init()
    {
        //BGM 변경 초기 설정 부분, 초기 설정된 BGM은 0번 위치
        BGM_num = 0;

        ///악기 변경 화면 초기 설정 부분
        Inst_num[0] = 0;
        Inst_num[1] = 1;
        Inst_num[2] = 0;
        Active_SelectedInst(0, 0);
        Active_SelectedInst(1, 1);
        Active_SelectedInst(2, 0);
    }

    public void Active_Inst()
    {
        Tool_Inst_UI.SetActive(true);
        Tool_BGM_UI.SetActive(false);

        Tool_TestObject.SetActive(true);
    }
    public void Save_Inst()
    {
        Message_Tool_SC.SetActive(true);
    }
    public void Change_Inst_Next(int selected)
    {
        Inst_num[selected] += 1;
        if (Inst_num[selected] > 4)
            Inst_num[selected] = 4;
        Active_SelectedInst(selected, Inst_num[selected]);

        if (selected == 0)
        {
            if (Inst_num[selected] == 1)
            {
                ldrum.GetChild(0).gameObject.SetActive(true);
                ldrum.GetChild(1).gameObject.SetActive(false);
                ldrum.GetChild(2).gameObject.SetActive(false);
                ldrum.GetChild(3).gameObject.SetActive(false);
            }
            else if (Inst_num[selected] == 2)
            {
                ldrum.GetChild(0).gameObject.SetActive(false);
                ldrum.GetChild(1).gameObject.SetActive(true);
                ldrum.GetChild(2).gameObject.SetActive(false);
                ldrum.GetChild(3).gameObject.SetActive(false);
            }
            else if (Inst_num[selected] == 3)
            {
                ldrum.GetChild(0).gameObject.SetActive(false);
                ldrum.GetChild(1).gameObject.SetActive(false);
                ldrum.GetChild(2).gameObject.SetActive(true);
                ldrum.GetChild(3).gameObject.SetActive(false);
            }
            else if (Inst_num[selected] == 4)
            {
                ldrum.GetChild(0).gameObject.SetActive(false);
                ldrum.GetChild(1).gameObject.SetActive(false);
                ldrum.GetChild(2).gameObject.SetActive(false);
                ldrum.GetChild(3).gameObject.SetActive(true);
            }
        }
        if (selected == 1)
        {
            if (Inst_num[selected] == 1)
            {
                drum1.SetActive(true);
                drum2.SetActive(false);
                drum3.SetActive(false);
                drum4.SetActive(false);

                drum_anim1.GetChild(0).gameObject.SetActive(true);
                drum_anim1.GetChild(1).gameObject.SetActive(false);
                drum_anim1.GetChild(2).gameObject.SetActive(false);
                drum_anim1.GetChild(3).gameObject.SetActive(false);

                anim201.SetActive(true);
                anim202.SetActive(false);
                anim203.SetActive(false);
                anim204.SetActive(false);

                anim301.SetActive(true);
                anim302.SetActive(false);
                anim303.SetActive(false);
                anim304.SetActive(false);

                anim401.SetActive(true);
                anim402.SetActive(false);
                anim403.SetActive(false);
                anim404.SetActive(false);

                anim501.SetActive(true);
                anim502.SetActive(false);
                anim503.SetActive(false);
                anim504.SetActive(false);
            }
            else if (Inst_num[selected] == 2)
            {
                drum1.SetActive(false);
                drum2.SetActive(true);
                drum3.SetActive(false);
                drum4.SetActive(false);

                drum_anim1.GetChild(0).gameObject.SetActive(false);
                drum_anim1.GetChild(1).gameObject.SetActive(true);
                drum_anim1.GetChild(2).gameObject.SetActive(false);
                drum_anim1.GetChild(3).gameObject.SetActive(false);

                anim201.SetActive(false);
                anim202.SetActive(true);
                anim203.SetActive(false);
                anim204.SetActive(false);

                anim301.SetActive(false);
                anim302.SetActive(true);
                anim303.SetActive(false);
                anim304.SetActive(false);

                anim401.SetActive(false);
                anim402.SetActive(true);
                anim403.SetActive(false);
                anim404.SetActive(false);

                anim501.SetActive(false);
                anim502.SetActive(true);
                anim503.SetActive(false);
                anim504.SetActive(false);
            }
            else if (Inst_num[selected] == 3)
            {
                drum1.SetActive(false);
                drum2.SetActive(false);
                drum3.SetActive(true);
                drum4.SetActive(false);

                drum_anim1.GetChild(0).gameObject.SetActive(false);
                drum_anim1.GetChild(1).gameObject.SetActive(false);
                drum_anim1.GetChild(2).gameObject.SetActive(true);
                drum_anim1.GetChild(3).gameObject.SetActive(false);

                anim201.SetActive(false);
                anim202.SetActive(false);
                anim203.SetActive(true);
                anim204.SetActive(false);

                anim301.SetActive(false);
                anim302.SetActive(false);
                anim303.SetActive(true);
                anim304.SetActive(false);

                anim401.SetActive(false);
                anim402.SetActive(false);
                anim403.SetActive(true);
                anim404.SetActive(false);

                anim501.SetActive(false);
                anim502.SetActive(false);
                anim503.SetActive(true);
                anim504.SetActive(false);
            }
            else if (Inst_num[selected] == 4)
            {
                drum1.SetActive(false);
                drum2.SetActive(false);
                drum3.SetActive(false);
                drum4.SetActive(true);

                drum_anim1.GetChild(0).gameObject.SetActive(false);
                drum_anim1.GetChild(1).gameObject.SetActive(false);
                drum_anim1.GetChild(2).gameObject.SetActive(false);
                drum_anim1.GetChild(3).gameObject.SetActive(true);

                anim201.SetActive(false);
                anim202.SetActive(false);
                anim203.SetActive(false);
                anim204.SetActive(true);

                anim301.SetActive(false);
                anim302.SetActive(false);
                anim303.SetActive(false);
                anim304.SetActive(true);

                anim401.SetActive(false);
                anim402.SetActive(false);
                anim403.SetActive(false);
                anim404.SetActive(true);

                anim501.SetActive(false);
                anim502.SetActive(false);
                anim503.SetActive(false);
                anim504.SetActive(true);
            }
        }
        if (selected == 2)
        {
            if (Inst_num[selected] == 1)
            {
                rdrum.GetChild(0).gameObject.SetActive(true);
                rdrum.GetChild(1).gameObject.SetActive(false);
                rdrum.GetChild(2).gameObject.SetActive(false);
                rdrum.GetChild(3).gameObject.SetActive(false);
            }
            else if (Inst_num[selected] == 2)
            {
                rdrum.GetChild(0).gameObject.SetActive(false);
                rdrum.GetChild(1).gameObject.SetActive(true);
                rdrum.GetChild(2).gameObject.SetActive(false);
                rdrum.GetChild(3).gameObject.SetActive(false);
            }
            else if (Inst_num[selected] == 3)
            {
                rdrum.GetChild(0).gameObject.SetActive(false);
                rdrum.GetChild(1).gameObject.SetActive(false);
                rdrum.GetChild(2).gameObject.SetActive(true);
                rdrum.GetChild(3).gameObject.SetActive(false);
            }
            else if (Inst_num[selected] == 4)
            {
                rdrum.GetChild(0).gameObject.SetActive(false);
                rdrum.GetChild(1).gameObject.SetActive(false);
                rdrum.GetChild(2).gameObject.SetActive(false);
                rdrum.GetChild(3).gameObject.SetActive(true);
            }
        }
    }
    public void Change_Inst_Prev(int selected)
    {

        Inst_num[selected] -= 1;
        if (Inst_num[selected] < 0)
            Inst_num[selected] = 0;
        Active_SelectedInst(selected, Inst_num[selected]);

        if (selected == 0)
        {
            if (Inst_num[selected] == 1)
            {
                ldrum.GetChild(0).gameObject.SetActive(true);
                ldrum.GetChild(1).gameObject.SetActive(false);
                ldrum.GetChild(2).gameObject.SetActive(false);
                ldrum.GetChild(3).gameObject.SetActive(false);
            }
            else if (Inst_num[selected] == 2)
            {
                ldrum.GetChild(0).gameObject.SetActive(false);
                ldrum.GetChild(1).gameObject.SetActive(true);
                ldrum.GetChild(2).gameObject.SetActive(false);
                ldrum.GetChild(3).gameObject.SetActive(false);
            }
            else if (Inst_num[selected] == 3)
            {
                ldrum.GetChild(0).gameObject.SetActive(false);
                ldrum.GetChild(1).gameObject.SetActive(false);
                ldrum.GetChild(2).gameObject.SetActive(true);
                ldrum.GetChild(3).gameObject.SetActive(false);
            }
            else if (Inst_num[selected] == 4)
            {
                ldrum.GetChild(0).gameObject.SetActive(false);
                ldrum.GetChild(1).gameObject.SetActive(false);
                ldrum.GetChild(2).gameObject.SetActive(false);
                ldrum.GetChild(3).gameObject.SetActive(true);
            }
        }
        if (selected == 1)
        {
            if (Inst_num[selected] == 1)
            {
                drum1.SetActive(true);
                drum2.SetActive(false);
                drum3.SetActive(false);
                drum4.SetActive(false);

                drum_anim1.GetChild(0).gameObject.SetActive(true);
                drum_anim1.GetChild(1).gameObject.SetActive(false);
                drum_anim1.GetChild(2).gameObject.SetActive(false);
                drum_anim1.GetChild(3).gameObject.SetActive(false);

                anim201.SetActive(true);
                anim202.SetActive(false);
                anim203.SetActive(false);
                anim204.SetActive(false);

                anim301.SetActive(true);
                anim302.SetActive(false);
                anim303.SetActive(false);
                anim304.SetActive(false);

                anim401.SetActive(true);
                anim402.SetActive(false);
                anim403.SetActive(false);
                anim404.SetActive(false);

                anim501.SetActive(true);
                anim502.SetActive(false);
                anim503.SetActive(false);
                anim504.SetActive(false);
            }
            else if (Inst_num[selected] == 2)
            {
                drum1.SetActive(false);
                drum2.SetActive(true);
                drum3.SetActive(false);
                drum4.SetActive(false);

                drum_anim1.GetChild(0).gameObject.SetActive(false);
                drum_anim1.GetChild(1).gameObject.SetActive(true);
                drum_anim1.GetChild(2).gameObject.SetActive(false);
                drum_anim1.GetChild(3).gameObject.SetActive(false);

                anim201.SetActive(false);
                anim202.SetActive(true);
                anim203.SetActive(false);
                anim204.SetActive(false);

                anim301.SetActive(false);
                anim302.SetActive(true);
                anim303.SetActive(false);
                anim304.SetActive(false);

                anim401.SetActive(false);
                anim402.SetActive(true);
                anim403.SetActive(false);
                anim404.SetActive(false);

                anim501.SetActive(false);
                anim502.SetActive(true);
                anim503.SetActive(false);
                anim504.SetActive(false);
            }
            else if (Inst_num[selected] == 3)
            {
                drum1.SetActive(false);
                drum2.SetActive(false);
                drum3.SetActive(true);
                drum4.SetActive(false);

                drum_anim1.GetChild(0).gameObject.SetActive(false);
                drum_anim1.GetChild(1).gameObject.SetActive(false);
                drum_anim1.GetChild(2).gameObject.SetActive(true);
                drum_anim1.GetChild(3).gameObject.SetActive(false);

                anim201.SetActive(false);
                anim202.SetActive(false);
                anim203.SetActive(true);
                anim204.SetActive(false);

                anim301.SetActive(false);
                anim302.SetActive(false);
                anim303.SetActive(true);
                anim304.SetActive(false);

                anim401.SetActive(false);
                anim402.SetActive(false);
                anim403.SetActive(true);
                anim404.SetActive(false);

                anim501.SetActive(false);
                anim502.SetActive(false);
                anim503.SetActive(true);
                anim504.SetActive(false);
            }
            else if (Inst_num[selected] == 4)
            {
                drum1.SetActive(false);
                drum2.SetActive(false);
                drum3.SetActive(false);
                drum4.SetActive(true);

                drum_anim1.GetChild(0).gameObject.SetActive(false);
                drum_anim1.GetChild(1).gameObject.SetActive(false);
                drum_anim1.GetChild(2).gameObject.SetActive(false);
                drum_anim1.GetChild(3).gameObject.SetActive(true);

                anim201.SetActive(false);
                anim202.SetActive(false);
                anim203.SetActive(false);
                anim204.SetActive(true);

                anim301.SetActive(false);
                anim302.SetActive(false);
                anim303.SetActive(false);
                anim304.SetActive(true);

                anim401.SetActive(false);
                anim402.SetActive(false);
                anim403.SetActive(false);
                anim404.SetActive(true);

                anim501.SetActive(false);
                anim502.SetActive(false);
                anim503.SetActive(false);
                anim504.SetActive(true);
            }
        }
        if (selected == 2)
        {
            if (Inst_num[selected] == 1)
            {
                rdrum.GetChild(0).gameObject.SetActive(true);
                rdrum.GetChild(1).gameObject.SetActive(false);
                rdrum.GetChild(2).gameObject.SetActive(false);
                rdrum.GetChild(3).gameObject.SetActive(false);
            }
            else if (Inst_num[selected] == 2)
            {
                rdrum.GetChild(0).gameObject.SetActive(false);
                rdrum.GetChild(1).gameObject.SetActive(true);
                rdrum.GetChild(2).gameObject.SetActive(false);
                rdrum.GetChild(3).gameObject.SetActive(false);
            }
            else if (Inst_num[selected] == 3)
            {
                rdrum.GetChild(0).gameObject.SetActive(false);
                rdrum.GetChild(1).gameObject.SetActive(false);
                rdrum.GetChild(2).gameObject.SetActive(true);
                rdrum.GetChild(3).gameObject.SetActive(false);
            }
            else if (Inst_num[selected] == 4)
            {
                rdrum.GetChild(0).gameObject.SetActive(false);
                rdrum.GetChild(1).gameObject.SetActive(false);
                rdrum.GetChild(2).gameObject.SetActive(false);
                rdrum.GetChild(3).gameObject.SetActive(true);
            }
        }
    }
    void Active_SelectedInst(int selected, int num)
    {
        GameObject SelectedGroup = Inst_group[selected];
        GameObject SelectedInst;

        for (int i = 0; i < SelectedGroup.transform.childCount; i++)
        {
            SelectedInst = SelectedGroup.transform.GetChild(i).gameObject;
            SelectedInst.SetActive(false);
        }
        SelectedGroup.transform.GetChild(num).gameObject.SetActive(true);
    }
    public void Inactive_BGM()
    {
        Tool_BGM_UI.SetActive(false);
    }
    public void Active_BGM()
    {
        Tool_BGM_UI.SetActive(true);
        Tool_Inst_UI.SetActive(false);

        Tool_TestObject.SetActive(false);
        Init();
    }
    public void Change_BGM_Next()
    {
        BGM_num += 1;
        if (BGM_num > 3)
            BGM_num = 0;
        Change_Text_Music();
    }
    public void Change_BGM_Prev()
    {
        BGM_num -= 1;
        if (BGM_num < 0)
            BGM_num = 3;
        Change_Text_Music();
    }
    void Change_Text_Music()
    {
        if (BGM_num == 0)
        {
            Text_Music.text = "활기찬 스타일";
        }
        else if (BGM_num == 1)
        {
            Text_Music.text = "예시 0";
        }
        else if (BGM_num == 2)
        {
            Text_Music.text = "예시 1";
        }
        else if (BGM_num == 3)
        {
            Text_Music.text = "예시 2";
        }
    }
    public void Play_BGM()
    {
        Manager_audio.instance.Set_BGM_Audiosource(BGM_num);
    }

}