using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;


public class Trigger_worm3 : MonoBehaviour
{
    private BluetoothHelper bluetoothHelper;
    private string deviceName;
    bool couroutine;

    void Start()
    {
        //Bluetooth 관련 permssion 획득 요청 (필수)
#if UNITY_2020_2_OR_NEWER
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation)
          || !Permission.HasUserAuthorizedPermission(Permission.FineLocation)
          || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_SCAN")
          || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_ADVERTISE")
          || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT"))
            Permission.RequestUserPermissions(new string[] {
                Permission.CoarseLocation,
                Permission.FineLocation,
                "android.permission.BLUETOOTH_SCAN",
                "android.permission.BLUETOOTH_ADVERTISE",
                "android.permission.BLUETOOTH_CONNECT"
              });
#endif
#endif

        //deviceName = "HapticPad";   //연결할 장치의 이름
        deviceName = "PAD_V2";   //연결할 장치의 이름
        try
        {
            bluetoothHelper = BluetoothHelper.GetInstance(deviceName);

            LinkedList<BluetoothDevice> ds = bluetoothHelper.getPairedDevicesList();

            foreach (BluetoothDevice d in ds)
            {
                Debug.Log($"{d.DeviceName} {d.DeviceAddress}");
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

    }


    //public int hit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stick"))
        {
            //Trigger_Kiwro obj = GameObject.FindGameObjectWithTag("Kiwro").GetComponent<Trigger_Kiwro>();
            Tactile();
        }
        /*else
            bluetoothHelper.SendData("<B>");*/
    }
    /*    private void OnTriggerExit(Collider other)
        {
            bluetoothHelper.SendData("<B>");
        }*/



    public void Tactile()
    {
        //진동 발생

        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes(1);
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes(20);
        byte[] c = BitConverter.GetBytes(5);


        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, b[0], c[0], 0xFE };
        bluetoothHelper.SendData(bytestosend);

    }
}



