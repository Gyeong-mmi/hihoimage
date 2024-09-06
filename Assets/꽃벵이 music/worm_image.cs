using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class worm_image : MonoBehaviour
{
    public AudioSource wormAudio;

    [Header("[TURTLE]")]
    public GameObject turtle1;
    public GameObject turtle2;
    public GameObject turtle3;

    [Header("[RABBIT]")]
    public GameObject rabbit1;
    public GameObject rabbit2;
    public GameObject rabbit3;
    public GameObject rabbit4;
    public GameObject rabbit5;

    [Header("[BLACK ARROW]")]
    public GameObject black1;
    public GameObject black2;
    public GameObject black3;
    public GameObject black4;
    public GameObject black5;
    public GameObject black6;

    [Header("[GREEN ARROW]")]
    public GameObject green1;
    public GameObject green2;
    public GameObject green3;
    public GameObject green4;
    public GameObject green5;
    public GameObject green6;

    [Header("[PINK ARROW]")]
    public GameObject pink1;
    public GameObject pink2;
    public GameObject pink3;
    public GameObject pink4;
    public GameObject pink5;
    public GameObject pink6;
    public GameObject pink7;
    public GameObject pink8;
    public GameObject pink9;
    public GameObject pink10;

    void Start()
    {
        if (wormAudio == null)
        {
            wormAudio = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        float currentTime = wormAudio.time;

        if ((currentTime >= 8.2f && currentTime < 12.7f) || (currentTime >= 38.7f && currentTime < 43.2f) || (currentTime >= 49.4f && currentTime < 53.4f))
        {
            black1.SetActive(true);
            black2.SetActive(true);
            black3.SetActive(true);
            black4.SetActive(true);
            black5.SetActive(true);
            black6.SetActive(true);
        }
        else
        {
            black1.SetActive(false);
            black2.SetActive(false);
            black3.SetActive(false);
            black4.SetActive(false);
            black5.SetActive(false);
            black6.SetActive(false);
        }

        if ((currentTime >= 12.7f && currentTime < 19.2f) || (currentTime >= 43.2f && currentTime < 49.4f))
        {
            turtle1.SetActive(true);
            green1.SetActive(true);
            green2.SetActive(true);
        }
        else
        {
            turtle1.SetActive(false);
            green1.SetActive(false);
            green2.SetActive(false);
        }

        if (currentTime >= 27.5f && currentTime < 32.1f)
        {
            turtle2.SetActive(true);
            green3.SetActive(true);
            green4.SetActive(true);
        }
        else
        {
            turtle2.SetActive(false);
            green3.SetActive(false);
            green4.SetActive(false);
        }

        if (currentTime >= 32.1f && currentTime < 38.7f)
        {
            turtle3.SetActive(true);
            green5.SetActive(true);
            green6.SetActive(true);
        }
        else
        {
            turtle3.SetActive(false);
            green5.SetActive(false);
            green6.SetActive(false);
        }


        if (currentTime >= 27.5f && currentTime < 32.1f)
        {
            rabbit1.SetActive(true);
            rabbit2.SetActive(true);
            pink1.SetActive(true);
            pink2.SetActive(true);
            pink3.SetActive(true);
            pink4.SetActive(true);
        }
        else
        {
            rabbit1.SetActive(false);
            rabbit2.SetActive(false);
            pink1.SetActive(false);
            pink2.SetActive(false);
            pink3.SetActive(false);
            pink4.SetActive(false);
        }

        if (currentTime >= 53.4f)
        {
            rabbit3.SetActive(true);
            rabbit4.SetActive(true);
            rabbit5.SetActive(true);
            pink5.SetActive(true);
            pink6.SetActive(true);
            pink7.SetActive(true);
            pink8.SetActive(true);
            pink9.SetActive(true);
            pink10.SetActive(true);
        }
        else
        {
            rabbit3.SetActive(false);
            rabbit4.SetActive(false);
            rabbit5.SetActive(false);
            pink5.SetActive(false);
            pink6.SetActive(false);
            pink7.SetActive(false);
            pink8.SetActive(false);
            pink9.SetActive(false);
            pink10.SetActive(false);
        }
    }
}
