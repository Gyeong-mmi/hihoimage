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

    // 음악놀이 bgm + 가상환경
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

    // 체험놀이 가상환경
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

    // 미니게임 가상환경
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

    //BT 통신용
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
        //저작도구 저장여부
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

        //콘텐츠 실행 중일 경우 해당 콘텐츠 비활성화 기능 구현 필요
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
        //음악놀이 데이터 저장
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
        //해당 음악 콘텐츠 재생 기능 구현 필요
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
        //상태 반환
        Is_Toolsaved = false;

        //해당 콘텐츠 설정 관련 기능 더미
        Dummy_setting_content();

        //Message_Intro setting
        Message_Intro.SetActive(true);

        // 이벤트 시스템 가져오기
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
            MAC.Change_text("안녕하세요, 여러분! 이번 시간에는 우리가 평소에 잘 알지 못하던 작은 생명체, 꽃벵이에 대해 알아보려고 해요.");
            //MAC.Animation_On();

            MAC2.Change_text("안녕하세요, 여러분! 이번 시간에는 우리가 평소에 잘 알지 못하던 작은 생명체, 꽃벵이에 대해 알아보려고 해요.");
            MAC2.Animation_On();
            MAC2.Change_size(48);

            StartCoroutine(Next_TextCoroutine(Session));
            StartCoroutine("Play_1"); //Audio 코루틴
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
            MAC.Change_text("주홍빛이 도는 아름다운 당근의 세계로 여러분을 초대합니다. 함께 당근의 매력에 빠져볼까요?");
            MAC2.Change_text("주홍빛이 도는 아름다운 당근의 세계로 여러분을 초대합니다. 함께 당근의 매력에 빠져볼까요?");
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
            MAC.Change_text("안녕하세요! 이번에는 옥수수의 신비로운 세계로 떠납니다! 울퉁불퉁한 옥수수를 관찰하고 느끼며, 옥수수의 특징과 성장과정을 함께 알아볼 거에요.");
            MAC2.Change_text("안녕하세요! 이번에는 옥수수의 신비로운 세계로 떠납니다! 울퉁불퉁한 옥수수를 관찰하고 느끼며, 옥수수의 특징과 성장과정을 함께 알아볼 거에요.");
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
            MAC.Change_text("이번에는 알로에의 신비로운 세계로 여행할 시간입니다.  알로에는 아름다운 나무 모습으로 자란다는데,");
            MAC2.Change_text("이번에는 알로에의 신비로운 세계로 여행할 시간입니다.  알로에는 아름다운 나무 모습으로 자란다는데,");
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
        //yield return new WaitForSeconds(8.3f); // 원하는 대기 시간 설정
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
            MAC.Change_text("작은 꽃벵이가 어떻게 자라고 변화하며 살아가는지 함께 알아보도록 할게요. 준비되셨나요?");
            MAC2.Change_text("작은 꽃벵이가 어떻게 자라고 변화하며 살아가는지 함께 알아보도록 할게요. 준비되셨나요?");

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
            MAC.Change_text("알로에 나무를 관찰하며 그 특유의 끈적끈적한 질감과 다양한 특징을 살펴보도록 하겠습니다. " +
               "\n함께 알로에의 세계에 빠져볼까요?");
            MAC2.Change_text("알로에 나무를 관찰하며 그 특유의 끈적끈적한 질감과 다양한 특징을 살펴보도록 하겠습니다. " +
               "\n함께 알로에의 세계에 빠져볼까요?");
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
        //다른 콘텐츠 내부기능 실행 중인거 비활성화
        //각 콘텐츠 별로 어떻게 실행시킬지

        Message_Intro.SetActive(true);
        //C1
        if (content_func == 0)
        {
            handModelsManager.ToggleHandModelsGroup(7);
            Worm_Background.SetActive(true);

            //해당 콘텐츠 내부 기능 실행
            MAC.Change_text("먼저, 우리는 꽃벵이의 생김새를 살펴볼 거에요. 흙 바닥 위에서 꿈틀꿈틀 거리는 꽃벵이의 모습은 정말 신기해요!");
            MAC2.Change_text("먼저, 우리는 꽃벵이의 생김새를 살펴볼 거에요. 흙 바닥 위에서 꿈틀꿈틀 거리는 꽃벵이의 모습은 정말 신기해요!");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 1)
        {
            handModelsManager.ToggleHandModelsGroup(5);
            Worm_Background.SetActive(true);

            //해당 콘텐츠 내부 기능 실행
            MAC.Change_text("꽃벵이의 꿈틀꿈틀한 촉감을 느껴볼까요?\r\n");
            MAC2.Change_text("꽃벵이의 꿈틀꿈틀한 촉감을 느껴볼까요?\r\n");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 2)
        {
            //해당 콘텐츠 내부 기능 실행
            MAC.Change_text("(테스트)꽃벵이 먹이에 대해 알아볼까요?");
            MAC2.Change_text("꽃뱅이에게 나뭇잎을 줘보세요.\n꽃벵이는 나뭇잎을 주식으로 삼아요.");
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

            //해당 콘텐츠 내부 기능 실행
            MAC.Change_text("이제 꽃벵이를 더 자세히 관찰해보기 위해 크기를 키워볼게요~");
            MAC2.Change_text("이제 꽃벵이를 더 자세히 관찰해보기 위해 크기를 키워볼게요~");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 4)
        {
            /*//해당 콘텐츠 내부 기능 실행
            handModelsManager.ToggleHandModelsGroup(7);
            Worm_Background.SetActive(true);

            //해당 콘텐츠 내부 기능 실행
            MAC.Change_text("꽃벵이에게 나뭇잎을 줘보세요. 꽃벵이는 나뭇잎을 주식으로 삼아요.");
            MAC2.Change_text("꽃벵이에게 나뭇잎을 줘보세요. 꽃벵이는 나뭇잎을 주식으로 삼아요.");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;*/
        }
        else if (content_func == 5)
        {
            handModelsManager.ToggleHandModelsGroup(7);
            Worm_Background.SetActive(true);

            //해당 콘텐츠 내부 기능 실행
            MAC.Change_text("꽃뱅이의 생활사를 알아보도록 하겠습니다." +
                "꽃벵이는 알에서부터 시작해요. \n알이 꿈틀거리네요~");
            MAC2.Change_text("꽃뱅이의 생활사를 알아보도록 하겠습니다." +
                "꽃벵이는 알에서부터 시작해요. \n알이 꿈틀거리네요~");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 6)
        {
            //해당 콘텐츠 내부 기능 실행
            //MAC.Change_text("(테스트)꽃벵이 체험활동을 해볼까요?");
            MAC2.Change_text("꿈틀거리는 꽃벵이들이 잠을 잘 시간이에요.\n 나뭇잎 침대로 옮겨 볼까요?");
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
            //해당 콘텐츠 내부 기능 실행
            handModelsManager.ToggleHandModelsGroup(8);

            MAC.Change_text("먼저, 주홍빛으로 물들인 당근의 생김새를 살펴봅시다! 원뿔 모양의 크고 다양한 형태, 정말로 독특한 매력을 가지고 있어요!");
            MAC2.Change_text("먼저, 주홍빛으로 물들인 당근의 생김새를 살펴봅시다! 원뿔 모양의 크고 다양한 형태, 정말로 독특한 매력을 가지고 있어요!");

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
            //해당 콘텐츠 내부 기능 실행
            handModelsManager.ToggleHandModelsGroup(6);

            MAC.Change_text("당근의 딱딱한 촉감을 느껴볼까요?");
            MAC2.Change_text("당근의 딱딱한 촉감을 느껴볼까요?");
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
            //해당 콘텐츠 내부 기능 실행
            handModelsManager.ToggleHandModelsGroup(8);

            MAC.Change_text("여러분! 당근이 어디서 자라나는지 알고 있나요? 강원 평창, 경남 밀양, 김해, 제주시 구좌읍 등 우리나라의 다양한 지역에서 생산되고 있어요.");
            //MAC.Animation_On();

            MAC2.Change_text("여러분! 당근이 어디서 자라나는지 알고 있나요? 강원 평창, 경남 밀양, 김해, 제주시 구좌읍 등 우리나라의 다양한 지역에서 생산되고 있어요.");
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 13)
        {
            MAC2.Change_size(48);

            //해당 콘텐츠 내부 기능 실행
            MAC.Change_text("여러분! 이번 시간에는 말에게 당근을 줄거에요. 준비가 되셨나요?");
            MAC2.Change_text("여러분! 이번 시간에는 말에게 당근을 줄거에요. 준비가 되셨나요?");
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
            //해당 콘텐츠 내부 기능 실행
            MAC.Change_text("먼저, 옥수수의 생김새를 자세히 관찰해 볼까요. 키가 큰 옥수수와 활짝 열린 옥수수의 모습이 정말 인상적이에요!");
            //MAC.Animation_On();

            MAC2.Change_text("먼저, 옥수수의 생김새를 자세히 관찰해 볼까요. 키가 큰 옥수수와 활짝 열린 옥수수의 모습이 정말 인상적이에요!");
            MAC2.Animation_On();

            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;

        }
        else if (content_func == 21)
        {
            handModelsManager.ToggleHandModelsGroup(7);
            MAC2.Change_size(48);
            //해당 콘텐츠 내부 기능 실행
            MAC.Change_text("옥수수의 울퉁불퉁한 촉감을 느껴볼까요?");
            MAC2.Change_text("옥수수의 울퉁불퉁한 촉감을 느껴볼까요?");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 22)
        {
            handModelsManager.ToggleHandModelsGroup(14);
            MAC2.Change_size(48);
            //해당 콘텐츠 내부 기능 실행
            MAC.Change_text("옥수수의 성장과정을 알아볼게요. \n먼저 씨앗을 뿌리고, 물방울 표시가 생기면 물을 줘 볼까요?");
            MAC2.Change_text("옥수수의 성장과정을 알아볼게요. \n먼저 씨앗을 뿌리고, 물방울 표시가 생기면 물을 줘 볼까요?");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        else if (content_func == 23)
        {
            handModelsManager.ToggleHandModelsGroup(7);
            MAC2.Change_size(48);
            //해당 콘텐츠 내부 기능 실행
            MAC.Change_text("옥수수의 성장과정을 탐구해보아요! " +
                "\n옥수수는 작은 씨앗으로 생을 시작해요. 마치 작은 고무공예품처럼 생겼어요.");
            MAC2.Change_text("옥수수의 성장과정을 탐구해보아요! " +
                "\n옥수수는 작은 씨앗으로 생을 시작해요. 마치 작은 고무공예품처럼 생겼어요.");
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

            //해당 콘텐츠 내부 기능 실행
            MAC.Change_text("알로에는 큰 나무처럼 생긴 줄기에서 두꺼운 잎이 나돌아가듯 자라나요. 나무의 가지처럼 생기 넘치는 나뭇가지 모양을 하고 있어요.");
            MAC2.Change_text("알로에는 큰 나무처럼 생긴 줄기에서 두꺼운 잎이 나돌아가듯 자라나요. 나무의 가지처럼 생기 넘치는 나뭇가지 모양을 하고 있어요.");
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

            //해당 콘텐츠 내부 기능 실행
            MAC.Change_text("알로에의 울퉁불퉁한 겉 촉감과 \n끈적끈적한 속 촉감을 느껴볼까요?");
            MAC2.Change_text("알로에의 울퉁불퉁한 겉 촉감과 \n끈적끈적한 속 촉감을 느껴볼까요?");
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

            //해당 콘텐츠 내부 기능 실행
            MAC.Change_text("여러분! 이번 시간에는 재미있는 알로에 줄기 자르기를 해볼거에요. 준비가 되셨나요?");
            MAC2.Change_text("여러분! 이번 시간에는 재미있는 알로에 줄기 자르기를 해볼거에요. 준비가 되셨나요?");
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

            //해당 콘텐츠 내부 기능 실행
            MAC.Change_text("첫번째 알로에는 알로에 베라에요. 가장 잘 알려진 알로에로 전 세계적으로 가장 많이 재배되고 있어요");
            MAC2.Change_text("첫번째 알로에는 알로에 베라에요. 가장 잘 알려진 알로에로 전 세계적으로 가장 많이 재배되고 있어요");
            //MAC.Animation_On();
            MAC2.Animation_On();
            StartCoroutine(Next_TextCoroutine2(content_func));
            eventSystem.enabled = false;
        }
        //SceneManager.LoadSceneAsync(1);
    }




    IEnumerator Next_TextCoroutine2(int content_func)
    {
        Debug.Log("8초 대기!");

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
            //////////////////꽃벵이 생활사 순서////////////////////////
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

            //////////////////옥수수 특징 순서////////////////////////
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
            /////////////////////알로에 특징 순서////////////////////////
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
        //yield return new WaitForSeconds(8.3f); // 원하는 대기 시간 설정
        StartCoroutine(Delay_BTW_Text2(content_func));
    }
    IEnumerator Delay_BTW_Text2(int content_func)
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Next_Text2(content_func));
    }

    IEnumerator Next_Text2(int content_func)
    {
        //------------------------꽃벵이 생활사--------------------------------------------
        if (content_func == 5)
        {

           // MAC.Change_text("알에서 꽃벵이로 변화했어요!\n 꽃벵이가 흙속으로 꿈틀꿈틀 기어가고 있어요.");
            MAC2.Change_text("알에서 꽃벵이로 변화했어요!\n 꽃벵이가 흙속으로 꿈틀꿈틀 기어가고 있어요.");

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

            //MAC.Change_text("꽃벵이가 안전한 곳을 찾기 위해 땅속을 기어가고 있어요. \n꽃벵이가 꿈틀 거리는 모습을 관찰해 보세요.");
            MAC2.Change_text("꽃벵이가 안전한 곳을 찾기 위해 땅속을 기어가고 있어요. \n꽃벵이가 꿈틀 거리는 모습을 관찰해 보세요.");

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

          //  MAC.Change_text("안전한 곳을 찾은 꽃벵이는 번데기가 되었네요.");
            MAC2.Change_text("안전한 곳을 찾은 꽃벵이는 번데기가 되었네요.");

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

           // MAC.Change_text("번데기의 속은 이렇게 생겼어요.");
            MAC2.Change_text("번데기의 속은 이렇게 생겼어요.");

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

           // MAC.Change_text("드디어 성충이 되었어요.\n 성충이된 꽃벵이가 여기저기 돌아다니네요.");
            MAC2.Change_text("드디어 성충이 되었어요.\n 성충이된 꽃벵이가 여기저기 돌아다니네요.");

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

          //  MAC.Change_text("이곳들은 당근이 특별한 맛과 영양을 가지게 하는 특별한 환경을 제공하고 있답니다.");
            MAC2.Change_text("이곳들은 당근이 특별한 맛과 영양을 가지게 하는 특별한 환경을 제공하고 있답니다.");

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
            MAC.Change_text("옥수수 씨앗을 심으면 몇 주 후에, 작은 싹으로 돋아나기 시작해요! 이 싹은 작고 녹색이에요.");
            MAC2.Change_text("옥수수 씨앗을 심으면 몇 주 후에, 작은 싹으로 돋아나기 시작해요! 이 싹은 작고 녹색이에요.");

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
            MAC.Change_text("그 싹이 자라면 더 크고 높아져요. 옥수수 식물은 큰 녹색 잎사귀를 가지고 있고 중간에 긴 줄기가 있어요. 그 줄기 위에는 작은 꽃이 피어나요.");
            MAC2.Change_text("그 싹이 자라면 더 크고 높아져요. 옥수수 식물은 큰 녹색 잎사귀를 가지고 있고 중간에 긴 줄기가 있어요. 그 줄기 위에는 작은 꽃이 피어나요.");

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
            MAC.Change_text("드디어 옥수수가 활짝 열렸어요. 다 자란 옥수수는 노란 알갱이들이 달려있어요.");
            MAC2.Change_text("드디어 옥수수가 활짝 열렸어요. 다 자란 옥수수는 노란 알갱이들이 달려있어요.");
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
            MAC.Change_text("알로에는 잎자루 없이 자라나서 나무처럼 우아함은 아니지만, 끈적끈적한 점액질이 특별한 아름다움을 선사해요.");
            MAC2.Change_text("알로에는 잎자루 없이 자라나서 나무처럼 우아함은 아니지만, 끈적끈적한 점액질이 특별한 아름다움을 선사해요.");
            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(10.1f);
            MAC.Animation_Off();
            MAC2.Animation_Off();
            StartCoroutine(EventSystem_true());
        }
        //--------------알로에 특징--------------------------
        else if (content_func == 31)
        {
            MAC.Change_text("알로에 베라는 주로 피부 관리하는데 사용되고 있어요");
            MAC2.Change_text("알로에 베라는 주로 피부 관리하는데 사용되고 있어요");

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
            MAC.Change_text("두번째 알로에는 알로에 마크로다크틸로사에요. 척박한 환경에서도 자라는 식물로 사막 지역에서 볼 수 있어요.");
            MAC2.Change_text("두번째 알로에는 알로에 마크로다크틸로사에요. 척박한 환경에서도 자라는 식물로 사막 지역에서 볼 수 있어요.");

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
            MAC.Change_text("알로에 마크로다크틸로사는 여드름, 습진, 아토피 피부염 같은 피부를 완화하는데 도움을 줘요.");
            MAC2.Change_text("알로에 마크로다크틸로사는 여드름, 습진, 아토피 피부염 같은 피부를 완화하는데 도움을 줘요.");

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
            MAC.Change_text("세번째 알로에는 알로에 아라보렌시스에요. 가장 큰 사이즈의 알로에죠.");
            MAC2.Change_text("세번째 알로에는 알로에 아라보렌시스에요. 가장 큰 사이즈의 알로에죠.");

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
            MAC.Change_text("알로에 아라보렌시스는 상처 치유에 도움을 주고, 소화를 개선하는데 도움을 줘요.");
            MAC2.Change_text("알로에 아라보렌시스는 상처 치유에 도움을 주고, 소화를 개선하는데 도움을 줘요.");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(8.1f);
            MAC.Animation_Off();
            MAC2.Animation_Off();
            StartCoroutine(EventSystem_true());
        }
        //------------------알로에 특징이었습니다~----------

        else if (content_func == 13)
        {
            MAC.Change_text("왼손에는 작은 당근을, 오른손에는 큰 당근을 들고 있을 거에요. 작은 말에게는 작은 당근을, 큰 말에게는 큰 당근을 주면 돼요.");
            MAC2.Change_text("왼손에는 작은 당근을, 오른손에는 큰 당근을 들고 있을 거에요. 작은 말에게는 작은 당근을, 큰 말에게는 큰 당근을 주면 돼요.");

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
            MAC.Change_text("그럼 시작해볼게요! 총 작은 당근 3개, 큰 당근 3개를 말에게 모두 주면 되는 거예요.");
            MAC2.Change_text("그럼 시작해볼게요! 총 작은 당근 3개, 큰 당근 3개를 말에게 모두 주면 되는 거예요.");

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
            MAC.Change_text("왼손에는 작은 칼, 오른손에는 큰 칼을 들고 있을 거에요. 작은 칼로는 작은 알로에를 큰 칼로는 큰 알로에를 잘라볼게요.");
            MAC2.Change_text("왼손에는 작은 칼, 오른손에는 큰 칼을 들고 있을 거에요. 작은 칼로는 작은 알로에를 큰 칼로는 큰 알로에를 잘라볼게요.");

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
            MAC.Change_text("그럼 시작해볼게요! 작은 알로에 3개, 큰 알로에 3개를 잘라볼거에요. 준비된 칼을 활용해서 알로에를 자르는 거에요!");
            MAC2.Change_text("그럼 시작해볼게요! 작은 알로에 3개, 큰 알로에 3개를 잘라볼거에요. 준비된 칼을 활용해서 알로에를 자르는 거에요!");

            //MAC.Animation_On();
            MAC2.Animation_On();
            yield return new WaitForSeconds(10.9f);
            MAC.Animation_Off();
            MAC2.Animation_Off();
            accct.SetActive(true);
            StartCoroutine(EventSystem_true());
        }
    }


    //저작 도구, 학생 저장 데이터 확인
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
            Message_L_SelectedStudentCheck.GetComponent<Message_SelectedStudentInfo>().Change_Info("학생으로 로그인할까요?");
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
        //그럼 여기 데이터는 무엇을 입력해야하나?
        //기존 결과 데이터 창에서는 임의로 평균값을 입력해서 변화하는 추이를 확인할 수 있게끔 해줌
        //현행은 우리가 데이터의 정답률이나 어떤 데이터를 주지 않기 때문에 판단 불가
        //그럼 앞으로 이거는 어떻게 할지 숙대랑 한 번 얘기를 해봐야할 듯


        //여기서 부터 result에 저장 하는 부분
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
        //콘텐츠 실행
    }
    void Dummy_setting_content_Func()
    {
        //콘텐츠 내부 기능 실행
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

        //Message_Intro setting, Inspector에서 scale 0,0,0으로 변경
        Message_Intro.SetActive(true);
        Message_Intro_AR.SetActive(true);
        MAC = Message_Intro.GetComponent<Message_anim_controller>();
        MAC2 = Message_Intro_AR.GetComponent<Message_anim_controller>();

    }

    //1122 콘텐츠 정상종료 후 다시 콘텐츠 실행할 때 저장된 데이터 초기화 하는 기능 필요
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
            // 현재 클립이 재생 중일 때까지 기다립니다.
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
            // 현재 클립이 재생 중일 때까지 기다립니다.
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
            // 현재 클립이 재생 중일 때까지 기다립니다.
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
            // 현재 클립이 재생 중일 때까지 기다립니다.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }

    //0324 북 변경
    //Inst_SavedNum 해당하는 자리 번호 저장
    //예시) {0,1,0} -> 없음, 북, 없음
    /*public void Change_Inst()
    {
        //1,2,3 번 북 비활성화
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