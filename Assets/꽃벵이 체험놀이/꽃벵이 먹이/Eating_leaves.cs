using ArduinoBluetoothAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;


public class Eating_leaves : MonoBehaviour
{
    public AudioSource eatsound;
    private float collisionTime = 0f;
    private bool isColliding = false;
    private GameObject currentLeaf = null;
    public GameObject leaf1;
    public GameObject leaf2;
    public GameObject leaf3;

    private BT_Comm bluetoothHelper;


    private void Start()
    {
        eatsound = GetComponent<AudioSource>();
        bluetoothHelper = BT_Comm.Instance;

    }

    private void Update()
    {
        if (isColliding)
        {
            if (currentLeaf != null && currentLeaf.activeSelf)
            {
                if (currentLeaf == leaf1)
                {
                    Invoke("Step2", 4.0f);
                }
                else if (currentLeaf == leaf2)
                {
                    Invoke("Step3", 4.0f);
                }
                else if (currentLeaf == leaf3)
                {
                    Invoke("Step1", 4.0f);
                }
                isColliding = false; // 충돌 상태 초기화
                collisionTime = 0f; // 충돌 시간 초기화
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("worm"))
        {
            isColliding = true;
            collisionTime = 0f; // 충돌 시간 초기화
            currentLeaf = null;
            if (leaf1.activeSelf)
            {
                currentLeaf = leaf1;
            }
            else if (leaf2.activeSelf)
            {
                currentLeaf = leaf2;
            }
            else if (leaf3.activeSelf)
            {
                currentLeaf = leaf3;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("worm"))
        {
            isColliding = false;
            collisionTime = 0f; // 충돌 시간 초기화

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

    private void ResetLeaves()
    {
        leaf1.SetActive(true);
    }

    void Step1()
    {
        if (currentLeaf != null)
        {
            currentLeaf.SetActive(false);

            char temp_mode_stx = '<';
            char temp_mode_etx = '>';
            byte[] af = BitConverter.GetBytes(temp_mode_stx);
            byte[] a = BitConverter.GetBytes('4');
            byte[] al = BitConverter.GetBytes(temp_mode_etx);
            //byte[] b = BitConverter.GetBytes(53);
            byte[] c = BitConverter.GetBytes('0');

            byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x51, c[0], 0xFE };
            bluetoothHelper.SendData(bytestosend);




        }
    }

    void Step2()
    {
        leaf1.SetActive(false);
        leaf2.SetActive(true);

        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('4');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        //byte[] b = BitConverter.GetBytes(53);
        byte[] c = BitConverter.GetBytes('0');

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x51, c[0], 0xFE };
        bluetoothHelper.SendData(bytestosend);

    }

    void Step3()
    {
        leaf2.SetActive(false);
        leaf3.SetActive(true);

        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('4');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        //byte[] b = BitConverter.GetBytes(53);
        byte[] c = BitConverter.GetBytes('0');

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x51, c[0], 0xFE };
        bluetoothHelper.SendData(bytestosend);

    }
}