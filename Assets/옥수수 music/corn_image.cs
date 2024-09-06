using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class corn_image : MonoBehaviour
{
    public AudioSource cornAudio;

    [Header("[BLACK ARROW]")]
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
    public GameObject black19;

    public GameObject black20;
    public GameObject black21;
    public GameObject black22;

    public GameObject black23;

    public GameObject black24;
    public GameObject black25;
    public GameObject black26;
    public GameObject black27;
    public GameObject black28;

    [Header("[BLUE ARROW]")]
    public GameObject blue1;
    public GameObject blue2;

    public GameObject blue3;

    [Header("[RED ARROW]")]
    public GameObject red1;
    public GameObject red2;

    public GameObject red3;
    public GameObject red4;

    public GameObject red5;

    void Start()
    {
        if (cornAudio == null)
        {
            cornAudio = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        float currentTime = cornAudio.time;

        if ((currentTime >= 8.8f && currentTime < 10.8f) || (currentTime >= 43.5f && currentTime < 45.5f))
        {
            black1.SetActive(true);
            black2.SetActive(true);

            blue1.SetActive(true);
            blue2.SetActive(true);
        }
        else
        {
            black1.SetActive(false);
            black2.SetActive(false);

            blue1.SetActive(false);
            blue2.SetActive(false);
        }

        if (currentTime >= 10.8f && currentTime < 12.8f)
        {
            black3.SetActive(true);
            black4.SetActive(true);

            red1.SetActive(true);
            red2.SetActive(true);
        }
        else
        {
            black3.SetActive(false);
            black4.SetActive(false);

            red1.SetActive(false);
            red2.SetActive(false);
        }

        if (currentTime >= 12.8f && currentTime < 16.3f)
        {
            black5.SetActive(true);
            black6.SetActive(true);
            black7.SetActive(true);
            black8.SetActive(true);
        }
        else
        {
            black5.SetActive(false);
            black6.SetActive(false);
            black7.SetActive(false);
            black8.SetActive(false);
        }

        if ((currentTime >= 16.3f && currentTime < 17.3f) || (currentTime >= 24.8f && currentTime < 25.9f))
        {
            black15.SetActive(true);
            black16.SetActive(true);
            black17.SetActive(true);
        }
        else
        {
            black15.SetActive(false);
            black16.SetActive(false);
            black17.SetActive(false);
        }

        if (currentTime >= 17.3f && currentTime < 19.3f)
        {
            black9.SetActive(true);
            black10.SetActive(true);
            black11.SetActive(true);
            black12.SetActive(true);
        }
        else
        {
            black9.SetActive(false);
            black10.SetActive(false);
            black11.SetActive(false);
            black12.SetActive(false);
        }

        if ((currentTime >= 47.5f && currentTime < 49.5f) || (currentTime >= 51.5f && currentTime < 53.7f))
        {
            black13.SetActive(true);
            black14.SetActive(true);
        }
        else
        {
            black13.SetActive(false);
            black14.SetActive(false);
        }

        if (currentTime >= 28.3f && currentTime < 30.5f)
        {
            blue3.SetActive(true);
            red5.SetActive(true);

            black18.SetActive(true);
            black19.SetActive(true);
        }
        else
        {
            blue3.SetActive(false);
            red5.SetActive(false);

            black18.SetActive(false);
            black19.SetActive(false);
        }

        if ((currentTime >= 32.7f && currentTime < 34.9f) || (currentTime >= 36.9f && currentTime < 39.1f))
        {
            black24.SetActive(true);
            black25.SetActive(true);
            black26.SetActive(true);
            black27.SetActive(true);
            black28.SetActive(true);
        }
        else
        {
            black24.SetActive(false);
            black25.SetActive(false);
            black26.SetActive(false);
            black27.SetActive(false);
            black28.SetActive(false);
        }

        if (currentTime >= 41.3f && currentTime < 43.5f)
        {
            black20.SetActive(true);
            black21.SetActive(true);
            black22.SetActive(true);
        }
        else
        {
            black20.SetActive(false);
            black21.SetActive(false);
            black22.SetActive(false);
        }

        if (currentTime >= 45.5f && currentTime < 47.5f)
        {
            red3.SetActive(true);
            red4.SetActive(true);
        }
        else
        {
            red3.SetActive(false);
            red4.SetActive(false);
        }

        if ((currentTime >= 49.5f && currentTime < 51.5f) || (currentTime >= 53.7f && currentTime < 58f))
        {
            black23.SetActive(true);
        }
        else
        {
            black23.SetActive(false);
        }
    }
}
