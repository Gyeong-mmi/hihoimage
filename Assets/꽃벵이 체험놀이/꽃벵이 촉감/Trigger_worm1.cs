using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;


public class Trigger_worm1 : MonoBehaviour
{
    //BT 통신용
    private BT_Comm bluetoothHelper;
    bool couroutine;

    void Start()
    {
        bluetoothHelper = BT_Comm.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collider1"))
        {
            Tactile1();
            Debug.Log("Tactile1");
        }
        if (other.gameObject.CompareTag("Collider2"))
        {
            Tactile2();
        }
        if (other.gameObject.CompareTag("Collider3"))
        {
            Tactile3();
        }
    }



    public void Tactile1()
    {
        //진동 발생

        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('1');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes(1);
        byte[] c = BitConverter.GetBytes('0');


        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, b[0], c[0], 0xFE };
        bluetoothHelper.SendData(bytestosend);

    }
    public void Tactile2()
    {
        //진동 발생

        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('1');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes(5);
        byte[] c = BitConverter.GetBytes('0');


        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, b[0], c[0], 0xFE };
        bluetoothHelper.SendData(bytestosend);

    }
    public void Tactile3()
    {
        //진동 발생

        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('1');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes(15);
        byte[] c = BitConverter.GetBytes('0');


        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, b[0], c[0], 0xFE };
        bluetoothHelper.SendData(bytestosend);

    }
}


