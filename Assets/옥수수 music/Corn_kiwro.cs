using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn_kiwro : MonoBehaviour
{
    public AudioSource cornAudio;

    public GameObject kiwro_no_anim;
    public GameObject kiwro_p;
    public GameObject kiwro_hit;
    public GameObject kiwro_hit_fast;
    public GameObject kiwro_hit_ffast;
    public GameObject kiwro_hit_soft;

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

        if ((currentTime >= 8.7f && currentTime < 16.3f) || (currentTime >= 17.2f && currentTime < 19.3f) || (currentTime >= 43.4f && currentTime < 48.7f) || (currentTime >= 51.4f && currentTime < 52.9f))
        {
            kiwro_no_anim.SetActive(false);
            kiwro_hit.SetActive(false);
            kiwro_hit_fast.SetActive(false);
            kiwro_hit_ffast.SetActive(false);
            kiwro_hit_soft.SetActive(false);

            kiwro_p.SetActive(true);
        }

        else if (currentTime >= 28.3f && currentTime < 30.5f)
        {
            kiwro_no_anim.SetActive(false);
            kiwro_hit.SetActive(false);
            kiwro_p.SetActive(false);
            kiwro_hit_ffast.SetActive(false);
            kiwro_hit_fast.SetActive(false);

            kiwro_hit_soft.SetActive(true);
        }

        else if ((currentTime >= 16f && currentTime < 17.3f) || (currentTime >= 24.7f && currentTime < 25.9f) || (currentTime >= 42.2f && currentTime < 43.5f))
        {
            kiwro_no_anim.SetActive(false);
            kiwro_hit.SetActive(false);
            kiwro_p.SetActive(false);
            kiwro_hit_ffast.SetActive(false);
            kiwro_hit_soft.SetActive(false);

            kiwro_hit_fast.SetActive(true);
        }

        else if ((currentTime >= 33.8f && currentTime < 34.9f) || (currentTime >= 38f && currentTime < 39.1f))
        {
            kiwro_no_anim.SetActive(false);
            kiwro_hit.SetActive(false);
            kiwro_p.SetActive(false);
            kiwro_hit_fast.SetActive(false);
            kiwro_hit_soft.SetActive(false);

            kiwro_hit_ffast.SetActive(true);
        }

        else if ((currentTime >= 50.7f && currentTime < 51.5f) || (currentTime >= 54.9f && currentTime < 55.7f))
        {
            kiwro_no_anim.SetActive(false);
            kiwro_hit_fast.SetActive(false);
            kiwro_p.SetActive(false);
            kiwro_hit_ffast.SetActive(false);
            kiwro_hit_soft.SetActive(false);

            kiwro_hit.SetActive(true);
        }
        else
        {
            kiwro_no_anim.SetActive(true);

            kiwro_hit_fast.SetActive(false);
            kiwro_p.SetActive(false);
            kiwro_hit.SetActive(false);
            kiwro_hit_ffast.SetActive(false);
            kiwro_hit_soft.SetActive(false);
        }
    }
}
