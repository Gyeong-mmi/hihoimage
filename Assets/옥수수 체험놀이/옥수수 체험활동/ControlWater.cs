using ArduinoBluetoothAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class ControlWater : MonoBehaviour
{
    private float collisionTime1 = 0f;
    private float collisionTime2 = 0f;
    private float collisionTime3 = 0f;

    private bool ContectWater1 = false;
    private bool ContectWater2 = false;
    private bool ContectWater3 = false;

    public static bool ColpleteWater1 = false;
    public static bool ColpleteWater2 = false;
    public static bool ColpleteWater3 = false;

    public GameObject particle;

    private BT_Comm bluetoothHelper;

    private void Start()
    {
        bluetoothHelper = BT_Comm.Instance;

    }
    private void Update()
    {
        if (ContectWater1)
        {
            Invoke("Complete1", 6.0f);
        }

        if (ContectWater2)
        {
            Invoke("Complete2", 6.0f);
        }

        if (ContectWater3)
        {
            Invoke("Complete3", 6.0f);
        }

        if (particle.activeSelf)
        {
            /*char temp_mode_stx = '<';
            char temp_mode_etx = '>';
            byte[] af = BitConverter.GetBytes(temp_mode_stx);
            byte[] a = BitConverter.GetBytes('4');
            byte[] al = BitConverter.GetBytes(temp_mode_etx);
            //byte[] b = BitConverter.GetBytes(53);
            byte[] c = BitConverter.GetBytes('0');

            byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x51, c[0], 0xFE };
            bluetoothHelper.SendData(bytestosend);*/
            Debug.Log("hey");
        }
        else
        {
            /*char temp_mode_stx = '<';
            char temp_mode_etx = '>';
            byte[] af = BitConverter.GetBytes(temp_mode_stx);
            byte[] a = BitConverter.GetBytes('4');
            byte[] al = BitConverter.GetBytes(temp_mode_etx);
            //byte[] b = BitConverter.GetBytes(53);
            byte[] c = BitConverter.GetBytes('0');

            byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x00, c[0], 0xFE };
            bluetoothHelper.SendData(bytestosend);*/
            Debug.Log("ho");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WaterCol1"))
        {
            ContectWater1 = true;
            collisionTime1 = 0f;
            particle.SetActive(true);
        }
        else if (other.gameObject.CompareTag("WaterCol2"))
        {
            ContectWater2 = true;
            collisionTime2 = 0f;
            particle.SetActive(true);
        }
        else if (other.gameObject.CompareTag("WaterCol3"))
        {
            ContectWater3 = true;
            collisionTime3 = 0f;
            particle.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WaterCol1"))
        {
            ContectWater1 = false;
            collisionTime1 = 0f;
        }
        else if (other.gameObject.CompareTag("WaterCol2"))
        {
            ContectWater2 = false;
            collisionTime2 = 0f;
        }
        else if (other.gameObject.CompareTag("WaterCol3"))
        {
            ContectWater3 = false;
            collisionTime3 = 0f;
        }

        // If none of the water collisions are active, turn off the particle
        if (!ContectWater1 && !ContectWater2 && !ContectWater3)
        {
            particle.SetActive(false);
        }
    }

    void Complete1()
    {
        ContectWater1 = false;
        particle.SetActive(false);

        ColpleteWater1 = true;
    }
    void Complete2()
    {
        ContectWater2 = false;
        particle.SetActive(false);

        ColpleteWater2 = true;
    }
    void Complete3()
    {
        ContectWater3 = false;
        particle.SetActive(false);

        ColpleteWater3 = true;
    }
}