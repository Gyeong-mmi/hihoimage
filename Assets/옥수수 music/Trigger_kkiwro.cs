using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;
public class Trigger_kkiwro : MonoBehaviour
{
    //BT 통신용
    private BT_Comm bluetoothHelper;
    public AudioSource cornAudio;
    private float activetime;
    private float collidertime;
    private float diff;
    private float accel;
    public bool isTrigger = false;
    public bool isScratch = false;
    float[] activeTimes = new float[] {
         9f, 9.5f, 10f, 10.5f,
         11f, 11.5f, 12.1f, 12.6f,
         13.2f, 13.8f, 14.4f, 14.8f,
         16.5f, 16.7f, 16.9f, 17.6f,
         18.1f, 18.7f, 19.2f, 25.2f,
         25.5f, 25.7f, 28.4f, 29f,
         29.5f, 30f, 33.5f, 38f,
         42.5f, 42.8f, 43.1f, 43.8f,
         44.3f, 44.9f, 45.4f, 46f,
         46.5f, 48f, 48.6f, 50.9f,
         52.5f, 53f, 55.2f
    };
    float[] inactiveTimes = new float[] {
         9f, 9.5f, 10f, 10.5f,
         11f, 11.5f, 12.1f, 12.6f,
         13.2f, 13.8f, 14.4f, 14.8f,
         16.5f, 16.7f, 16.9f, 17.6f,
         18.1f, 18.7f, 19.2f, 25.2f,
         25.5f, 25.7f, 28.4f, 29f,
         29.5f, 30f, 34.5f, 39f,
         42.5f, 42.8f, 43.1f, 43.8f,
         44.3f, 44.9f, 45.4f, 46f,
         46.5f, 48f, 48.6f, 50.9f,
         52.5f, 53f, 55.2f
    };
    int idx = 0;
    private bool isLogging = false;
    private float state = 0; //긁기 = 0, 치기 = 1
    public void init()
    {
        idx = 0;
        isTrigger = false;
        isLogging = false;
    }
    void Start()
    {
        bluetoothHelper = BT_Comm.Instance;
    }
    void Update()
    {
        float curTime = cornAudio.time;
        Trigger_Kiwro obj = GameObject.FindGameObjectWithTag("Kiwro").GetComponent<Trigger_Kiwro>();
        GameObject collider_one = GameObject.FindGameObjectWithTag("col1");
        GameObject collider_two = GameObject.FindGameObjectWithTag("col2");
        CheckStartEnd_Kiwro chk1 = collider_one.GetComponent<CheckStartEnd_Kiwro>();
        CheckStartEnd_Kiwro chk2 = collider_two.GetComponent<CheckStartEnd_Kiwro>();
        if (curTime >= activeTimes[idx] - 1 && !isLogging)
        {
            isLogging = true;
            isScratch = false;
            chk1.isScratch = false;
            chk2.isScratch = false;
            chk1.isEntered = false;
            chk2.isEntered = false;
        }
        else if (isLogging && curTime >= inactiveTimes[idx] + 1)
        {
            isLogging = false;
            isScratch = false;
            chk1.isScratch = false;
            chk2.isScratch = false;
            chk1.isEntered = false;
            chk2.isEntered = false;
            idx++;
        }
        if (isLogging)
        {
            float col1 = chk1.triggertime;
            float col2 = chk2.triggertime;
            if (chk1.isScratch || chk2.isScratch) isScratch = true;
            if (isScratch && !isTrigger)
            {
                accel = Mathf.Abs(col1 - col2);
                if (col1 > col2)
                {
                    collidertime = col1;
                }
                else
                {
                    collidertime = col2;
                }

                state = 0;
                Manager_ResultInDetail.instance.Add_RIDdata(idx + 1, collidertime, accel, state);  //시간, 강도, 긁기.치기 확인
                isScratch = false;
                chk1.isScratch = false;
                chk2.isScratch = false;
                chk1.isEntered = false;
                chk2.isEntered = false;
                //big collider 의 bool 변수
            }
            else if (!isScratch && isTrigger)
            {
                accel = bluetoothHelper.acc_Stick1.magnitude;
                state = 1;
                Manager_ResultInDetail.instance.Add_RIDdata(idx + 1, collidertime, accel, state);  //시간, 강도, 긁기.치기 확인\
                //isTrigger = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stick"))
        {
            Trigger_Kiwro obj = GameObject.FindGameObjectWithTag("Kiwro").GetComponent<Trigger_Kiwro>();
            obj.Tactile_hit();
            collidertime = cornAudio.time;
            isTrigger = true;
        }
    }
}