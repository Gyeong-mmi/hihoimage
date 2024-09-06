using ArduinoBluetoothAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetleaf : MonoBehaviour
{
    public GameObject leaf1;
    public GameObject leaf2;
    public GameObject leaf3;

    private BT_Comm bluetoothHelper;
    // Start is called before the first frame update
    void Start()
    {
        bluetoothHelper = BT_Comm.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // 모든 리프가 비활성화된 경우 10초 후에 leaf1 활성화
        if (!leaf1.activeSelf && !leaf2.activeSelf && !leaf3.activeSelf)
        {

            //eatsound.Play();
            Invoke("ResetLeaves", 8.0f);
        }
    }
    private void ResetLeaves()
    {
        leaf1.SetActive(true);

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
}