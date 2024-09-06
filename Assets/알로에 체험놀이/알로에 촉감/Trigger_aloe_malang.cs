using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;


public class Trigger_aloe_malang : MonoBehaviour
{
    //BT Ελ½ΕΏλ
    private BT_Comm bluetoothHelper;

    void Start()
    {
        bluetoothHelper = BT_Comm.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stick"))
        {
            Tactile();
            On_magnetic();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Stick"))
        {
            Off_magnetic();
        }
    }

    public void Tactile()
    {
        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('5');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes(53);
        byte[] c = BitConverter.GetBytes('0');
    }

    public void On_magnetic()
    {
        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('A');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        //byte[] b = BitConverter.GetBytes(53);
        byte[] c = BitConverter.GetBytes('0');


        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x53, c[0], 0xFE };
        bluetoothHelper.SendData(bytestosend);
    }

    public void Off_magnetic()
    {
        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('B');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        //byte[] b = BitConverter.GetBytes(54);
        byte[] c = BitConverter.GetBytes('0');


        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x54, c[0], 0xFE };
        bluetoothHelper.SendData(bytestosend);
    }
}


