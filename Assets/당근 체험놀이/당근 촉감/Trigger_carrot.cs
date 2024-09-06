using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;


public class Trigger_carrot : MonoBehaviour
{
    //BT 통신용
    private BT_Comm bluetoothHelper;
    void Start()
    {
        bluetoothHelper = BT_Comm.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Carrot")) //당근 충돌
        {
            Tactile();
        }
    }
    public void Tactile()//오른쪽
    {
        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('2');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes(0x52);
        byte[] c = BitConverter.GetBytes(0x00);
        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x52, c[0], 0xFE };
        string hexString = BitConverter.ToString(bytestosend).Replace("-", " ");
        bluetoothHelper.SendData(bytestosend);
    }
}


