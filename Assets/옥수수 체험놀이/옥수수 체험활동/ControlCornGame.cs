using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ArduinoBluetoothAPI;
using UnityEngine.Android;
using System;

public class ControlCornGame : MonoBehaviour
{
    public GameObject fallSeed1;
    public GameObject fallSeed2;
    public GameObject fallSeed3;

    public GameObject water1;
    public GameObject water2;
    public GameObject water3;

    public GameObject Collider1;
    public GameObject Collider2;
    public GameObject Collider3;

    public GameObject WaterCollider1;
    public GameObject WaterCollider2;
    public GameObject WaterCollider3;

    public GameObject Corn1;
    public GameObject Corn2;
    public GameObject Corn3;



    int num = 0;
    float distance;

    private Vector3 Seedposition1;
    private Vector3 Seedposition2;
    private Vector3 Seedposition3;

    private BT_Comm bluetoothHelper;
    private void Start()
    {
        Seedposition1 = fallSeed1.transform.position;
        Seedposition2 = fallSeed2.transform.position;
        Seedposition3 = fallSeed3.transform.position;

        bluetoothHelper = BT_Comm.Instance;
    }

    public void Tactile()
    {
        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('4');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x00, 0x52, 0xFE };

        bluetoothHelper.SendData(bytestosend);
    }

    private void Update()
    {
        if (ControlGround.ContectSeed1 == true && ControlGround.ContectSeed2 == false && ControlGround.ContectSeed3 == false)
        {
            num = 1;

            fallSeed1.SetActive(true);
            //Tactile();
            StartCoroutine(WaitFall_Seed());
        }
        else if (ControlGround.ContectSeed2 == true && ControlGround.ContectSeed1 == false && ControlGround.ContectSeed3 == false)
        {
            num = 2;

            fallSeed2.SetActive(true);
            //Tactile();
            StartCoroutine(WaitFall_Seed());
        }
        else if (ControlGround.ContectSeed3 == true && ControlGround.ContectSeed1 == false && ControlGround.ContectSeed2 == false)
        {
            num = 3;

            fallSeed3.SetActive(true);
            //Tactile();
            StartCoroutine(WaitFall_Seed());
        }

        if (ControlWater.ColpleteWater1 == true)
        {
            WaterCollider1.SetActive(false);
            Corn1.SetActive(true);
            water1.SetActive(false);
        }
        if (ControlWater.ColpleteWater2 == true)
        {
            WaterCollider2.SetActive(false);
            Corn2.SetActive(true);
            water2.SetActive(false);
        }
        if (ControlWater.ColpleteWater3 == true)
        {
            WaterCollider3.SetActive(false);
            Corn3.SetActive(true);
            water3.SetActive(false);
        }


    }

    IEnumerator WaitFall_Seed()
    {
        if (num == 1)
        {
            Collider2.SetActive(false);
            Collider3.SetActive(false);
            yield return new WaitForSeconds(3.0f);
            FallSeed1();
        }
        else if (num == 2)
        {
            Collider1.SetActive(false);
            Collider3.SetActive(false);
            yield return new WaitForSeconds(3.0f);
            FallSeed2();
        }
        else if (num == 3)
        {
            Collider1.SetActive(false);
            Collider2.SetActive(false);
            yield return new WaitForSeconds(3.0f);
            FallSeed3();
        }
    }

    void FallSeed1()
    {
        Vector3 targetPosition = fallSeed1.transform.position - new Vector3(0, 3f, 0); // 목표 위치는 현재 위치에서 아래로 이동
        float moveSpeed = 0.003f; // 이동 속도 조절
        fallSeed1.transform.position = Vector3.MoveTowards(fallSeed1.transform.position, targetPosition, moveSpeed);
        //ControlGround.ContectSeed1 = false;
        StartCoroutine(WaitSecond());


    }

    void FallSeed2()
    {
        Vector3 targetPosition = fallSeed2.transform.position - new Vector3(0, 3f, 0); // 목표 위치는 현재 위치에서 아래로 이동
        float moveSpeed = 0.003f; // 이동 속도 조절
        fallSeed2.transform.position = Vector3.MoveTowards(fallSeed2.transform.position, targetPosition, moveSpeed);
        //ControlGround.ContectSeed2 = false;
        StartCoroutine(WaitSecond());


    }

    void FallSeed3()
    {
        Vector3 targetPosition = fallSeed3.transform.position - new Vector3(0, 3f, 0); // 목표 위치는 현재 위치에서 아래로 이동
        float moveSpeed = 0.003f; // 이동 속도 조절
        fallSeed3.transform.position = Vector3.MoveTowards(fallSeed3.transform.position, targetPosition, moveSpeed);
        //ControlGround.ContectSeed3 = false;
        StartCoroutine(WaitSecond());
    }

    IEnumerator WaitSecond()
    {
        if (fallSeed1.activeSelf == true && fallSeed2.activeSelf == false && fallSeed3.activeSelf == false)
        {
            yield return new WaitForSeconds(3.5f);
            Collider1.SetActive(false);
            Collider2.SetActive(true);
            Collider3.SetActive(true);
            ControlGround.ContectSeed1 = false;

            water1.SetActive(true);
            WaterCollider1.SetActive(true);
        }
        else if (fallSeed2.activeSelf == true && fallSeed1.activeSelf == false && fallSeed3.activeSelf == false)
        {
            yield return new WaitForSeconds(3.5f);
            Collider1.SetActive(true);
            Collider2.SetActive(false);
            Collider3.SetActive(true);
            ControlGround.ContectSeed2 = false;
            water2.SetActive(true);
            WaterCollider2.SetActive(true);
        }
        else if (fallSeed3.activeSelf == true && fallSeed1.activeSelf == false && fallSeed2.activeSelf == false)
        {
            yield return new WaitForSeconds(3.5f);
            Collider1.SetActive(true);
            Collider2.SetActive(true);
            Collider3.SetActive(false);
            ControlGround.ContectSeed3 = false;
            water3.SetActive(true);
            WaterCollider3.SetActive(true);
        }
        else if (fallSeed1.activeSelf == true && fallSeed2.activeSelf == true && fallSeed3.activeSelf == false)
        {
            yield return new WaitForSeconds(3.5f);

            if (ControlGround.ContectSeed2 == true)
            {
                water2.SetActive(true);
                WaterCollider2.SetActive(true);
                ControlGround.ContectSeed2 = false;

                Collider1.SetActive(false);
                Collider2.SetActive(false);
                Collider3.SetActive(true);
            }
            else if (ControlGround.ContectSeed1 == true)
            {
                water1.SetActive(true);
                WaterCollider1.SetActive(true);
                ControlGround.ContectSeed1 = false;

                Collider1.SetActive(false);
                Collider2.SetActive(false);
                Collider3.SetActive(true);
            }
        }
        else if (fallSeed1.activeSelf == true && fallSeed3.activeSelf == true && fallSeed2.activeSelf == false)
        {
            Collider1.SetActive(false);
            Collider2.SetActive(true);
            Collider3.SetActive(false);

            if (water1.activeSelf == true && water2.activeSelf == false && water3.activeSelf == false)
            {
                yield return new WaitForSeconds(0.2f);
                water3.SetActive(true);
                WaterCollider3.SetActive(true);

                ControlGround.ContectSeed1 = false;
                ControlGround.ContectSeed2 = false;
                ControlGround.ContectSeed3 = false;
            }
            else
            {
                yield return new WaitForSeconds(3.5f);
                water1.SetActive(true);
                WaterCollider1.SetActive(true);

                ControlGround.ContectSeed1 = false;
                ControlGround.ContectSeed2 = false;
                ControlGround.ContectSeed3 = false;
            }
        }
        else if (fallSeed2.activeSelf == true && fallSeed3.activeSelf == true && fallSeed1.activeSelf == false)
        {
            if (water2.activeSelf == true && water1.activeSelf == false && water3.activeSelf == false)
            {
                yield return new WaitForSeconds(3.5f);
                water3.SetActive(true);
                WaterCollider3.SetActive(true);

                Collider1.SetActive(true);
                Collider2.SetActive(false);
                Collider3.SetActive(false);
                ControlGround.ContectSeed1 = false;
                ControlGround.ContectSeed2 = false;
                ControlGround.ContectSeed3 = false;
            }
            else
            {
                yield return new WaitForSeconds(3.5f);
                water2.SetActive(true);
                WaterCollider2.SetActive(true);

                Collider1.SetActive(true);
                Collider2.SetActive(false);
                Collider3.SetActive(false);
                ControlGround.ContectSeed1 = false;
                ControlGround.ContectSeed2 = false;
                ControlGround.ContectSeed3 = false;
            }
        }
        else if (fallSeed2.activeSelf == true && fallSeed3.activeSelf == true && fallSeed1.activeSelf == true)
        {
            if (water1.activeSelf == true && water2.activeSelf == true && water3.activeSelf == false)
            {
                yield return new WaitForSeconds(3.5f);
                water3.SetActive(true);
                WaterCollider3.SetActive(true);

                Collider1.SetActive(false);
                Collider2.SetActive(false);
                Collider3.SetActive(false);
                ControlGround.ContectSeed1 = false;
                ControlGround.ContectSeed2 = false;
                ControlGround.ContectSeed3 = false;
            }
            else if (water1.activeSelf == true && water3.activeSelf == true && water2.activeSelf == false)
            {
                yield return new WaitForSeconds(3.5f);
                water2.SetActive(true);
                WaterCollider2.SetActive(true);

                Collider1.SetActive(false);
                Collider2.SetActive(false);
                Collider3.SetActive(false);
                ControlGround.ContectSeed1 = false;
                ControlGround.ContectSeed2 = false;
                ControlGround.ContectSeed3 = false;
            }
            else if (water2.activeSelf == true && water3.activeSelf == true && water1.activeSelf == false)
            {
                yield return new WaitForSeconds(3.5f);
                water1.SetActive(true);
                WaterCollider1.SetActive(true);

                Collider1.SetActive(false);
                Collider2.SetActive(false);
                Collider3.SetActive(false);
                ControlGround.ContectSeed1 = false;
                ControlGround.ContectSeed2 = false;
                ControlGround.ContectSeed3 = false;
            }
            else if (Corn1.activeSelf == true && Corn2.activeSelf == true && Corn3.activeSelf == false)
            {
                yield return new WaitForSeconds(3.5f);
                water3.SetActive(true);
                WaterCollider3.SetActive(true);

                Collider1.SetActive(false);
                Collider2.SetActive(false);
                Collider3.SetActive(false);
                ControlGround.ContectSeed1 = false;
                ControlGround.ContectSeed2 = false;
                ControlGround.ContectSeed3 = false;
            }
        }
    }

    public void SeedRest()
    {
        fallSeed1.transform.position = Seedposition1;
        fallSeed2.transform.position = Seedposition2;
        fallSeed2.transform.position = Seedposition3;
    }

}