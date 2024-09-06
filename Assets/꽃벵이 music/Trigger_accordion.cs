using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Trigger_accordion : MonoBehaviour
{
    //BT 통신용
    private BT_Comm bluetoothHelper;

    private float collidertime;

    private float prevDis = 0;  //이전 위치
    private float currDis = 0;  //현재 위치
    private float standard = 2f;
    private float predist = 0;
    private float accel;
    private float accel1;
    private float accel2;

    public AudioSource wormAudio;

    float[] activeTimes = new float[] {
         9.02f, 9.69f, 10.83f, 14f,
         27.5f, 28.7f, 29.2f, 29.8f,
         30.85f, 31.35f, 33f, 35.5f,
         39f, 39.5f, 40.5f, 44f,
         49.5f, 50.02f, 51.13f, 55.46f,
         56.05f, 56.53f
    };
    float[] inactiveTimes = new float[] {
         9.02f, 9.69f, 10.83f, 17f,
         28.2f, 28.7f, 29.2f, 30.3f,
         30.85f, 31.35f, 35f, 37.5f,
         39f, 39.5f, 40.5f, 47f,
         49.5f, 50.02f, 51.13f, 55.46f,
         56.05f, 56.53f
    };
    int idx = 0;
    private bool isLogging = false;

    public void init()
    {
        idx = 0;
        prevDis = 0;
        currDis = 0;
        predist = 0;
        isLogging = false;
    }

    private float state = 0; //줄어들기 = 0, 늘어나기 = 1

    void Start()
    {
        bluetoothHelper = BT_Comm.Instance;
    }

    void Update()
    {
        currDis = CalculateDistance();
        //Debug.Log("ACCORD: " + currDis);    //max: 0~ 0.5

        if (Mathf.Abs(currDis - prevDis) > 0.3f) //거리차이가 0.3 이상이라면,
        {
            //Debug.Log("VIB_AC: " + currDis);    //max: 0~ 0.5
            Tactile_on();    //진동 발생 
            prevDis = currDis;  //위치 업데이트
        }

        float curTime = wormAudio.time;

        if (curTime >= activeTimes[idx] - 1 && !isLogging)
        {
            isLogging = true;
        }
        else if (isLogging && curTime >= inactiveTimes[idx] + 1)
        {
            isLogging = false;
            idx++;
        }

        if (isLogging)
        {
            if (Mathf.Abs(currDis - predist) >= standard) // 거리 차이 2이상
            {
                collidertime = wormAudio.time;
                if (predist > currDis)
                {
                    state = 0;
                    accel1 = bluetoothHelper.acc_Stick1.magnitude;
                    accel2 = bluetoothHelper.acc_Stick2.magnitude;
                    accel = (accel1 + accel2) / 2;
                }
                else
                {
                    state = 1;
                    accel1 = bluetoothHelper.acc_Stick1.magnitude;
                    accel2 = bluetoothHelper.acc_Stick2.magnitude;
                    accel = (accel1 + accel2) / 2;
                }
                Manager_ResultInDetail.instance.Add_RIDdata(idx + 1, collidertime, accel, state);  //시간, 강도, 방향
                predist = currDis;
            }
        }
    }

    public void Tactile_on()
    {
        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('6');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x51, 0x51, 0xFE };

        bluetoothHelper.SendData(bytestosend);
    }
    public void Tactile_off()
    {
        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('6');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x00, 0x00, 0xFE };

        bluetoothHelper.SendData(bytestosend);
    }

    public float CalculateDistance()
    {
        Transform accorTransform1 = GameObject.FindWithTag("RH_M").transform;
        Transform accorTransform2 = GameObject.FindWithTag("LH_M").transform;

        //벡터 간의 거리를 계산
        float distance = Vector3.Distance(accorTransform1.position, accorTransform2.position);

        return (distance * 20); //0~10 (대략)
    }
}