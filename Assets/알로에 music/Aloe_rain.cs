using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aloe_rain : MonoBehaviour
{
    public AudioSource aloeAudio;

    public GameObject rain_no_anim;
    public GameObject rain_shake;
    public GameObject rain_shake_fast;
    public GameObject rain_tilt;
    public GameObject rain_shake_ffast;

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

        if ((currentTime >= 33.2f && currentTime < 43.3f))
        {
            rain_no_anim.SetActive(false);
            rain_shake_fast.SetActive(false);
            rain_tilt.SetActive(false);
            rain_shake_ffast.SetActive(false);
            rain_shake.SetActive(true);
        }

        else if ((currentTime >= 50.3f && currentTime < 57.4f) || (currentTime >= 59.8f && currentTime < 66.9f))
        {
            rain_no_anim.SetActive(false);
            rain_shake_fast.SetActive(false);
            rain_shake.SetActive(false);
            rain_shake_ffast.SetActive(false);
            rain_tilt.SetActive(true);
        }

        else if (currentTime >= 12.3f && currentTime < 14.3f)
        {
            rain_no_anim.SetActive(false);
            rain_shake.SetActive(false);
            rain_tilt.SetActive(false);
            rain_shake_fast.SetActive(false);
            rain_shake_ffast.SetActive(true);
        }

        else if ((currentTime >= 16.5f && currentTime < 18.8f) || (currentTime >= 24.9f && currentTime < 26.4f) || (currentTime >= 69.4f && currentTime < 71.8f) || (currentTime >= 74.2f && currentTime < 76.7f))
        {
            rain_no_anim.SetActive(false);
            rain_shake.SetActive(false);
            rain_tilt.SetActive(false);
            rain_shake_ffast.SetActive(false);
            rain_shake_fast.SetActive(true);
        }
        else
        {
            rain_no_anim.SetActive(true);
            rain_shake_ffast.SetActive(false);
            rain_shake.SetActive(false);
            rain_tilt.SetActive(false);
            rain_shake_fast.SetActive(false);
        }
    }
}
