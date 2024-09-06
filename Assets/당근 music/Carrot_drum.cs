using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot_drum : MonoBehaviour
{
    public AudioSource carrotAudio;

    public GameObject drum_no_anim;
    public GameObject drum_rlrl;
    public GameObject drum_hit;
    public GameObject drum_hit_fast;
    public GameObject drum_rlrlrlr;

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

        if ((currentTime >= 12.1f && currentTime < 12.9f) || (currentTime >= 16.5f && currentTime < 17.3f) || (currentTime >= 43.1f && currentTime < 43.8f))
        {
            drum_no_anim.SetActive(false);
            drum_rlrl.SetActive(false);
            drum_hit_fast.SetActive(false);
            drum_rlrlrlr.SetActive(false);

            drum_hit.SetActive(true);
        }

        else if ((currentTime >= 19.2f && currentTime < 21.5f) || (currentTime >= 28.4f && currentTime < 30.6f))
        {
            drum_no_anim.SetActive(false);
            drum_hit.SetActive(false);
            drum_hit_fast.SetActive(false);
            drum_rlrlrlr.SetActive(false);

            drum_rlrl.SetActive(true);
        }

        else if ((currentTime >= 46.3f && currentTime < 47.6f) || (currentTime >= 50.9f && currentTime < 52.1f))
        {
            drum_no_anim.SetActive(false);
            drum_hit.SetActive(false);
            drum_rlrl.SetActive(false);
            drum_rlrlrlr.SetActive(false);

            drum_hit_fast.SetActive(true);
        }

        else if ((currentTime >= 23.7f && currentTime < 26f) || (currentTime >= 32.8f && currentTime < 35f))
        {
            drum_no_anim.SetActive(false);
            drum_hit.SetActive(false);
            drum_rlrl.SetActive(false);
            drum_hit_fast.SetActive(false);

            drum_rlrlrlr.SetActive(true);
        }
        else
        {
            drum_no_anim.SetActive(true);

            drum_hit.SetActive(false);
            drum_hit_fast.SetActive(false);
            drum_rlrl.SetActive(false);
            drum_rlrlrlr.SetActive(false);
        }
    }
}
