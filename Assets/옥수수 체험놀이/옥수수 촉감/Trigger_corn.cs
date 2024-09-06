using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;


public class Trigger_corn : MonoBehaviour
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
        }
    }

    public void Tactile()
    {
        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('3');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        //byte[] b = BitConverter.GetBytes(51);
        byte[] c = BitConverter.GetBytes('0');

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x51, c[0], 0xFE };
        bluetoothHelper.SendData(bytestosend);
    }
}


