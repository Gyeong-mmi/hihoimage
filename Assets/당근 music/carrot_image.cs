using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class carrot_image : MonoBehaviour
{
    public AudioSource carrotAudio;

    [Header("[BLACK]")]
    public GameObject black1;

    public GameObject black2;

    public GameObject black3;
    public GameObject black4;
    public GameObject black5;

    public GameObject black6;
    public GameObject black7;
    public GameObject black8;

    [Header("[BLUE]")]
    public GameObject blue1;
    public GameObject blue2;
    public GameObject blue3;
    public GameObject blue4;

    public GameObject blue5;
    public GameObject blue6;
    public GameObject blue7;
    public GameObject blue8;

    public GameObject blue9;
    public GameObject blue10;
    public GameObject blue11;
    public GameObject blue12;

    [Header("[RED]")]
    public GameObject red1;
    public GameObject red2;
    public GameObject red3;
    public GameObject red4;

    public GameObject red5;
    public GameObject red6;
    public GameObject red7;

    public GameObject red8;
    public GameObject red9;
    public GameObject red10;

    void Start()
    {
        if (carrotAudio == null)
        {
            carrotAudio = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        float currentTime = carrotAudio.time;

        if ((currentTime >= 10.7f && currentTime < 12.9f) || (currentTime >= 15.1f && currentTime < 17.3f))
        {
            black1.SetActive(true);
        }
        else
        {
            black1.SetActive(false);
        }

        if (currentTime >= 41.6f && currentTime < 43.8f)
        {
            black2.SetActive(true);
        }
        else
        {
            black2.SetActive(false);
        }

        if (currentTime >= 17.3f && currentTime < 21.5f)
        {
            red1.SetActive(true);
            red2.SetActive(true);
            red3.SetActive(true);
            red4.SetActive(true);
        }
        else
        {
            red1.SetActive(false);
            red2.SetActive(false);
            red3.SetActive(false);
            red4.SetActive(false);
        }

        if (currentTime >= 23.7f && currentTime < 26.2f)
        {
            blue1.SetActive(true);
            blue2.SetActive(true);
            blue3.SetActive(true);
            blue4.SetActive(true);

            red5.SetActive(true);
            red6.SetActive(true);
            red7.SetActive(true);
        }
        else
        {
            blue1.SetActive(false);
            blue2.SetActive(false);
            blue3.SetActive(false);
            blue4.SetActive(false);

            red5.SetActive(false);
            red6.SetActive(false);
            red7.SetActive(false);
        }

        if (currentTime >= 28.4f && currentTime < 30.6f)
        {
            blue5.SetActive(true);
            blue6.SetActive(true);
            blue7.SetActive(true);
            blue8.SetActive(true);
        }
        else
        {
            blue5.SetActive(false);
            blue6.SetActive(false);
            blue7.SetActive(false);
            blue8.SetActive(false);
        }

        if (currentTime >= 32.8f && currentTime < 35f)
        {
            blue9.SetActive(true);
            blue10.SetActive(true);
            blue11.SetActive(true);
            blue12.SetActive(true);

            red8.SetActive(true);
            red9.SetActive(true);
            red10.SetActive(true);
        }
        else
        {
            blue9.SetActive(false);
            blue10.SetActive(false);
            blue11.SetActive(false);
            blue12.SetActive(false);

            red8.SetActive(false);
            red9.SetActive(false);
            red10.SetActive(false);
        }

        if (currentTime >= 43.8f && currentTime < 47.8f)
        {
            black3.SetActive(true);
            black4.SetActive(true);
            black5.SetActive(true);
        }
        else
        {
            black3.SetActive(false);
            black4.SetActive(false);
            black5.SetActive(false);
        }

        if (currentTime >= 47.8f && currentTime < 58f)
        {
            black6.SetActive(true);
            black7.SetActive(true);
            black8.SetActive(true);
        }
        else
        {
            black6.SetActive(false);
            black7.SetActive(false);
            black8.SetActive(false);
        }
    }
}
