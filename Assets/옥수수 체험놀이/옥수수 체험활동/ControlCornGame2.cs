using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlCornGame2 : MonoBehaviour
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

    public GameObject Corn1;
    public GameObject Corn2;
    public GameObject Corn3;


    int num = 0;
    float distance;

    private Vector3 Seedposition1;
    private Vector3 Seedposition2;
    private Vector3 Seedposition3;

    public static bool ContectWater1 = false;
    public static bool ContectWater2 = false;
    public static bool ContectWater3 = false;


    private void Start()
    {
        Seedposition1 = fallSeed1.transform.position;
        Seedposition2 = fallSeed2.transform.position;
        Seedposition3 = fallSeed3.transform.position;
    }

    private void Update()
    {
        if (ControlGround.ContectSeed1 == true && ControlGround.ContectSeed2 == false && ControlGround.ContectSeed3 == false)
        {
            num = 1;

            fallSeed1.SetActive(true);
            Invoke("WaitFall_Seed", 3.0f);
        }
        else if (ControlGround.ContectSeed2 == true && ControlGround.ContectSeed1 == false && ControlGround.ContectSeed3 == false)
        {
            num = 2;

            fallSeed2.SetActive(true);
            Invoke("WaitFall_Seed", 3.0f);
        }
        else if (ControlGround.ContectSeed3 == true && ControlGround.ContectSeed1 == false && ControlGround.ContectSeed2 == false)
        {
            num = 3;

            fallSeed3.SetActive(true);
            Invoke("WaitFall_Seed", 3.0f);
        }
    }

    void WaitFall_Seed()
    {
        if (num == 1)
        {
            Collider2.SetActive(false);
            Collider3.SetActive(false);
            
            FallSeed1();
        }
        else if (num == 2)
        {
            Collider1.SetActive(false);
            Collider3.SetActive(false);
            
            FallSeed2();
        }
        else if (num == 3)
        {
            Collider1.SetActive(false);
            Collider2.SetActive(false);

            FallSeed3();
        }
    }

    void FallSeed1()
    {
        Vector3 targetPosition = fallSeed1.transform.position - new Vector3(0, 5f, 0); // 목표 위치는 현재 위치에서 아래로 이동
        float moveSpeed = 0.04f; // 이동 속도 조절
        fallSeed1.transform.position = Vector3.Lerp(fallSeed1.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Invoke("WaitSecond", 2.5f);
    }

    void FallSeed2()
    {
        Vector3 targetPosition = fallSeed2.transform.position - new Vector3(0, 5f, 0); // 목표 위치는 현재 위치에서 아래로 이동
        float moveSpeed = 0.04f; // 이동 속도 조절
        fallSeed2.transform.position = Vector3.Lerp(fallSeed2.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Invoke("WaitSecond", 2.5f);
    }

    void FallSeed3()
    {
        Vector3 targetPosition = fallSeed3.transform.position - new Vector3(0, 5f, 0); // 목표 위치는 현재 위치에서 아래로 이동
        float moveSpeed = 0.04f; // 이동 속도 조절
        fallSeed3.transform.position = Vector3.Lerp(fallSeed3.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Invoke("WaitSecond", 2.5f);
    }

    void WaitSecond()
    {
        if (fallSeed1.activeSelf == true && fallSeed2.activeSelf == false && fallSeed3.activeSelf == false)
        {
            Collider1.SetActive(false);
            Collider2.SetActive(true);
            Collider3.SetActive(true);
            ControlGround.ContectSeed1 = false;

            water1.SetActive(true);
            ContectWater1 = true;
        }
        else if (fallSeed2.activeSelf == true && fallSeed1.activeSelf == false && fallSeed3.activeSelf == false)
        {
            Collider1.SetActive(true);
            Collider2.SetActive(false);
            Collider3.SetActive(true);
            ControlGround.ContectSeed2 = false;
            water2.SetActive(true);
            ContectWater2 = true;
        }
        else if (fallSeed3.activeSelf == true && fallSeed1.activeSelf == false && fallSeed2.activeSelf == false)
        {
            Collider1.SetActive(true);
            Collider2.SetActive(true);
            Collider3.SetActive(false);
            ControlGround.ContectSeed3 = false;
            water3.SetActive(true);
            ContectWater3 = true;
        }
        else if (fallSeed1.activeSelf == true && fallSeed2.activeSelf == true && fallSeed3.activeSelf == false)
        {
            Collider1.SetActive(false);
            Collider2.SetActive(false);
            Collider3.SetActive(true);

            if (ControlGround.ContectSeed2 == true)
            {
                water2.SetActive(true);
                ContectWater2 = true;
                ControlGround.ContectSeed2 = false;
            }
            else if (ControlGround.ContectSeed1 == true)
            {
                water1.SetActive(true);
                ContectWater1 = true;
                ControlGround.ContectSeed1 = false;
            }
        }
        else if (fallSeed1.activeSelf == true && fallSeed3.activeSelf == true && fallSeed2.activeSelf == false)
        {
            Collider1.SetActive(false);
            Collider2.SetActive(true);
            Collider3.SetActive(false);
            ControlGround.ContectSeed1 = false;
            ControlGround.ContectSeed2 = false;
            ControlGround.ContectSeed3 = false;

            if (water1.activeSelf == true)
            {
                water3.SetActive(true);
                ContectWater3 = true;
            }
            else
            {
                water1.SetActive(true);
                ContectWater1 = true;
            }
        }
        else if (fallSeed2.activeSelf == true && fallSeed3.activeSelf == true && fallSeed1.activeSelf == false)
        {
            Collider1.SetActive(true);
            Collider2.SetActive(false);
            Collider3.SetActive(false);
            ControlGround.ContectSeed1 = false;
            ControlGround.ContectSeed2 = false;
            ControlGround.ContectSeed3 = false;
            if (water2.activeSelf == true)
            {
                water3.SetActive(true);
                ContectWater3 = true;
            }
            else
            {
                water2.SetActive(true);
                ContectWater2 = true;
            }
        }
        else
        {
            ControlGround.ContectSeed1 = false;
            ControlGround.ContectSeed2 = false;
            ControlGround.ContectSeed3 = false;
            if (ControlGround.ContectSeed1 == true)
            {
                if (ControlGround.ContectSeed2 == true)
                {
                    water3.SetActive(true);
                    ContectWater3 = true;
                }
                else if (water3.activeSelf == true)
                {
                    water2.SetActive(true);
                    ContectWater2 = true;
                }
            }
            else if (water2.activeSelf == true)
            {
                if (water1.activeSelf == true)
                {
                    water3.SetActive(true);
                    ContectWater3 = true;
                }
                else if (water3.activeSelf == true)
                {
                    water2.SetActive(true);
                    ContectWater2 = true;
                }
            }
            else if (water3.activeSelf == true)
            {
                if (water2.activeSelf == true)
                {
                    water1.SetActive(true);
                    ContectWater1 = true;
                }
                else if (water3.activeSelf == true)
                {
                    water2.SetActive(true);
                    ContectWater2 = true;
                }
            }


        }

    }
}
