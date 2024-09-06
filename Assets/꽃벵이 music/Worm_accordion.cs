using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm_accordion : MonoBehaviour
{
    public AudioSource wormAudio;

    public GameObject no_anim;
    public GameObject last_anim;
    public GameObject slow_anim;
    public GameObject normal_anim;
    public GameObject double_anim;

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

        if ((currentTime >= 9.1f && currentTime < 12.7f) || (currentTime >= 39.3f && currentTime < 43.2f) || (currentTime >= 49.96f && currentTime < 53.4f))
        {
            no_anim.SetActive(false);
            last_anim.SetActive(false);
            slow_anim.SetActive(false);
            normal_anim.SetActive(true);
            double_anim.SetActive(false);
        }

        else if ((currentTime >= 14.1f && currentTime < 19.2f) || (currentTime >= 33f && currentTime < 34.4f) || (currentTime >= 35.3f && currentTime < 38.7f) || (currentTime >= 44.3f && currentTime < 49.4f))
        {
            no_anim.SetActive(false);
            last_anim.SetActive(false);
            slow_anim.SetActive(true);
            normal_anim.SetActive(false);
            double_anim.SetActive(false);
        }

        else if (currentTime >= 27.5f && currentTime < 29.7f)  //°ÅºÏÀÌ Åä³¢ °°ÀÌ
        {
            no_anim.SetActive(false);
            last_anim.SetActive(false);
            slow_anim.SetActive(false);
            normal_anim.SetActive(false);
            double_anim.SetActive(true);
        }

        else if (currentTime >= 29.8f && currentTime < 32f)  //°ÅºÏÀÌ Åä³¢ °°ÀÌ
        {
            no_anim.SetActive(false);
            last_anim.SetActive(false);
            slow_anim.SetActive(false);
            normal_anim.SetActive(false);
            double_anim.SetActive(true);
        }

        else if (currentTime >= 55.4f && currentTime < 57.3f)
        {
            no_anim.SetActive(false);
            last_anim.SetActive(true);
            slow_anim.SetActive(false);
            normal_anim.SetActive(false);
            double_anim.SetActive(false);
        }

        else
        {
            no_anim.SetActive(true);
            last_anim.SetActive(false);
            slow_anim.SetActive(false);
            normal_anim.SetActive(false);
            double_anim.SetActive(false);
        }
    }
}
