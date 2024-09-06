using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicon : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        Invoke("PlayAudio", 3f);
    }

    void PlayAudio()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}