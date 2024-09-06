using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;

public class Trigger_Kiwro : MonoBehaviour
{
    //BT 통신용
    private BT_Comm bluetoothHelper;

    private int count = 0;
    public bool isStarted = false;
    public bool isScratched = false;
    private float start_time;
    private float end_time;

    public AudioSource cornAudio;

    void Start()
    {
        bluetoothHelper = BT_Comm.Instance;
    }

    public void Tactile()
    {
        //진동 발생 긁기
        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('8');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes('0');

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x51, b[0], 0xFE };

        bluetoothHelper.SendData(bytestosend);
    }

    public void Tactile_hit()
    {
        //진동 발생 치기
        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('8');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes('0');

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x52, b[0], 0xFE };

        bluetoothHelper.SendData(bytestosend);
    }

    public float StartTime()
    {
        //시작 시각
        isStarted = true;
        isScratched = false;
        start_time = cornAudio.time;

        return start_time;
    }
    public float EndTime()
    {
        //종료 시각
        isStarted = false;
        isScratched = true;
        end_time = cornAudio.time;

        return end_time;
    }
}
