using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aloe_image : MonoBehaviour
{
    public AudioSource aloeAudio;

    [Header("[BLACK]")]
    public GameObject black1;
    public GameObject black2;
    public GameObject black3;

    public GameObject black4;
    public GameObject black5;
    public GameObject black6;

    public GameObject black7;
    public GameObject black8;

    public GameObject black9;
    public GameObject black10;

    public GameObject black11;
    public GameObject black12;

    public GameObject black13;
    public GameObject black14;
    public GameObject black15;

    public GameObject black16;
    public GameObject black17;
    public GameObject black18;

    [Header("[BLUE]")]
    public GameObject blue1;
    public GameObject blue2;

    [Header("[RED]")]
    public GameObject red1;
    public GameObject red2;

    [Header("[TURTLE]")]
    public GameObject turttle1;
    public GameObject green1;

    void Start()
    {
        if (aloeAudio == null)
        {
            aloeAudio = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        float currentTime = aloeAudio.time;

        if (currentTime >= 9f && currentTime < 14.3f)
        {
            black1.SetActive(true);
            black2.SetActive(true);
            black3.SetActive(true);
        }
        else
        {
            black1.SetActive(false);
            black2.SetActive(false);
            black3.SetActive(false);
        }

        if (currentTime >= 14.3f && currentTime < 18.8f)
        {
            black4.SetActive(true);
            black5.SetActive(true);
            black6.SetActive(true);
        }
        else
        {
            black4.SetActive(false);
            black5.SetActive(false);
            black6.SetActive(false);
        }

        if (currentTime >= 23.8f && currentTime < 28.3f)
        {
            black7.SetActive(true);
            black8.SetActive(true);
        }
        else
        {
            black7.SetActive(false);
            black8.SetActive(false);
        }

        if ((currentTime >= 33.3f && currentTime < 35.8f) || (currentTime >= 38.3f && currentTime < 40.8f))
        {
            black9.SetActive(true);
            black10.SetActive(true);

            blue1.SetActive(true);
            blue2.SetActive(true);
        }
        else
        {
            black9.SetActive(false);
            black10.SetActive(false);

            blue1.SetActive(false);
            blue2.SetActive(false);
        }

        if ((currentTime >= 35.8f && currentTime < 38.3f) || (currentTime >= 40.8f && currentTime < 43.3f))
        {
            black11.SetActive(true);
            black12.SetActive(true);

            red1.SetActive(true);
            red2.SetActive(true);
        }
        else
        {
            black11.SetActive(false);
            black12.SetActive(false);

            red1.SetActive(false);
            red2.SetActive(false);
        }

        if ((currentTime >= 50.4f && currentTime < 57.4f) || (currentTime >= 59.9f && currentTime < 66.9f))
        {
            turttle1.SetActive(true);
            green1.SetActive(true);
        }
        else
        {
            turttle1.SetActive(false);
            green1.SetActive(false);
        }

        if (currentTime >= 66.9f && currentTime < 71.9f)
        {
            black13.SetActive(true);
            black14.SetActive(true);
            black15.SetActive(true);
        }
        else
        {
            black13.SetActive(false);
            black14.SetActive(false);
            black15.SetActive(false);
        }

        if (currentTime >= 71.9f && currentTime < 76.9f)
        {
            black16.SetActive(true);
            black17.SetActive(true);
            black18.SetActive(true);
        }
        else
        {
            black16.SetActive(false);
            black17.SetActive(false);
            black18.SetActive(false);
        }
    }
}
