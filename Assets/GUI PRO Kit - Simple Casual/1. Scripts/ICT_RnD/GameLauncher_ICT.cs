using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;
using NRKernal;
using NRKernal.NRExamples;
using UnityEngine.EventSystems;

public class GameLauncher_ICT : MonoBehaviour
{
    public GameObject ICT_RnD_UI;
    private GameObject Loading;
    private GameObject Home;
    private GameObject Setting;
    private GameObject Login;
    private GameObject Tool;
    private GameObject Result;
    private GameObject Contents;
    private GameObject Mode;
    private GameObject Survey;
    private GameObject Monitoring_Music1;
    private GameObject Monitoring_Music2;
    private GameObject Monitoring_Music3;
    private GameObject Monitoring_Music4;
    private GameObject Monitoring_C1;
    private GameObject Monitoring_C2;
    private GameObject Monitoring_C3;
    private GameObject Monitoring_C4;

    public GameObject Message_UI;
    private GameObject Message_Tool;
    private GameObject Message_Content_StudentCheck;
    private GameObject Message_Intro;
    private GameObject Message_L_StudentCheck;
    private GameObject Message_L_FieldEmpty;
    private GameObject Message_L_StudentDataSaved;
    private GameObject Message_L_SelectedStudentCheck;
    private GameObject Message_L_Nonselect;
    private GameObject Message_Survey_StudentCheck;
    private GameObject Message_Answer_NonSelected;

    private GameObject Message_EndMusicContent;
    private Message_anim_controller MAC;

    private GameObject Prev_page;
    private GameObject Next_page;
    private bool Is_Toolsaved = false;

    private EventSystem eventSystem;

    [Header("[NO SCENE]")]
    public GameObject no;

    // ���ǳ��� bgm + ����ȯ��
    [Header("[Music BGM & LYRICS & ENVIRONMENT]")]
    public AudioSource Warm;
    public AudioSource Carrot;
    public AudioSource Corn;
    public AudioSource Aloe;

    public GameObject Warm_Scene;
    public GameObject Carrot_Scene;
    public GameObject Corn_Scene;
    public GameObject Aloe_Scene;

    public GameObject dec_final;
    public GameObject final_active;

    public HandModelsManager handModelsManager;

    // ü����� ����ȯ��
    [Header("[Experience ENVIRONMENT]")]
    public GameObject Worm_Background;
    public GameObject Carrot_Background;
    public GameObject Corn_Background;
    public GameObject Aloe_Background;

    public GameObject ARUI;
    private GameObject Message_Intro_AR;

    private Message_anim_controller MAC2;

    AudioSource audioSource;
    public AudioClip[] audios1;
    public AudioClip[] audios2;
    public AudioClip[] audios3;
    public AudioClip[] audios4;

    // �̴ϰ��� ����ȯ��
    [Header("[Minigame ENVIRONMENT]")]
    //public GameObject Worm_Background;
    public GameObject Carrot_mini;
    //public GameObject Corn_Background;
    public GameObject Aloe_mini;
    public GameObject Worm_eat;
    public GameObject Worm_mini;

    [Header("[Carrot UI]")]
    public GameObject leftC;
    public GameObject rightC;
    public GameObject Horse;

    public GameObject SC1;
    public GameObject SC2;
    public GameObject SC3;
    public GameObject BC1;
    public GameObject BC2;
    public GameObject BC3;
    public GameObject actt;

    [Header("[Aloe UI]")]
    public GameObject AL1;

    public Text BAL;
    public Text SAL;
    public Text CBAL;
    public Text CSAL;

    public GameObject accct;

    public GameObject Cutal1;
    public GameObject Cutal2;
    public GameObject Cutal3;
    public GameObject ccutal1;
    public GameObject ccutal2;
    public GameObject ccutal3;

    public GameObject GK1;
    public GameObject GK2;
    public GameObject GK3;
    public GameObject gk1;
    public GameObject gk2;
    public GameObject gk3;

    public GameObject bal1;
    public GameObject bal2;
    public GameObject bal3;
    public GameObject sal1;
    public GameObject sal2;
    public GameObject sal3;

    public GameObject bbal1;
    public GameObject bbal2;
    public GameObject bbal3;
    public GameObject ssal1;
    public GameObject ssal2;
    public GameObject ssal3;

    public GameObject aloe_active;

    public GameObject rightR;


    // Start is called before the first frame update
    [Header("[LOADING PAGE COMPONENT]")]
    [SerializeField]
    public Slider progressBar;
    public Text loadingPercent;
    public Image loadingIcon;

    private bool loadingCompleted;
    private int nextScene;

    public int Session;

    //BT ��ſ�
    private BT_Comm bluetoothHelper;

    /*//0324
    private int[] Inst_SavedNum = new int[3] { 0, 1, 0 };

    [Header("[Drum Object]")]
    public GameObject drum1;
    public GameObject drum2;
    public GameObject drum3;
    public GameObject drum4;*/

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(LoadScene());
        StartCoroutine(RotateIcon());

        loadingCompleted = false;
        nextScene = 0;
        Init_page();

        audioSource = GetComponent<AudioSource>();

        bluetoothHelper = BT_Comm.Instance;
    }

    IEnumerator LoadScene()
    {
        //yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        //while (!op.isDone)
        while (true)
        {
            //yield return null;

            timer += Time.deltaTime;

            if (op.progress >= 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);
                loadingPercent.text = "progressBar.value";

                if (progressBar.value == 1.0f)
                    op.allowSceneActivation = true;
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, op.progress, timer);
                if (progressBar.value >= op.progress)
                {
                    timer = 0f;

                    //End of scene index
                    if (nextScene == 2 && loadingCompleted)
                    {
                        StopAllCoroutines();
                    }
                }
            }
        }
    }

    IEnumerator RotateIcon()
    {
        float timer = 0f;
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            timer += Time.deltaTime;

            //Debug.Log(progressBar.value);
            //Debug.Log("check");
            if (progressBar.value < 100f)
            {
                progressBar.value = Mathf.RoundToInt(Mathf.Lerp(progressBar.value, 100f, timer / 8));
                loadingIcon.rectTransform.Rotate(new Vector3(0, 0, 100 * Time.deltaTime));
                loadingPercent.text = progressBar.value.ToString();
            }
            else
            {
                StopAllCoroutines();
                //Debug.Log("100%");

                Next_page = Home;
                UI_change();

                //Loading.SetActive(false);
                ////Mode.SetActive(true);
                //Home.SetActive(true);
            }
        }
    }

    public void UI_change()
    {
        GameObject page;
        for (int i = 0; i < ICT_RnD_UI.transform.childCount; i++)
        {
            page = ICT_RnD_UI.transform.GetChild(i).gameObject;
            if (page.gameObject.activeSelf)
            {
                Prev_page = page.gameObject;
                //Debug.Log(Prev_page);
            }
        }
        Prev_page.SetActive(false);
        Next_page.SetActive(true);
    }

    public void Button_Save_Tool()
    {
        //���۵��� ���忩��
        Is_Toolsaved = true;

        Next_page = Home;
        UI_change();
    }
    public void Button_Back_ToHome()
    {
        handModelsManager.ToggleHandModelsGroup(0);

        Next_page = Home;
        UI_change();

        Warm.Stop();
        Carrot.Stop();
        Corn.Stop();
        Aloe.Stop();

        no.SetActive(true);

        Worm_Background.SetActive(false);
        Carrot_Background.SetActive(false);
        Corn_Background.SetActive(false);
        Aloe_Background.SetActive(false);

        Warm_Scene.SetActive(false);
        Carrot_Scene.SetActive(false);
        Corn_Scene.SetActive(false);
        Aloe_Scene.SetActive(false);

        Carrot_mini.SetActive(false);
        Aloe_mini.SetActive(false);
        Worm_eat.SetActive(false);
        Worm_mini.SetActive(false);

        leftC.SetActive(false);
        AL1.SetActive(false);

        MAC2.Animation_Off();
        StopAllCoroutines();

        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('Q');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes(0x00);
        byte[] c = BitConverter.GetBytes(0x00);

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x00, 0x00, 0xFE };

        bluetoothHelper.SendData(bytestosend);
    }
    public void Button_Back_ToContent()
    {
        Next_page = Contents;
        UI_change();
    }
    public void Button_Back_ToMode()
    {
        handModelsManager.ToggleHandModelsGroup(0);

        Next_page = Mode;
        UI_change();

        Warm.Stop();
        Carrot.Stop();
        Corn.Stop();
        Aloe.Stop();

        no.SetActive(true);

        Worm_Background.SetActive(false);
        Carrot_Background.SetActive(false);
        Corn_Background.SetActive(false);
        Aloe_Background.SetActive(false);

        Warm_Scene.SetActive(false);
        Carrot_Scene.SetActive(false);
        Corn_Scene.SetActive(false);
        Aloe_Scene.SetActive(false);

        Carrot_mini.SetActive(false);
        Aloe_mini.SetActive(false);
        Worm_eat.SetActive(false);
        Worm_mini.SetActive(false);

        leftC.SetActive(false);
        AL1.SetActive(false);

        MAC2.Animation_Off();
        StopAllCoroutines();

        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('Q');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes(0x00);
        byte[] c = BitConverter.GetBytes(0x00);

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, b[0], c[0], 0xFE };

        bluetoothHelper.SendData(bytestosend);
    }
    public void Button_Setting()
    {
        Setting.SetActive(true);
    }

    public void Button_Setting_Close()
    {
        Setting.SetActive(false);
    }

    public void Button_Home()
    {
        handModelsManager.ToggleHandModelsGroup(0);

        //������ ���� ���� ��� �ش� ������ ��Ȱ��ȭ ��� ���� �ʿ�
        Next_page = Home;
        UI_change();

        Warm.Stop();
        Carrot.Stop();
        Corn.Stop();
        Aloe.Stop();

        no.SetActive(true);

        Worm_Background.SetActive(false);
        Carrot_Background.SetActive(false);
        Corn_Background.SetActive(false);
        Aloe_Background.SetActive(false);

        Warm_Scene.SetActive(false);
        Carrot_Scene.SetActive(false);
        Corn_Scene.SetActive(false);
        Aloe_Scene.SetActive(false);

        Carrot_mini.SetActive(false);
        Aloe_mini.SetActive(false);
        Worm_eat.SetActive(false);
        Worm_mini.SetActive(false);

        leftC.SetActive(false);
        AL1.SetActive(false);

        MAC2.Animation_Off();

        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('Q');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes(0x00);
        byte[] c = BitConverter.GetBytes(0x00);

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, b[0], c[0], 0xFE };

        bluetoothHelper.SendData(bytestosend);
    }
    public void Button_Tool()
    {
        Next_page = Tool;
        UI_change();
    }
    public void Button_Result()
    {
        Next_page = Result;
        UI_change();
        //Manager_Result.instance.Refresh_data();
        Manager_ffinall.instance.Refresh_data();
    }
    public void Button_BR()
    {
        Manager_ffinall.instance.Refresh_data();
    }
    public void Button_Contents()
    {
        bool Is_Logindatasaved = Manager_login.instance.Get_Islogindatasaved();

        if (Is_Logindatasaved)
        {
            Next_page = Contents;
            UI_change();
        }
        else
        {
            Message_Content_StudentCheck.SetActive(true);
        }
    }
    public void Button_Mode(int num_mode)
    {
        //0 : Music, 1 : Contents
        if (num_mode == 0)
        {
            Run_Music_Contents();
        }
        else if (num_mode == 1)
        {
            Run_Contents();
        }
    }

    public void Button_End_Musiccontent()
    {
        //dec_final.SetActive(true);
        //final_active.SetActive(true);
        //���ǳ��� ������ ����
        Message_EndMusicContent.SetActive(true);
        Manager_ResultInDetail.instance.Save_RIDdata(Session);

        //handModelsManager.ToggleHandModelsGroup(0);

        if (Session == 0)
        {
            Warm.Stop();
            Carrot.Stop();
            Corn.Stop();
            Aloe.Stop();
            GameObject music1 = GameObject.FindGameObjectWithTag("Music1");
            Trigger_accordion script1 = music1.GetComponent<Trigger_accordion>();
            script1.init();

            handModelsManager.ToggleHandModelsGroup(0);

            no.SetActive(true);

            Worm_Background.SetActive(false);
            Carrot_Background.SetActive(false);
            Corn_Background.SetActive(false);
            Aloe_Background.SetActive(false);

            Warm_Scene.SetActive(false);
            Carrot_Scene.SetActive(false);
            Corn_Scene.SetActive(false);
            Aloe_Scene.SetActive(false);

            Carrot_mini.SetActive(false);
            Aloe_mini.SetActive(false);

            char temp_mode_stx = '<';
            char temp_mode_etx = '>';
            byte[] af = BitConverter.GetBytes(temp_mode_stx);
            byte[] a = BitConverter.GetBytes('Q');
            byte[] al = BitConverter.GetBytes(temp_mode_etx);
            byte[] b = BitConverter.GetBytes(0x00);
            byte[] c = BitConverter.GetBytes(0x00);

            byte[] bytestosend = { af[0], a[0], al[0], 0xFF, b[0], c[0], 0xFE };

            bluetoothHelper.SendData(bytestosend);
        }
        else if (Session == 1)
        {
            Warm.Stop();
            Carrot.Stop();
            Corn.Stop();
            Aloe.Stop();
            GameObject music2 = GameObject.FindGameObjectWithTag("Music2");
            Trigger_ddrum script2 = music2.GetComponent<Trigger_ddrum>();
            script2.init();

            handModelsManager.ToggleHandModelsGroup(0);

            no.SetActive(true);

            Worm_Background.SetActive(false);
            Carrot_Background.SetActive(false);
            Corn_Background.SetActive(false);
            Aloe_Background.SetActive(false);

            Warm_Scene.SetActive(false);
            Carrot_Scene.SetActive(false);
            Corn_Scene.SetActive(false);
            Aloe_Scene.SetActive(false);

            Carrot_mini.SetActive(false);
            Aloe_mini.SetActive(false);

            char temp_mode_stx = '<';
            char temp_mode_etx = '>';
            byte[] af = BitConverter.GetBytes(temp_mode_stx);
            byte[] a = BitConverter.GetBytes('Q');
            byte[] al = BitConverter.GetBytes(temp_mode_etx);
            byte[] b = BitConverter.GetBytes(0x00);
            byte[] c = BitConverter.GetBytes(0x00);

            byte[] bytestosend = { af[0], a[0], al[0], 0xFF, b[0], c[0], 0xFE };

            bluetoothHelper.SendData(bytestosend);
        }
        else if (Session == 2)
        {
            Warm.Stop();
            Carrot.Stop();
            Corn.Stop();
            Aloe.Stop();
            GameObject music3 = GameObject.FindGameObjectWithTag("Kiwro");
            Trigger_kkiwro script3 = music3.GetComponent<Trigger_kkiwro>();
            script3.init();

            handModelsManager.ToggleHandModelsGroup(0);

            no.SetActive(true);

            Worm_Background.SetActive(false);
            Carrot_Background.SetActive(false);
            Corn_Background.SetActive(false);
            Aloe_Background.SetActive(false);

            Warm_Scene.SetActive(false);
            Carrot_Scene.SetActive(false);
            Corn_Scene.SetActive(false);
            Aloe_Scene.SetActive(false);

            Carrot_mini.SetActive(false);
            Aloe_mini.SetActive(false);

            char temp_mode_stx = '<';
            char temp_mode_etx = '>';
            byte[] af = BitConverter.GetBytes(temp_mode_stx);
            byte[] a = BitConverter.GetBytes('Q');
            byte[] al = BitConverter.GetBytes(temp_mode_etx);
            byte[] b = BitConverter.GetBytes(0x00);
            byte[] c = BitConverter.GetBytes(0x00);

            byte[] bytestosend = { af[0], a[0], al[0], 0xFF, b[0], c[0], 0xFE };

            bluetoothHelper.SendData(bytestosend);
        }
        else if (Session == 3)
        {
            Warm.Stop();
            Carrot.Stop();
            Corn.Stop();
            Aloe.Stop();
            GameObject music4 = GameObject.FindGameObjectWithTag("Music4");
            Trigger_rain script4 = music4.GetComponent<Trigger_rain>();
            script4.init();

            handModelsManager.ToggleHandModelsGroup(0);

            no.SetActive(true);

            Worm_Background.SetActive(false);
            Carrot_Background.SetActive(false);
            Corn_Background.SetActive(false);
            Aloe_Background.SetActive(false);

            Warm_Scene.SetActive(false);
            Carrot_Scene.SetActive(false);
            Corn_Scene.SetActive(false);
            Aloe_Scene.SetActive(false);

            Carrot_mini.SetActive(false);
            Aloe_mini.SetActive(false);

            char temp_mode_stx = '<';
            char temp_mode_etx = '>';
            byte[] af = BitConverter.GetBytes(temp_mode_stx);
            byte[] a = BitConverter.GetBytes('Q');
            byte[] al = BitConverter.GetBytes(temp_mode_etx);
            byte[] b = BitConverter.GetBytes(0x00);
            byte[] c = BitConverter.GetBytes(0x00);

            byte[] bytestosend = { af[0], a[0], al[0], 0xFF, b[0], c[0], 0xFE };

            bluetoothHelper.SendData(bytestosend);
        }

        dec_final.SetActive(true);
        final_active.SetActive(true);
    }

    public void Run_Mode(int contentname)
    {
        Session = contentname;

        Next_page = Mode;
        UI_change();
    }
    public void Run_Music_Contents()
    {
        /*        Next_page = Monitoring_Music;
                UI_change();
                Manager_ResultInDetail.instance.Clear_RIDdata();*/

        if (Session == 0)
        {
            Next_page = Monitoring_Music1;

            Warm.mute = false;
            Carrot.mute = true;
            Corn.mute = true;
            Aloe.mute = true;

            Warm_Scene.SetActive(true);
            Carrot_Scene.SetActive(false);
            Corn_Scene.SetActive(false);
            Aloe_Scene.SetActive(false);

            Worm_Background.SetActive(false);
            Carrot_Background.SetActive(false);
            Corn_Background.SetActive(false);
            Aloe_Background.SetActive(false);

            Carrot_mini.SetActive(false);
            Aloe_mini.SetActive(false);

            handModelsManager.ToggleHandModelsGroup(1);
        }
        else if (Session == 1)
        {
            Next_page = Monitoring_Music2;

            Warm.mute = true;
            Carrot.mute = false;
            Corn.mute = true;
            Aloe.mute = true;

            Warm_Scene.SetActive(false);
            Carrot_Scene.SetActive(true);
            Corn_Scene.SetActive(false);
            Aloe_Scene.SetActive(false);

            Worm_Background.SetActive(false);
            Carrot_Background.SetActive(false);
            Corn_Background.SetActive(false);
            Aloe_Background.SetActive(false);

            Carrot_mini.SetActive(false);
            Aloe_mini.SetActive(false);

            handModelsManager.ToggleHandModelsGroup(2);
        }
        else if (Session == 2)
        {
            Next_page = Monitoring_Music3;

            Warm.mute = true;
            Carrot.mute = true;
            Corn.mute = false;
            Aloe.mute = true;

            Warm_Scene.SetActive(false);
            Carrot_Scene.SetActive(false);
            Corn_Scene.SetActive(true);
            Aloe_Scene.SetActive(false);

            Worm_Background.SetActive(false);
            Carrot_Background.SetActive(false);
            Corn_Background.SetActive(false);
            Aloe_Background.SetActive(false);

            Carrot_mini.SetActive(false);
            Aloe_mini.SetActive(false);

            handModelsManager.ToggleHandModelsGroup(3);
        }
        else if (Session == 3)
        {
            Next_page = Monitoring_Music4;

            Warm.mute = true;
            Carrot.mute = true;
            Corn.mute = true;
            Aloe.mute = false;

            Warm_Scene.SetActive(false);
            Carrot_Scene.SetActive(false);
            Corn_Scene.SetActive(false);
            Aloe_Scene.SetActive(true);

            Worm_Background.SetActive(false);
            Carrot_Background.SetActive(false);
            Corn_Background.SetActive(false);
            Aloe_Background.SetActive(false);

            Carrot_mini.SetActive(false);
            Aloe_mini.SetActive(false);

            handModelsManager.ToggleHandModelsGroup(4);
        }
        UI_change();
        Manager_ResultInDetail.instance.Clear_RIDdata();
    }

    public void Button_Music_Play()
    {
        //�ش� ���� ������ ��� ��� ���� �ʿ�
        if (Session == 0)
        {
            Warm.Play();
            GameObject music1 = GameObject.FindGameObjectWithTag("Music1");
            Trigger_accordion script1 = music1.GetComponent<Trigger_accordion>();
            script1.init();
        }
        else if (Session == 1)
        {
            Carrot.Play();
            GameObject music2 = GameObject.FindGameObjectWithTag("Music2");
            Trigger_ddrum script2 = music2.GetComponent<Trigger_ddrum>();
            script2.init();
        }
        else if (Session == 2)
        {
            Corn.Play();
            GameObject music3 = GameObject.FindGameObjectWithTag("Kiwro");
            Trigger_kkiwro script3 = music3.GetComponent<Trigger_kkiwro>();
            script3.init();
        }
        else if (Session == 3)
        {
            Aloe.Play();
            GameObject music4 = GameObject.FindGameObjectWithTag("Music4");
            Trigger_rain script4 = music4.GetComponent<Trigger_rain>();
            script4.init();
        }
    }

    public void Button_Music_Replay()
    {
        if (Session == 0)
        {
            Warm.Play();
            GameObject music1 = GameObject.FindGameObjectWithTag("Music1");
            Trigger_accordion script1 = music1.GetComponent<Trigger_accordion>();
            script1.init();
        }
        else if (Session == 1)
        {
            Carrot.Play();
            GameObject music2 = GameObject.FindGameObjectWithTag("Music2");
            Trigger_ddrum script2 = music2.GetComponent<Trigger_ddrum>();
            script2.init();
        }
        else if (Session == 2)
        {
            Corn.Play();
            GameObject music3 = GameObject.FindGameObjectWithTag("Kiwro");
            Trigger_kkiwro script3 = music3.GetComponent<Trigger_kkiwro>();
            script3.init();
        }
        else if (Session == 3)
        {
            Aloe.Play();
            GameObject music4 = GameObject.FindGameObjectWithTag("Music4");
            Trigger_rain script4 = music4.GetComponent<Trigger_rain>();
            script4.init();
        }
    }

    public void Button_Music_Stop()
    {
        if (Session == 0)
        {
            Warm.Stop();
            GameObject music1 = GameObject.FindGameObjectWithTag("Music1");
            Trigger_accordion script1 = music1.GetComponent<Trigger_accordion>();
            script1.init();
        }
        else if (Session == 1)
        {
            Carrot.Stop();
            GameObject music2 = GameObject.FindGameObjectWithTag("Music2");
            Trigger_ddrum script2 = music2.GetComponent<Trigger_ddrum>();
            script2.init();
        }
        else if (Session == 2)
        {
            Corn.Stop();
            GameObject music3 = GameObject.FindGameObjectWithTag("Kiwro");
            Trigger_kkiwro script3 = music3.GetComponent<Trigger_kkiwro>();
            script3.init();
        }
        else if (Session == 3)
        {
            Aloe.Stop();
            GameObject music4 = GameObject.FindGameObjectWithTag("Music4");
            Trigger_rain script4 = music4.GetComponent<Trigger_rain>();
            script4.init();
        }
    }

    public void Button_Music_Analysis()
    {

        Debug.Log("Analysis " + "Session : " + Session);
        if (Session == 0)
        {

        }
        else if (Session == 1)
        {

        }
        else if (Session == 2)
        {

        }
        else if (Session == 3)
        {

        }
    }

    public void Button_Music_Listening()
    {
        if (Session == 0)
        {
            Warm.Play();
        }
        else if (Session == 1)
        {
            Carrot.Play();
        }
        else if (Session == 2)
        {
            Corn.Play();
        }
        else if (Session == 3)
        {
            Aloe.Play();
        }
    }
    public void Run_Contents()
    {
        //���� ��ȯ
        Is_Toolsaved = false;

        //�ش� ������ ���� ���� ��� ����
        Dummy_setting_content();

        //Message_Intro setting
        Message_Intro.SetActive(true);

        // �̺�Ʈ �ý��� ��������
        eventSystem = EventSystem.current;

        if (Session == 0)
        {

            handModelsManager.ToggleHandModelsGroup(7);

            Worm_Background.SetActive(true);
            Carrot_Background.SetActive(false);
            Corn_Background.SetActive(false);
            Aloe_Background.SetActive(false);

            Warm_Scene.SetActive(false);
            Carrot_Scene.SetActive(false);
            Corn_Scene.SetActive(false);
            Aloe_Scene.SetActive(false);

            Carrot_mini.SetActive(false);
            Aloe_mini.SetActive(false);

            Next_page = Monitoring_C1;
            MAC.Change_text("�ȳ��ϼ���, ������! �̹� �ð����� �츮�� ��ҿ� �� ���� ���ϴ� ���� ����ü, �ɺ��̿� ���� �˾ƺ����� �ؿ�.");
            //MAC.Animation_On();

            MAC2.Change_text("�ȳ��ϼ���, ������! �̹� �ð����� �츮�� ��ҿ� �� ���� ���ϴ� ���� ����ü, �ɺ��̿� ���� �˾ƺ����� �ؿ�.");
            MAC2.Animation_On();
            MAC2.Change_size(48);

            StartCoroutine(Next_TextCoroutine(Session));
            StartCoroutine("Play_1"); //Audio �ڷ�ƾ
            eventSystem.enabled = false;

        }
        else if (Session == 1)
        {
            handModelsManager.ToggleHandModelsGroup(8);

            Carrot_Background.SetActive(true);
            Worm_Background.SetActive(false);
            Corn_Background.SetActive(false);
            Aloe_Background.SetActive(false);

            Warm_Scene.SetActive(false);
            Carrot_Scene.SetActive(false);
            Corn_Scene.SetActive(false);
            Aloe_Scene.SetActive(false);

            Carrot_mini.SetActive(false);
            Aloe_mini.SetActive(false);

            Next_page = Monitoring_C2;
            MAC.Change_text("��ȫ���� ���� �Ƹ��ٿ� ����� ����� �������� �ʴ��մϴ�. �Բ� ����� �ŷ¿� ���������?");
            MAC2.Change_text("��ȫ���� ���� �Ƹ��ٿ� ����� ����� �������� �ʴ��մϴ�. �Բ� ����� �ŷ¿� ���������?");
            MAC2.Change_size(48);

            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine(Session));

            StartCoroutine("Play_2");
            eventSystem.enabled = false;
        }
        else if (Session == 2)
        {
            handModelsManager.ToggleHandModelsGroup(8);

            Corn_Background.SetActive(true);
            Worm_Background.SetActive(false);
            Carrot_Background.SetActive(false);
            Aloe_Background.SetActive(false);

            Warm_Scene.SetActive(false);
            Carrot_Scene.SetActive(false);
            Corn_Scene.SetActive(false);
            Aloe_Scene.SetActive(false);

            Carrot_mini.SetActive(false);
            Aloe_mini.SetActive(false);

            Next_page = Monitoring_C3;
            MAC.Change_text("�ȳ��ϼ���! �̹����� �������� �ź�ο� ����� �����ϴ�! ���������� �������� �����ϰ� ������, �������� Ư¡�� ��������� �Բ� �˾ƺ� �ſ���.");
            MAC2.Change_text("�ȳ��ϼ���! �̹����� �������� �ź�ο� ����� �����ϴ�! ���������� �������� �����ϰ� ������, �������� Ư¡�� ��������� �Բ� �˾ƺ� �ſ���.");
            MAC2.Change_size(40);

            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine(Session));
            StartCoroutine("Play_3");
            eventSystem.enabled = false;
        }
        else if (Session == 3)
        {
            handModelsManager.ToggleHandModelsGroup(8);

            Aloe_Background.SetActive(true);
            Worm_Background.SetActive(false);
            Carrot_Background.SetActive(false);
            Corn_Background.SetActive(false);

            Warm_Scene.SetActive(false);
            Carrot_Scene.SetActive(false);
            Corn_Scene.SetActive(false);
            Aloe_Scene.SetActive(false);

            Carrot_mini.SetActive(false);
            Aloe_mini.SetActive(false);

            Next_page = Monitoring_C4;
            MAC.Change_text("�̹����� �˷ο��� �ź�ο� ����� ������ �ð��Դϴ�.  �˷ο��� �Ƹ��ٿ� ���� ������� �ڶ��ٴµ�,");
            MAC2.Change_text("�̹����� �˷ο��� �ź�ο� ����� ������ �ð��Դϴ�.  �˷ο��� �Ƹ��ٿ� ���� ������� �ڶ��ٴµ�,");
            MAC2.Change_size(48);

            //MAC.Animation_On();
            MAC2.Animation_On();

            StartCoroutine(Next_TextCoroutine(Session));
            StartCoroutine("Play_4");
            eventSystem.enabled = false;
        }
        UI_change();
        //SceneManager.LoadSceneAsync(1);
    }

    IEnumerator Next_TextCoroutine(int Session)
    {
        switch (Session)
        {
            case 0:
                yield return new WaitForSeconds(10.1f);
                MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            case 1:
                yield return new WaitForSeconds(9.1f);
                MAC.Animation_Off();
                MAC2.Animation_Off();
                StartCoroutine(EventSystem_true());
                break;
            case 2:
                yield return new WaitForSeconds(14.1f);
                MAC.Animation_Off();
                MAC2.Animation_Off();
                StartCoroutine(EventSystem_true());
                break;
            case 3:
                yield return new WaitForSeconds(8.1f);
                MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            case 4:
                yield return new WaitForSeconds(8.0f);
                MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
        }
        //yield return new WaitForSeconds(8.3f); // ���ϴ� ��� �ð� ����
        StartCoroutine(Delay_BTW_Text(Session));
    }
    IEnumerator EventSystem_true()
    {
        yield return new WaitForSeconds(1.5f);
        eventSystem.enabled = true;
    }
    IEnumerator Delay_BTW_Text(int Session)
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Next_Text(Session));
    }
    IEnumerator Next_Text(int Session)
    {
        if (Session == 0)
        {
            MAC.Change_text("���� �ɺ��̰� ��� �ڶ�� ��ȭ�ϸ� ��ư����� �Բ� �˾ƺ����� �ҰԿ�. �غ�Ǽ̳���?");
            MAC2.Change_text("���� �ɺ��̰� ��� �ڶ�� ��ȭ�ϸ� ��ư����� �Բ� �˾ƺ����� �ҰԿ�. �غ�Ǽ̳���?");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(8.3f);
            MAC.Animation_Off();
            MAC2.Animation_Off();
            StartCoroutine(EventSystem_true());
        }
        if (Session == 1)
        {

        }
        if (Session == 2)
        {
            //yield return new WaitForSeconds(6f);
        }
        if (Session == 3)
        {
            MAC.Change_text("�˷ο� ������ �����ϸ� �� Ư���� ���������� ������ �پ��� Ư¡�� ���캸���� �ϰڽ��ϴ�. " +
               "\n�Բ� �˷ο��� ���迡 ���������?");
            MAC2.Change_text("�˷ο� ������ �����ϸ� �� Ư���� ���������� ������ �پ��� Ư¡�� ���캸���� �ϰڽ��ϴ�. " +
               "\n�Բ� �˷ο��� ���迡 ���������?");
            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(12.3f);
            MAC.Animation_Off();
            MAC2.Animation_Off();
            StartCoroutine(EventSystem_true());
        }
    }
    public void Run_Contents_Func(int content_func)
    {
        //�ٸ� ������ ���α�� ���� ���ΰ� ��Ȱ��ȭ
        //�� ������ ���� ��� �����ų��

        Message_Intro.SetActive(true);
        //C1
        if (content_func == 0)
        {
            handModelsManager.ToggleHandModelsGroup(7);
            Worm_Background.SetActive(true);

            //�ش� ������ ���� ��� ����
            MAC.Change_text("����, �츮�� �ɺ����� ������� ���캼 �ſ���. �� �ٴ� ������ ��Ʋ��Ʋ �Ÿ��� �ɺ����� ����� ���� �ű��ؿ�!");
            MAC2.Change_text("����, �츮�� �ɺ����� ������� ���캼 �ſ���. �� �ٴ� ������ ��Ʋ��Ʋ �Ÿ��� �ɺ����� ����� ���� �ű��ؿ�!");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 1)
        {
            handModelsManager.ToggleHandModelsGroup(5);
            Worm_Background.SetActive(true);

            //�ش� ������ ���� ��� ����
            MAC.Change_text("�ɺ����� ��Ʋ��Ʋ�� �˰��� ���������?\r\n");
            MAC2.Change_text("�ɺ����� ��Ʋ��Ʋ�� �˰��� ���������?\r\n");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 2)
        {
            //�ش� ������ ���� ��� ����
            MAC.Change_text("(�׽�Ʈ)�ɺ��� ���̿� ���� �˾ƺ����?");
            MAC2.Change_text("�ɹ��̿��� �������� �ຸ����.\n�ɺ��̴� �������� �ֽ����� ��ƿ�.");
            MAC.Animation_On_Off();
            MAC2.Animation_On();

            handModelsManager.ToggleHandModelsGroup(11);
            Worm_Background.SetActive(true);

            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 3)
        {
            handModelsManager.ToggleHandModelsGroup(7);
            Worm_Background.SetActive(true);

            //�ش� ������ ���� ��� ����
            MAC.Change_text("���� �ɺ��̸� �� �ڼ��� �����غ��� ���� ũ�⸦ Ű�����Կ�~");
            MAC2.Change_text("���� �ɺ��̸� �� �ڼ��� �����غ��� ���� ũ�⸦ Ű�����Կ�~");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 4)
        {
            /*//�ش� ������ ���� ��� ����
            handModelsManager.ToggleHandModelsGroup(7);
            Worm_Background.SetActive(true);

            //�ش� ������ ���� ��� ����
            MAC.Change_text("�ɺ��̿��� �������� �ຸ����. �ɺ��̴� �������� �ֽ����� ��ƿ�.");
            MAC2.Change_text("�ɺ��̿��� �������� �ຸ����. �ɺ��̴� �������� �ֽ����� ��ƿ�.");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;*/
        }
        else if (content_func == 5)
        {
            handModelsManager.ToggleHandModelsGroup(7);
            Worm_Background.SetActive(true);

            //�ش� ������ ���� ��� ����
            MAC.Change_text("�ɹ����� ��Ȱ�縦 �˾ƺ����� �ϰڽ��ϴ�." +
                "�ɺ��̴� �˿������� �����ؿ�. \n���� ��Ʋ�Ÿ��׿�~");
            MAC2.Change_text("�ɹ����� ��Ȱ�縦 �˾ƺ����� �ϰڽ��ϴ�." +
                "�ɺ��̴� �˿������� �����ؿ�. \n���� ��Ʋ�Ÿ��׿�~");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 6)
        {
            //�ش� ������ ���� ��� ����
            //MAC.Change_text("(�׽�Ʈ)�ɺ��� ü��Ȱ���� �غ����?");
            MAC2.Change_text("��Ʋ�Ÿ��� �ɺ��̵��� ���� �� �ð��̿���.\n ������ ħ��� �Ű� �����?");
            //MAC.Animation_On_Off();
            MAC2.Animation_On();

            handModelsManager.ToggleHandModelsGroup(12);
            Worm_Background.SetActive(true);
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }//C2
        else if (content_func == 10)
        {
            Carrot_Background.SetActive(true);
            leftC.SetActive(false);
            Carrot_mini.SetActive(false);
            //�ش� ������ ���� ��� ����
            handModelsManager.ToggleHandModelsGroup(8);

            MAC.Change_text("����, ��ȫ������ ������ ����� ������� ���캾�ô�! ���� ����� ũ�� �پ��� ����, ������ ��Ư�� �ŷ��� ������ �־��!");
            MAC2.Change_text("����, ��ȫ������ ������ ����� ������� ���캾�ô�! ���� ����� ũ�� �پ��� ����, ������ ��Ư�� �ŷ��� ������ �־��!");

            //MAC.Animation_On();
            MAC2.Animation_On();

            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 11)
        {
            Carrot_Background.SetActive(true);
            leftC.SetActive(false);
            Carrot_mini.SetActive(false);
            //�ش� ������ ���� ��� ����
            handModelsManager.ToggleHandModelsGroup(6);

            MAC.Change_text("����� ������ �˰��� ���������?");
            MAC2.Change_text("����� ������ �˰��� ���������?");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 12)
        {
            Carrot_Background.SetActive(true);
            leftC.SetActive(false);
            Carrot_mini.SetActive(false);
            //�ش� ������ ���� ��� ����
            handModelsManager.ToggleHandModelsGroup(8);

            MAC.Change_text("������! ����� ��� �ڶ󳪴��� �˰� �ֳ���? ���� ��â, �泲 �о�, ����, ���ֽ� ������ �� �츮������ �پ��� �������� ����ǰ� �־��.");
            //MAC.Animation_On();

            MAC2.Change_text("������! ����� ��� �ڶ󳪴��� �˰� �ֳ���? ���� ��â, �泲 �о�, ����, ���ֽ� ������ �� �츮������ �پ��� �������� ����ǰ� �־��.");
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 13)
        {
            MAC2.Change_size(48);

            //�ش� ������ ���� ��� ����
            MAC.Change_text("������! �̹� �ð����� ������ ����� �ٰſ���. �غ� �Ǽ̳���?");
            MAC2.Change_text("������! �̹� �ð����� ������ ����� �ٰſ���. �غ� �Ǽ̳���?");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;

            Carrot_Background.SetActive(false);
            Carrot_mini.SetActive(true);

            rightC.SetActive(true);
            Horse.SetActive(true);
            Horse.transform.GetChild(0).gameObject.SetActive(false);
            Horse.transform.GetChild(1).gameObject.SetActive(false);

            leftC.SetActive(true);
            leftC.transform.GetChild(0).gameObject.SetActive(true);
            leftC.transform.GetChild(1).gameObject.SetActive(true);
            leftC.transform.GetChild(2).gameObject.SetActive(true);
            leftC.transform.GetChild(3).gameObject.SetActive(true);
            leftC.transform.GetChild(4).gameObject.SetActive(true);
            leftC.transform.GetChild(5).gameObject.SetActive(true);

            SC1.SetActive(true);
            SC2.SetActive(true);
            SC3.SetActive(true);
            BC1.SetActive(true);
            BC2.SetActive(true);
            BC3.SetActive(true);

            handModelsManager.ToggleHandModelsGroup(9);

            Transform B0 = GameObject.Find("B_Carrot_Step0").transform.Find("BC0");
            Transform B1 = GameObject.Find("B_Carrot_Step1").transform.Find("BC1");
            Transform B2 = GameObject.Find("B_Carrot_Step2").transform.Find("BC2");
            Transform B3 = GameObject.Find("B_Carrot_Step3").transform.Find("BC3");
            Transform S0 = GameObject.Find("S_Carrot_Step0").transform.Find("SC0");
            Transform S1 = GameObject.Find("S_Carrot_Step1").transform.Find("SC1");
            Transform S2 = GameObject.Find("S_Carrot_Step2").transform.Find("SC2");
            Transform S3 = GameObject.Find("S_Carrot_Step3").transform.Find("SC3");

            B0.gameObject.SetActive(true);
            B1.gameObject.SetActive(false);
            B2.gameObject.SetActive(false);
            B3.gameObject.SetActive(false);

            S0.gameObject.SetActive(true);
            S1.gameObject.SetActive(false);
            S2.gameObject.SetActive(false);
            S3.gameObject.SetActive(false);

        }//C3
        else if (content_func == 20)
        {
            handModelsManager.ToggleHandModelsGroup(7);
            MAC2.Change_size(48);
            //�ش� ������ ���� ��� ����
            MAC.Change_text("����, �������� ������� �ڼ��� ������ �����. Ű�� ū �������� Ȱ¦ ���� �������� ����� ���� �λ����̿���!");
            //MAC.Animation_On();

            MAC2.Change_text("����, �������� ������� �ڼ��� ������ �����. Ű�� ū �������� Ȱ¦ ���� �������� ����� ���� �λ����̿���!");
            MAC2.Animation_On();

            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;

        }
        else if (content_func == 21)
        {
            handModelsManager.ToggleHandModelsGroup(7);
            MAC2.Change_size(48);
            //�ش� ������ ���� ��� ����
            MAC.Change_text("�������� ���������� �˰��� ���������?");
            MAC2.Change_text("�������� ���������� �˰��� ���������?");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 22)
        {
            handModelsManager.ToggleHandModelsGroup(14);
            MAC2.Change_size(48);
            //�ش� ������ ���� ��� ����
            MAC.Change_text("�������� ��������� �˾ƺ��Կ�. \n���� ������ �Ѹ���, ����� ǥ�ð� ����� ���� �� �����?");
            MAC2.Change_text("�������� ��������� �˾ƺ��Կ�. \n���� ������ �Ѹ���, ����� ǥ�ð� ����� ���� �� �����?");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 23)
        {
            handModelsManager.ToggleHandModelsGroup(7);
            MAC2.Change_size(48);
            //�ش� ������ ���� ��� ����
            MAC.Change_text("�������� ��������� Ž���غ��ƿ�! " +
                "\n�������� ���� �������� ���� �����ؿ�. ��ġ ���� ������ǰó�� ������.");
            MAC2.Change_text("�������� ��������� Ž���غ��ƿ�! " +
                "\n�������� ���� �������� ���� �����ؿ�. ��ġ ���� ������ǰó�� ������.");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;

        }//C4
        else if (content_func == 32)
        {
            Aloe_Background.SetActive(true);
            Aloe_mini.SetActive(false);
            AL1.SetActive(false);

            handModelsManager.ToggleHandModelsGroup(8);

            //�ش� ������ ���� ��� ����
            MAC.Change_text("�˷ο��� ū ����ó�� ���� �ٱ⿡�� �β��� ���� �����ư��� �ڶ󳪿�. ������ ����ó�� ���� ��ġ�� �������� ����� �ϰ� �־��.");
            MAC2.Change_text("�˷ο��� ū ����ó�� ���� �ٱ⿡�� �β��� ���� �����ư��� �ڶ󳪿�. ������ ����ó�� ���� ��ġ�� �������� ����� �ϰ� �־��.");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 33)
        {
            Aloe_Background.SetActive(true);
            Aloe_mini.SetActive(false);
            AL1.SetActive(false);

            handModelsManager.ToggleHandModelsGroup(8);

            //�ش� ������ ���� ��� ����
            MAC.Change_text("�˷ο��� ���������� �� �˰��� \n���������� �� �˰��� ���������?");
            MAC2.Change_text("�˷ο��� ���������� �� �˰��� \n���������� �� �˰��� ���������?");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 30)
        {
            Aloe_Background.SetActive(false);
            Aloe_mini.SetActive(true);

            aloe_active.SetActive(true);

            AL1.SetActive(true);
            rightR.SetActive(true);

            BAL.text = 0.ToString();
            SAL.text = 0.ToString();
            CBAL.text = 0.ToString();
            CSAL.text = 0.ToString();

            Cutal1.transform.GetChild(0).gameObject.SetActive(false);
            Cutal1.transform.GetChild(1).gameObject.SetActive(false);
            Cutal2.transform.GetChild(0).gameObject.SetActive(false);
            Cutal2.transform.GetChild(1).gameObject.SetActive(false);
            Cutal3.transform.GetChild(0).gameObject.SetActive(false);
            Cutal3.transform.GetChild(1).gameObject.SetActive(false);

            ccutal1.transform.GetChild(0).gameObject.SetActive(false);
            ccutal1.transform.GetChild(1).gameObject.SetActive(false);
            ccutal2.transform.GetChild(0).gameObject.SetActive(false);
            ccutal2.transform.GetChild(1).gameObject.SetActive(false);
            ccutal3.transform.GetChild(0).gameObject.SetActive(false);
            ccutal3.transform.GetChild(1).gameObject.SetActive(false);

            bbal1.SetActive(true);
            bbal2.SetActive(true);
            bbal3.SetActive(true);
            ssal1.SetActive(true);
            ssal2.SetActive(true);
            ssal3.SetActive(true);

            bal1.SetActive(false);
            bal2.SetActive(false);
            bal3.SetActive(false);
            sal1.SetActive(false);
            sal2.SetActive(false);
            sal3.SetActive(false);

            GK1.SetActive(false);
            GK2.SetActive(false);
            GK3.SetActive(false);
            gk1.SetActive(false);
            gk2.SetActive(false);
            gk3.SetActive(false);

            handModelsManager.ToggleHandModelsGroup(10);

            //�ش� ������ ���� ��� ����
            MAC.Change_text("������! �̹� �ð����� ����ִ� �˷ο� �ٱ� �ڸ��⸦ �غ��ſ���. �غ� �Ǽ̳���?");
            MAC2.Change_text("������! �̹� �ð����� ����ִ� �˷ο� �ٱ� �ڸ��⸦ �غ��ſ���. �غ� �Ǽ̳���?");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 31)
        {
            Aloe_Background.SetActive(true);
            Aloe_mini.SetActive(false);
            AL1.SetActive(false);

            handModelsManager.ToggleHandModelsGroup(8);

            //�ش� ������ ���� ��� ����
            MAC.Change_text("ù��° �˷ο��� �˷ο� ���󿡿�. ���� �� �˷��� �˷ο��� �� ���������� ���� ���� ���ǰ� �־��");
            MAC2.Change_text("ù��° �˷ο��� �˷ο� ���󿡿�. ���� �� �˷��� �˷ο��� �� ���������� ���� ���� ���ǰ� �־��");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        //SceneManager.LoadSceneAsync(1);
    }




    IEnumerator Next_TextCoroutine2(int content_func)
    {
        Debug.Log("8�� ���!");

        switch (content_func)
        {
            case 0:
                yield return new WaitForSeconds(11.1f);
                MAC.Animation_Off();
                MAC2.Animation_Off();
                StartCoroutine(EventSystem_true());
                break;
            case 1:
                yield return new WaitForSeconds(5.1f);
                MAC.Animation_Off();
                MAC2.Animation_Off(); StartCoroutine(EventSystem_true());
                StartCoroutine(EventSystem_true());
                break;
            case 2:
                yield return new WaitForSeconds(6.2f);
                MAC.Animation_Off();
                MAC2.Animation_Off();
                StartCoroutine(EventSystem_true());
                break;
            case 3:
                yield return new WaitForSeconds(6.1f);
                //MAC.Animation_Off();
                MAC2.Animation_Off();
                StartCoroutine(EventSystem_true());
                break;
            /*case 4:
                yield return new WaitForSeconds(9.0f);
                MAC.Animation_Off();
                MAC2.Animation_Off();
                break;*/
            //////////////////�ɺ��� ��Ȱ�� ����////////////////////////
            case 5:
                Debug.Log("case 5");
                yield return new WaitForSeconds(9.1f);
                //MAC.Animation_Off();
                MAC2.Animation_Off();
                //StartCoroutine(EventSystem_true());
                break;
            case 91:
                Debug.Log("case 91");
                yield return new WaitForSeconds(10.0f);
                //MAC.Animation_Off();
                MAC2.Animation_Off();
                //StartCoroutine(EventSystem_true());
                break;
            case 92:
                Debug.Log("case 92");
                yield return new WaitForSeconds(9.0f);
                //MAC.Animation_Off();
                MAC2.Animation_Off();
                //StartCoroutine(EventSystem_true());
                break;
            case 93:
                Debug.Log("case 93");
                yield return new WaitForSeconds(9.0f);
               // MAC.Animation_Off();
                MAC2.Animation_Off();
                //StartCoroutine(EventSystem_true());
                break;
            ///////////////////////////////////////////////////////////////////////////
            case 6:
                yield return new WaitForSeconds(6.2f);
                //MAC.Animation_Off();
                MAC2.Animation_Off();
                StartCoroutine(EventSystem_true());
                break;
            case 7:
                yield return new WaitForSeconds(5.0f);
                //MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            case 8:
                yield return new WaitForSeconds(5.0f);
                //MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            case 9:
                yield return new WaitForSeconds(5.0f);
               // MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            case 10:
                yield return new WaitForSeconds(12.1f);
              //  MAC.Animation_Off();
                MAC2.Animation_Off();
                StartCoroutine(EventSystem_true());
                break;
            case 11:
                yield return new WaitForSeconds(4.1f);
               // MAC.Animation_Off();
                MAC2.Animation_Off();
                StartCoroutine(EventSystem_true());
                break;
            case 12:
                yield return new WaitForSeconds(13.1f);
               // MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            case 13:
                yield return new WaitForSeconds(6.0f);
              //  MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            case 14:
                yield return new WaitForSeconds(5.0f);
              //  MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            case 15:
                yield return new WaitForSeconds(5.0f);
               // MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            case 16:
                yield return new WaitForSeconds(5.0f);
              //  MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            case 17:
                yield return new WaitForSeconds(5.0f);
              //  MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            case 18:
                yield return new WaitForSeconds(5.0f);
             //   MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            case 19:
                yield return new WaitForSeconds(5.0f);
              //  MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            case 20:
                yield return new WaitForSeconds(12.1f);
                MAC.Animation_Off();
                MAC2.Animation_Off();
                StartCoroutine(EventSystem_true());
                break;
            case 21:
                yield return new WaitForSeconds(5.1f);
                //MAC.Animation_Off();
                MAC2.Animation_Off();
                StartCoroutine(EventSystem_true());
                break;
            case 22:
                yield return new WaitForSeconds(8.5f);
                //MAC.Animation_Off();
                MAC2.Animation_Off();
                StartCoroutine(EventSystem_true());
                break;

            //////////////////������ Ư¡ ����////////////////////////
            case 23:
                yield return new WaitForSeconds(10.5f);
                MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            case 51:
                yield return new WaitForSeconds(8.0f);
                MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            //////////////////////////////////////////
            case 31:
                yield return new WaitForSeconds(9.1f);
                MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            /////////////////////�˷ο� Ư¡ ����////////////////////////
            case 32:
                yield return new WaitForSeconds(11.5f);
                MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
            case 33:
                yield return new WaitForSeconds(7.1f);
                MAC.Animation_Off();
                MAC2.Animation_Off();
                StartCoroutine(EventSystem_true());
                break;
            case 61:
                yield return new WaitForSeconds(5f);
                break;
            case 62:
                yield return new WaitForSeconds(5f);
                break;
            //////////////////////////////////////////
            ///
            case 30:
                yield return new WaitForSeconds(7.0f);
                MAC.Animation_Off();
                MAC2.Animation_Off();
                break;
        }
        //yield return new WaitForSeconds(8.3f); // ���ϴ� ��� �ð� ����
        StartCoroutine(Delay_BTW_Text2(content_func));
    }
    IEnumerator Delay_BTW_Text2(int content_func)
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Next_Text2(content_func));
    }

    IEnumerator Next_Text2(int content_func)
    {
        //------------------------�ɺ��� ��Ȱ��--------------------------------------------
        if (content_func == 5)
        {

           // MAC.Change_text("�˿��� �ɺ��̷� ��ȭ�߾��!\n �ɺ��̰� ������� ��Ʋ��Ʋ ���� �־��.");
            MAC2.Change_text("�˿��� �ɺ��̷� ��ȭ�߾��!\n �ɺ��̰� ������� ��Ʋ��Ʋ ���� �־��.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(8f);
          //  MAC.Animation_Off();
            MAC2.Animation_Off();
            yield return new WaitForSeconds(5.0f);

            content_func = 91;
            StartCoroutine(Next_Text2(content_func));
        }
        if (content_func == 91)
        {

            //MAC.Change_text("�ɺ��̰� ������ ���� ã�� ���� ������ ���� �־��. \n�ɺ��̰� ��Ʋ �Ÿ��� ����� ������ ������.");
            MAC2.Change_text("�ɺ��̰� ������ ���� ã�� ���� ������ ���� �־��. \n�ɺ��̰� ��Ʋ �Ÿ��� ����� ������ ������.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(9.1f);
           // MAC.Animation_Off();
            MAC2.Animation_Off();
            yield return new WaitForSeconds(40f);

            content_func = 92;
            StartCoroutine(Next_Text2(content_func));
        }
        if (content_func == 92)
        {

          //  MAC.Change_text("������ ���� ã�� �ɺ��̴� �����Ⱑ �Ǿ��׿�.");
            MAC2.Change_text("������ ���� ã�� �ɺ��̴� �����Ⱑ �Ǿ��׿�.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(5.1f);
          //  MAC.Animation_Off();
            MAC2.Animation_Off();
            yield return new WaitForSeconds(2.2f);

            content_func = 93;
            StartCoroutine(Next_Text2(content_func));
        }
        if (content_func == 93)
        {

           // MAC.Change_text("�������� ���� �̷��� ������.");
            MAC2.Change_text("�������� ���� �̷��� ������.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(4.1f);
         //   MAC.Animation_Off();
            MAC2.Animation_Off();
            yield return new WaitForSeconds(2.3f);

            content_func = 94;
            StartCoroutine(Next_Text2(content_func));
        }
        if (content_func == 94)
        {

           // MAC.Change_text("���� ������ �Ǿ����.\n �����̵� �ɺ��̰� �������� ���ƴٴϳ׿�.");
            MAC2.Change_text("���� ������ �Ǿ����.\n �����̵� �ɺ��̰� �������� ���ƴٴϳ׿�.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(7.5f);
         //   MAC.Animation_Off();
            MAC2.Animation_Off();
            StartCoroutine(EventSystem_true());
        }
        //--------------------------------------------------------------------
        if (content_func == 12)
        {

          //  MAC.Change_text("�̰����� ����� Ư���� ���� ������ ������ �ϴ� Ư���� ȯ���� �����ϰ� �ִ�ϴ�.");
            MAC2.Change_text("�̰����� ����� Ư���� ���� ������ ������ �ϴ� Ư���� ȯ���� �����ϰ� �ִ�ϴ�.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(8.1f);
         //   MAC.Animation_Off();
            MAC2.Animation_Off();
            StartCoroutine(EventSystem_true());
        }
        else if (content_func == 20)
        {

        }
        //-----------------------------------------------
        if (content_func == 23)
        {
            MAC.Change_text("������ ������ ������ �� �� �Ŀ�, ���� ������ ���Ƴ��� �����ؿ�! �� ���� �۰� ����̿���.");
            MAC2.Change_text("������ ������ ������ �� �� �Ŀ�, ���� ������ ���Ƴ��� �����ؿ�! �� ���� �۰� ����̿���.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(9.1f);
            MAC.Animation_Off();
            MAC2.Animation_Off();

            content_func = 50;
            StartCoroutine(Delay_BTW_Text2(content_func));
        }
        else if (content_func == 50)
        {
            MAC2.Change_size(40);
            MAC.Change_text("�� ���� �ڶ�� �� ũ�� ��������. ������ �Ĺ��� ū ��� �ٻ�͸� ������ �ְ� �߰��� �� �ٱⰡ �־��. �� �ٱ� ������ ���� ���� �Ǿ��.");
            MAC2.Change_text("�� ���� �ڶ�� �� ũ�� ��������. ������ �Ĺ��� ū ��� �ٻ�͸� ������ �ְ� �߰��� �� �ٱⰡ �־��. �� �ٱ� ������ ���� ���� �Ǿ��.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(13.6f);
            MAC.Animation_Off();
            MAC2.Animation_Off();

            content_func = 51;
            StartCoroutine(Delay_BTW_Text2(content_func));
        }
        else if (content_func == 51)
        {
            MAC.Change_text("���� �������� Ȱ¦ ���Ⱦ��. �� �ڶ� �������� ��� �˰��̵��� �޷��־��.");
            MAC2.Change_text("���� �������� Ȱ¦ ���Ⱦ��. �� �ڶ� �������� ��� �˰��̵��� �޷��־��.");
            MAC2.Change_size(48);
            //MAC.Animation_On();
            MAC2.Animation_On();

            yield return new WaitForSeconds(8.8f);
            MAC.Animation_Off();
            MAC2.Animation_Off();
            StartCoroutine(EventSystem_true());
        }
        //-----------------------------------------------
        else if (content_func == 32)
        {
            MAC.Change_text("�˷ο��� ���ڷ� ���� �ڶ󳪼� ����ó�� ������� �ƴ�����, ���������� �������� Ư���� �Ƹ��ٿ��� �����ؿ�.");
            MAC2.Change_text("�˷ο��� ���ڷ� ���� �ڶ󳪼� ����ó�� ������� �ƴ�����, ���������� �������� Ư���� �Ƹ��ٿ��� �����ؿ�.");
            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(10.1f);
            MAC.Animation_Off();
            MAC2.Animation_Off();
            StartCoroutine(EventSystem_true());
        }
        //--------------�˷ο� Ư¡--------------------------
        else if (content_func == 31)
        {
            MAC.Change_text("�˷ο� ����� �ַ� �Ǻ� �����ϴµ� ���ǰ� �־��");
            MAC2.Change_text("�˷ο� ����� �ַ� �Ǻ� �����ϴµ� ���ǰ� �־��");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(5.1f);
            MAC.Animation_Off();
            MAC2.Animation_Off();

            content_func = 60;
            StartCoroutine(Delay_BTW_Text2(content_func));
        }
        else if (content_func == 60)
        {
            MAC.Change_text("�ι�° �˷ο��� �˷ο� ��ũ�δ�ũƿ�λ翡��. ô���� ȯ�濡���� �ڶ�� �Ĺ��� �縷 �������� �� �� �־��.");
            MAC2.Change_text("�ι�° �˷ο��� �˷ο� ��ũ�δ�ũƿ�λ翡��. ô���� ȯ�濡���� �ڶ�� �Ĺ��� �縷 �������� �� �� �־��.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(10.1f);
            MAC.Animation_Off();
            MAC2.Animation_Off();

            content_func = 61;
            StartCoroutine(Delay_BTW_Text2(content_func));
        }
        else if (content_func == 61)
        {
            MAC.Change_text("�˷ο� ��ũ�δ�ũƿ�λ�� ���帧, ����, ������ �Ǻο� ���� �Ǻθ� ��ȭ�ϴµ� ������ ���.");
            MAC2.Change_text("�˷ο� ��ũ�δ�ũƿ�λ�� ���帧, ����, ������ �Ǻο� ���� �Ǻθ� ��ȭ�ϴµ� ������ ���.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(9.1f);
            MAC.Animation_Off();
            MAC2.Animation_Off();

            content_func = 62;
            StartCoroutine(Delay_BTW_Text2(content_func));
        }
        else if (content_func == 62)
        {
            MAC.Change_text("����° �˷ο��� �˷ο� �ƶ󺸷��ý�����. ���� ū �������� �˷ο���.");
            MAC2.Change_text("����° �˷ο��� �˷ο� �ƶ󺸷��ý�����. ���� ū �������� �˷ο���.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(6.7f);
            MAC.Animation_Off();
            MAC2.Animation_Off();

            content_func = 63;
            StartCoroutine(Delay_BTW_Text2(content_func));
        }
        else if (content_func == 63)
        {
            MAC.Change_text("�˷ο� �ƶ󺸷��ý��� ��ó ġ���� ������ �ְ�, ��ȭ�� �����ϴµ� ������ ���.");
            MAC2.Change_text("�˷ο� �ƶ󺸷��ý��� ��ó ġ���� ������ �ְ�, ��ȭ�� �����ϴµ� ������ ���.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(8.1f);
            MAC.Animation_Off();
            MAC2.Animation_Off();
            StartCoroutine(EventSystem_true());
        }
        //------------------�˷ο� Ư¡�̾����ϴ�~----------

        else if (content_func == 13)
        {
            MAC.Change_text("�޼տ��� ���� �����, �����տ��� ū ����� ��� ���� �ſ���. ���� �����Դ� ���� �����, ū �����Դ� ū ����� �ָ� �ſ�.");
            MAC2.Change_text("�޼տ��� ���� �����, �����տ��� ū ����� ��� ���� �ſ���. ���� �����Դ� ���� �����, ū �����Դ� ū ����� �ָ� �ſ�.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(12.5f);
            MAC.Animation_Off();
            MAC2.Animation_Off();

            content_func = 77;
            StartCoroutine(Delay_BTW_Text2(content_func));
        }
        else if (content_func == 77)
        {
            MAC.Change_text("�׷� �����غ��Կ�! �� ���� ��� 3��, ū ��� 3���� ������ ��� �ָ� �Ǵ� �ſ���.");
            MAC2.Change_text("�׷� �����غ��Կ�! �� ���� ��� 3��, ū ��� 3���� ������ ��� �ָ� �Ǵ� �ſ���.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(8.9f);
            MAC.Animation_Off();
            MAC2.Animation_Off();
            actt.SetActive(true);
            StartCoroutine(EventSystem_true());
        }

        else if (content_func == 30)
        {
            MAC.Change_text("�޼տ��� ���� Į, �����տ��� ū Į�� ��� ���� �ſ���. ���� Į�δ� ���� �˷ο��� ū Į�δ� ū �˷ο��� �߶󺼰Կ�.");
            MAC2.Change_text("�޼տ��� ���� Į, �����տ��� ū Į�� ��� ���� �ſ���. ���� Į�δ� ���� �˷ο��� ū Į�δ� ū �˷ο��� �߶󺼰Կ�.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(11.5f);
            MAC.Animation_Off();
            MAC2.Animation_Off();

            content_func = 88;
            StartCoroutine(Delay_BTW_Text2(content_func));
        }
        else if (content_func == 88)
        {
            MAC.Change_text("�׷� �����غ��Կ�! ���� �˷ο� 3��, ū �˷ο� 3���� �߶󺼰ſ���. �غ�� Į�� Ȱ���ؼ� �˷ο��� �ڸ��� �ſ���!");
            MAC2.Change_text("�׷� �����غ��Կ�! ���� �˷ο� 3��, ū �˷ο� 3���� �߶󺼰ſ���. �غ�� Į�� Ȱ���ؼ� �˷ο��� �ڸ��� �ſ���!");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(10.9f);
            MAC.Animation_Off();
            MAC2.Animation_Off();
            accct.SetActive(true);
            StartCoroutine(EventSystem_true());
        }
    }


    //���� ����, �л� ���� ������ Ȯ��
    public void Button_Message_Contents()
    {
        if (Is_Toolsaved)
        {
            Button_Contents();
        }
        else
        {
            Message_Tool.SetActive(true);
        }
    }
    public void Button_Message_Contents_Select(int Num_content)
    {
        Run_Mode(Num_content);
        dec_final.SetActive(false);
        final_active.SetActive(false);
    }
    public void Button_Login()
    {
        Login.SetActive(true);
    }
    public void Button_Survey()
    {
        bool Is_Logindatasaved = Manager_login.instance.Get_Islogindatasaved();

        if (Is_Logindatasaved)
        {
            Survey.SetActive(true);
            Manager_Survey.instance.Init_Survey();
        }
        else
        {
            Message_Survey_StudentCheck.SetActive(true);
        }
    }

    public void Button_Message_Login_SelectedStudentCheck()
    {
        bool Is_Studentdatasaved = Manager_login.instance.Get_Is_StudentDataSelected();

        if (Is_Studentdatasaved)
        {
            Message_L_SelectedStudentCheck.SetActive(true);
            Message_L_SelectedStudentCheck.GetComponent<Message_SelectedStudentInfo>().Change_Info("�л����� �α����ұ��?");
        }
        else
        {
            Message_L_Nonselect.SetActive(true);
        }
    }
    public void Button_Message_Login_StudentNotSelect()
    {
        Message_L_Nonselect.SetActive(true);
    }
    public void Button_Message_Login_StudentDataSaved()
    {
        Message_L_StudentDataSaved.SetActive(true);
    }
    public void Button_Message_Login_FieldEmpty()
    {
        Message_L_FieldEmpty.SetActive(true);
    }


    public void Save_Data()
    {
        //�׷� ���� �����ʹ� ������ �Է��ؾ��ϳ�?
        //���� ��� ������ â������ ���Ƿ� ��հ��� �Է��ؼ� ��ȭ�ϴ� ���̸� Ȯ���� �� �ְԲ� ����
        //������ �츮�� �������� ������̳� � �����͸� ���� �ʱ� ������ �Ǵ� �Ұ�
        //�׷� ������ �̰Ŵ� ��� ���� ����� �� �� ��⸦ �غ����� ��


        //���⼭ ���� result�� ���� �ϴ� �κ�
        DialogueData Saved_data = new DialogueData();

        Saved_data.ID = Manager_login.instance.ID;
        Saved_data.Name = Manager_login.instance.Name;
        Saved_data.Birth_date = Manager_login.instance.Birthdate;
        Saved_data.Date = Manager_login.instance.Date;
        Saved_data.Session = Manager_login.instance.Session.ToString();
        Saved_data.Data_1 = Manager_login.instance.Data_1;
        Saved_data.Data_2 = Manager_login.instance.Data_2;
        //Manager_Result.instance.Add_data(Saved_data);
        //Manager_Result.instance.Write();
        Manager_ffinall.instance.Add_data(Saved_data);
        Manager_ffinall.instance.Write();

    }
    void Dummy_setting_content()
    {
        //������ ����
    }
    void Dummy_setting_content_Func()
    {
        //������ ���� ��� ����
    }

    void Init_page()
    {
        Loading = ICT_RnD_UI.transform.GetChild(0).gameObject;
        Home = ICT_RnD_UI.transform.GetChild(1).gameObject;
        Tool = ICT_RnD_UI.transform.GetChild(2).gameObject;
        Result = ICT_RnD_UI.transform.GetChild(3).gameObject;
        Contents = ICT_RnD_UI.transform.GetChild(4).gameObject;
        Mode = ICT_RnD_UI.transform.GetChild(5).gameObject;
        Monitoring_Music1 = ICT_RnD_UI.transform.GetChild(6).gameObject;
        Monitoring_Music2 = ICT_RnD_UI.transform.GetChild(7).gameObject;
        Monitoring_Music3 = ICT_RnD_UI.transform.GetChild(8).gameObject;
        Monitoring_Music4 = ICT_RnD_UI.transform.GetChild(9).gameObject;
        Monitoring_C1 = ICT_RnD_UI.transform.GetChild(10).gameObject;
        Monitoring_C2 = ICT_RnD_UI.transform.GetChild(11).gameObject;
        Monitoring_C3 = ICT_RnD_UI.transform.GetChild(12).gameObject;
        Monitoring_C4 = ICT_RnD_UI.transform.GetChild(13).gameObject;

        Setting = ICT_RnD_UI.transform.GetChild(14).gameObject;
        Login = ICT_RnD_UI.transform.GetChild(15).gameObject;
        Survey = ICT_RnD_UI.transform.GetChild(16).gameObject;

        Message_Tool = Message_UI.transform.GetChild(0).gameObject;
        Message_Content_StudentCheck = Message_UI.transform.GetChild(1).gameObject;
        Message_Intro = Message_UI.transform.GetChild(2).gameObject;
        Message_L_FieldEmpty = Message_UI.transform.GetChild(3).gameObject;
        Message_L_StudentDataSaved = Message_UI.transform.GetChild(4).gameObject;
        Message_L_SelectedStudentCheck = Message_UI.transform.GetChild(5).gameObject;
        Message_L_Nonselect = Message_UI.transform.GetChild(6).gameObject;
        Message_Survey_StudentCheck = Message_UI.transform.GetChild(7).gameObject;
        Message_EndMusicContent = Message_UI.transform.GetChild(8).gameObject;

        Message_Intro_AR = ARUI.transform.GetChild(0).gameObject;

        //Message_Intro setting, Inspector���� scale 0,0,0���� ����
        Message_Intro.SetActive(true);
        Message_Intro_AR.SetActive(true);
        MAC = Message_Intro.GetComponent<Message_anim_controller>();
        MAC2 = Message_Intro_AR.GetComponent<Message_anim_controller>();

    }

    //1122 ������ �������� �� �ٽ� ������ ������ �� ����� ������ �ʱ�ȭ �ϴ� ��� �ʿ�
    public void UI_Back()
    {
        Prev_page.SetActive(true);
        Next_page.SetActive(false);
    }

    IEnumerator Play_1()
    {
        for (int i = 0; i < audios1.Length; i++)
        {
            audioSource.clip = audios1[i];
            audioSource.Play();
            // ���� Ŭ���� ��� ���� ������ ��ٸ��ϴ�.
            yield return new WaitWhile(() => audioSource.isPlaying);
            //yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Play_2()
    {
        for (int i = 0; i < audios2.Length; i++)
        {
            audioSource.clip = audios2[i];
            audioSource.Play();
            // ���� Ŭ���� ��� ���� ������ ��ٸ��ϴ�.
            yield return new WaitWhile(() => audioSource.isPlaying);
            //yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Play_3()
    {
        for (int i = 0; i < audios3.Length; i++)
        {
            audioSource.clip = audios3[i];
            audioSource.Play();
            // ���� Ŭ���� ��� ���� ������ ��ٸ��ϴ�.
            yield return new WaitWhile(() => audioSource.isPlaying);
            //yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Play_4()
    {
        for (int i = 0; i < audios4.Length; i++)
        {
            audioSource.clip = audios4[i];
            audioSource.Play();
            // ���� Ŭ���� ��� ���� ������ ��ٸ��ϴ�.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }

    //0324 �� ����
    //Inst_SavedNum �ش��ϴ� �ڸ� ��ȣ ����
    //����) {0,1,0} -> ����, ��, ����
    /*public void Change_Inst()
    {
        //1,2,3 �� �� ��Ȱ��ȭ
        for (int i = 0; i < 4; i++)
        {
            if (Inst_SavedNum[i] == 0)
            {
                drum1.SetActive(true);
                drum2.SetActive(false);
                drum3.SetActive(false);
                drum4.SetActive(false);
            }
            else if (Inst_SavedNum[i] == 1)
            {
                drum1.SetActive(false);
                drum2.SetActive(true);
                drum3.SetActive(false);
                drum4.SetActive(false);
            }
            else if (Inst_SavedNum[i] == 2)
            {
                drum1.SetActive(false);
                drum2.SetActive(false);
                drum3.SetActive(true);
                drum4.SetActive(false);

            }
            else if (Inst_SavedNum[i] == 3)
            {
                drum1.SetActive(false);
                drum2.SetActive(false);
                drum3.SetActive(false);
                drum4.SetActive(true);

            }
        }
    }*/
}