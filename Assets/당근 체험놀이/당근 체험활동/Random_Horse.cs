using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Horse : MonoBehaviour
{
    public GameObject Big;
    public GameObject Small;
    public GameObject actt;

    [Header("[Right Carrot UI]")]
    public GameObject SC1;
    public GameObject SC2;
    public GameObject SC3;
    public GameObject BC1;
    public GameObject BC2;
    public GameObject BC3;

    [Header("[Controller Carrot UI]")]
    public GameObject LeftC;

    void Start()
    {
        
    }

    void ActivateRandomObject()
    {
        // 랜덤으로 0 또는 1을 생성
        int randomIndex = Random.Range(0, 2);

        // 랜덤으로 선택된 인덱스에 해당하는 오브젝트를 활성화
        if (randomIndex == 0)
        {
            Big.SetActive(true);
            Small.SetActive(false);
        }
        else
        {
            Big.SetActive(false);
            Small.SetActive(true);
        }
    }

    void Update()
    {
        // 두 개의 오브젝트가 모두 비활성화된 경우 다시 랜덤으로 하나를 활성화
        if (!Big.activeSelf && !Small.activeSelf && actt.activeSelf)
        {
            ActivateRandomObject();
        }

        Transform B3 = GameObject.Find("B_Carrot_Step3").transform.Find("BC3");
        Transform S3 = GameObject.Find("S_Carrot_Step3").transform.Find("SC3");

        if (B3.gameObject.activeSelf)
        {
            if (BC1.activeSelf && BC2.activeSelf && BC3.activeSelf)
            {
                BC1.SetActive(false);
                LeftC.transform.GetChild(1).gameObject.SetActive(false);
            }
            else if (!BC1.activeSelf && BC2.activeSelf && BC3.activeSelf)
            {
                BC2.SetActive(false);
                LeftC.transform.GetChild(2).gameObject.SetActive(false);
            }
            else if (!BC1.activeSelf && !BC2.activeSelf && BC3.activeSelf)
            {
                BC3.SetActive(false);
                LeftC.transform.GetChild(3).gameObject.SetActive(false);
            }

            HideB();
        }
        else if (S3.gameObject.activeSelf)
        {
            if (SC1.activeSelf && SC2.activeSelf && SC3.activeSelf)
            {
                SC1.SetActive(false);
                LeftC.transform.GetChild(4).gameObject.SetActive(false);
            }
            else if (!SC1.activeSelf && SC2.activeSelf && SC3.activeSelf)
            {
                SC2.SetActive(false);
                LeftC.transform.GetChild(5).gameObject.SetActive(false);
            }
            else if (!SC1.activeSelf && !SC2.activeSelf && SC3.activeSelf)
            {
                SC3.SetActive(false);
                LeftC.transform.GetChild(6).gameObject.SetActive(false);
            }

            HideS();
        }
    }

    private void HideB()
    {
        Transform B0 = GameObject.Find("B_Carrot_Step0").transform.Find("BC0");
        Transform B3 = GameObject.Find("B_Carrot_Step3").transform.Find("BC3");

        Big.SetActive(false);
        B3.gameObject.SetActive(false);
        B0.gameObject.SetActive(true);
    }

    private void HideS()
    {
        Transform S0 = GameObject.Find("S_Carrot_Step0").transform.Find("SC0");
        Transform S3 = GameObject.Find("S_Carrot_Step3").transform.Find("SC3");

        Small.SetActive(false);
        S3.gameObject.SetActive(false);
        S0.gameObject.SetActive(true);
    }
}
