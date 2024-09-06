using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;
using NRKernal.NRExamples;
using static ChartAndGraph.BarChart;

public class Set_Worm : MonoBehaviour
{
    public GameObject worm;
    public GameObject Egg1;
    public GameObject worm_live;
    public GameObject soil;
    public GameObject Plane_ver;
    public GameObject Plane_ver_worm;
    public GameObject worm_house1;
    public GameObject worm_house2;
    public GameObject adult_worm;
    public GameObject eating_worm;
    public GameObject worm_mini;
    public GameObject BackObject;

    bool flag = false;
    bool Start_flag = false;
    private Vector3 initPos;
    private Vector3 initialLocalScale;
    //BT 통신용
    
    AudioSource audioSource;
    public AudioClip[] audiosCharacter;
    public AudioClip[] audiosApperance;
    public AudioClip[] audiosFeel;
    public AudioClip[] audiosEat;
    public AudioClip[] audiosGame;
    public AudioClip[] audiosLives;

    private BT_Comm bluetoothHelper;

    private void Update()
    {
        
    }

    private void Start()
    {
        bluetoothHelper = BT_Comm.Instance;

        audioSource = GetComponent<AudioSource>();
        initPos = worm.transform.position;
        initialLocalScale = worm.transform.localScale;
        
    }
    public void Worm_Apperance()
    {
        /*char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('Q');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes(0x00);
        byte[] c = BitConverter.GetBytes(0x00);

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x00, 0x00, 0xFE };

        bluetoothHelper.SendData(bytestosend);*/

        worm_mini.SetActive(false);
        eating_worm.SetActive(false);
        adult_worm.SetActive(false);
        worm.transform.localScale = initialLocalScale;
        StartCoroutine("Play_Apperance");
        StartCoroutine(WaitAndActivate(4.0f));
        
    }
    private IEnumerator WaitAndActivate(float delay)
    {
        yield return new WaitForSeconds(delay);
        worm.SetActive(true);
    }
    public void Worm_Character()
    {
        /*char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('Q');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes(0x00);
        byte[] c = BitConverter.GetBytes(0x00);

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x00, 0x00, 0xFE };

        bluetoothHelper.SendData(bytestosend);*/

        worm_mini.SetActive(false);
        eating_worm.SetActive(false);
        adult_worm.SetActive(false);
        StartCoroutine("Play_Character");
        StartCoroutine(WaitAndActivate_Character(6.0f));
        
    }
    public void Worm_Lives()
    {
        /*char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('Q');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes(0x00);
        byte[] c = BitConverter.GetBytes(0x00);

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x00, 0x00, 0xFE };

        bluetoothHelper.SendData(bytestosend);*/

        worm_mini.SetActive(false);
        eating_worm.SetActive(false);
        adult_worm.SetActive(false);
        StartCoroutine("Play_Lives");
        StartCoroutine(WaitAndActivate_Lives(4.0f));
    }
    private IEnumerator WaitAndActivate_Lives(float delay)
    {
        for (int i = 0; i < 7; i++)
        {
            adult_worm.SetActive(false);
            switch (i)
            {
                case 0:
                    yield return new WaitForSeconds(delay);
                    Egg1.SetActive(true);
                    break;
                case 1:
                    yield return new WaitForSeconds(6.6f);
                    Egg1.SetActive(false);
                    worm_live.SetActive(true);
                    soil.SetActive(true);
                    break;
                case 2:
                    yield return new WaitForSeconds(13f);
                    worm_live.SetActive(false);
                    soil.SetActive(false);
                    Plane_ver.SetActive(true);
                    BackObject.SetActive(false);
                    Plane_ver_worm.SetActive(true);
                    break;
                case 3:
                    yield return new WaitForSeconds(50f);
                    Plane_ver_worm.SetActive(false);
                    worm_house1.SetActive(true);
                    break;
                case 4:
                    yield return new WaitForSeconds(6f);
                    worm_house1.SetActive(false);
                    worm_house2.SetActive(true);
                    break;
                case 5:
                    yield return new WaitForSeconds(6f);
                    worm_house1.SetActive(false);
                    worm_house2.SetActive(true);
                    break;
                case 6:
                    yield return new WaitForSeconds(0.2f);
                    BackObject.SetActive(true);
                    worm_house2.SetActive(false);
                    Plane_ver.SetActive(false);
                    adult_worm.SetActive(true);
                    break;
            }
        }
    }
    public void Worm_Eat()
    {
        adult_worm.SetActive(false);
        worm_mini.SetActive(false);
        StartCoroutine(WaitforEat());

        StartCoroutine("Play_Eat");
    }
    IEnumerator WaitforEat()
    {
        /*char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('Q');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes(0x00);
        byte[] c = BitConverter.GetBytes(0x00);

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x00, 0x00, 0xFE };

        bluetoothHelper.SendData(bytestosend);*/

        yield return new WaitForSeconds(5f);
        eating_worm.SetActive(true);
    }
    private IEnumerator WaitAndActivate_Character(float delay)
    {
        yield return new WaitForSeconds(delay);
        worm.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        worm.SetActive(true);
    }
    public void Worm_Feel()
    {
        worm_mini.SetActive(false);
        eating_worm.SetActive(false);
        adult_worm.SetActive(false);
        worm.SetActive(false);
        StartCoroutine("Play_Feel");
    }
    public void Worm_Experiential()
    {
        /*char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('Q');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes(0x00);
        byte[] c = BitConverter.GetBytes(0x00);

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x00, 0x00, 0xFE };

        bluetoothHelper.SendData(bytestosend);*/

        eating_worm.SetActive(false);
        adult_worm.SetActive(false);
        worm.SetActive(false);

        GameSuccess_worm.flag = false;

        StartCoroutine(WaitforGame());

        StartCoroutine("Play_Game");
    }
    IEnumerator WaitforGame()
    {

        yield return new WaitForSeconds(7f);
        worm_mini.SetActive(true);
    }
    IEnumerator Play_Apperance()
    {
        for (int i = 0; i < audiosApperance.Length; i++)
        {
            audioSource.clip = audiosApperance[i];
            audioSource.Play();
            // 현재 클립이 재생 중일 때까지 기다립니다.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
    IEnumerator Play_Character()
    {
        for (int i = 0; i < audiosCharacter.Length; i++)
        {
            audioSource.clip = audiosCharacter[i];
            audioSource.Play();
            // 현재 클립이 재생 중일 때까지 기다립니다.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
    IEnumerator Play_Feel()
    {
        for (int i = 0; i < audiosFeel.Length; i++)
        {
            audioSource.clip = audiosFeel[i];
            audioSource.Play();
            // 현재 클립이 재생 중일 때까지 기다립니다.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
    IEnumerator Play_Lives()
    {
        for (int i = 0; i < audiosLives.Length; i++)
        {
            audioSource.clip = audiosLives[i];
            audioSource.Play();
            // 현재 클립이 재생 중일 때까지 기다립니다.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }

    }
    IEnumerator Play_Eat()
    {
        for (int i = 0; i < audiosEat.Length; i++)
        {
            audioSource.clip = audiosEat[i];
            audioSource.Play();
            // 현재 클립이 재생 중일 때까지 기다립니다.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }

    IEnumerator Play_Game()
    {
        for (int i = 0; i < audiosGame.Length; i++)
        {
            audioSource.clip = audiosGame[i];
            audioSource.Play();
            // 현재 클립이 재생 중일 때까지 기다립니다.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
}