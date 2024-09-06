using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;

public class Trigger_ddrum : MonoBehaviour
{
    //BT 통신용
    private BT_Comm bluetoothHelper;

    private float collidertime;
    private float accel;
    public bool istrigger = false;

    public AudioSource carrotAudio;

    void Start()
    {
        bluetoothHelper = BT_Comm.Instance;
    }
    float[] activeTimes = new float[] {
         12.7f, 17f, 19.8f, 20.4f,
         21f, 21.4f, 24f, 24.4f,
         24.8f, 25f, 25.2f, 25.6f,
         25.8f, 28.6f, 29.2f, 29.8f,
         30.2f, 32.8f, 33.2f, 33.6f,
         33.8f, 34f, 34.4f, 34.6f,
         43.2f, 46.6f, 46.9f, 47.2f,
         51.4f, 51.7f, 52f
    };
    float[] inactiveTimes = new float[] {
         12.7f, 17f, 19.8f, 20.4f,
         21f, 21.4f, 24f, 24.4f,
         24.8f, 25f, 25.2f, 25.6f,
         25.8f, 28.6f, 29.2f, 29.8f,
         30.2f, 32.8f, 33.2f, 33.6f,
         33.8f, 34f, 34.4f, 34.6f,
         43.2f, 46.6f, 46.9f, 47.2f,
         51.4f, 51.7f, 52f
    };
    int idx = 0;
    private bool isLogging = false;
    private bool isActivated = true;

    public void init()
    {
        idx = 0;
        isActivated = true;
        istrigger = false;
        isLogging = false;
    }

    void Activation()
    {
        isActivated = true;    
    }

    /*    enum State {
            right = 0,
            left
        }*/

    private float state = 0; //right = 0, left = 1

    private void Update()
    {
        float curTime = carrotAudio.time;

        if (curTime >= activeTimes[idx] - 1 && !isLogging)
        {
            isLogging = true;
        }
        else if(isLogging && curTime >= inactiveTimes[idx] + 1)
        {
            isLogging = false;
            idx++;
        }

        if (isLogging) {
            if (istrigger && isActivated)  //충돌이 발생했다,
            {
                if (state == 0)
                {
                    accel = bluetoothHelper.acc_Stick1.magnitude;
                }
                else
                {
                    accel = bluetoothHelper.acc_Stick2.magnitude;
                }
                isActivated = false;
                Invoke("Activation", 0.3f);
                Manager_ResultInDetail.instance.Add_RIDdata(idx + 1, collidertime, accel, state);  //시간, 강도, 오 왼 상태
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stick1"))  //오른쪽
        {
            right();
            collidertime = carrotAudio.time;
            istrigger = true;
            state = 0;
        }
        else if (other.gameObject.CompareTag("Stick2"))  //왼쪽
        {
            left();
            collidertime = carrotAudio.time;
            istrigger = true;
            state = 1;
        }
    }

    public void right()
    {
        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('7');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes('0');

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x52, b[0], 0xFE };

        bluetoothHelper.SendData(bytestosend);
    }

    public void left()
    {
        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('7');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes('0');

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, b[0], 0x52, 0xFE };

        bluetoothHelper.SendData(bytestosend);
    }
}
