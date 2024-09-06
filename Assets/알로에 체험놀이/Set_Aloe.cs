using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Set_Aloe : MonoBehaviour
{
    public GameObject aloe;
    public GameObject aloe_images;
    public GameObject Feel_aloe;
    public List<GameObject> objectList;
    bool flag = false;
    bool Start_flag = false;
    private Vector3 initPos;
    private Vector3 initialLocalScale;
    AudioSource audioSource;
    public AudioClip[] audiosCharacter;
    public AudioClip[] audiosApperance;
    public AudioClip[] audiosFeel;
    public AudioClip[] audiosPlay;
    private bool isSoundPlaying = false;
    private void Start()
    {
        initPos = aloe.transform.position;
        initialLocalScale = aloe.transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }
    public void Aloe_Apperance()
    {
        if (!isSoundPlaying)
        {
            isSoundPlaying = true;
            Feel_aloe.SetActive(false);
            aloe_images.SetActive(false);
            StartCoroutine(Play_Apperance());
            StartCoroutine(WaitAndActivate_Appearance(8.3f));
        }
    }
    private IEnumerator WaitAndActivate_Appearance(float delay)
    {
        aloe_images.SetActive(false);
        StartCoroutine(ActivateObjectsSequentially(2));
        yield return new WaitForSeconds(delay);
        aloe.SetActive(true);
        isSoundPlaying = false;
    }
    public void Aloe_Character()
    {
        foreach (GameObject obj in objectList)
        {
            obj.SetActive(false);
            flag = false;
            //Debug.Log("obj.SetActive(false); flag: " + flag);
        }
        if (!isSoundPlaying)
        {
            isSoundPlaying = true;
            aloe.SetActive(false);
            Feel_aloe.SetActive(false);
            StartCoroutine(Play_Character());
            StartCoroutine(WaitAndActivate_Character(3.0f));
        }
    }
    private IEnumerator WaitAndActivate_Character(float delay)
    {
        yield return new WaitForSeconds(delay);
        aloe_images.SetActive(true);
        StartCoroutine(ActivateObjectsSequentially(1)); // È°¼ºÈ­ number = 1
        isSoundPlaying = false;
    }
    IEnumerator ActivateObjectsSequentially(int number)
    {
        if (number == 1)
        {
            for (int i = 0; i < objectList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        yield return new WaitForSeconds(0.1f);
                        objectList[i].SetActive(true);
                        break;
                    case 1:
                        yield return new WaitForSeconds(15f);
                        objectList[i].SetActive(true);
                        break;
                    case 2:
                        yield return new WaitForSeconds(22.5f);
                        objectList[i].SetActive(true);
                        break;
                }
            }
        }
        if (number == 2)
        {
            foreach (GameObject obj in objectList)
            {
                obj.SetActive(false);
                flag = false;
                //Debug.Log("obj.SetActive(false); flag: " + flag);
            }
        }
    }
    public void Feel_Aloe()
    {
        if (!isSoundPlaying)
        {
            isSoundPlaying = true;
            aloe.SetActive(false);
            aloe_images.SetActive(false);
            StartCoroutine(Play_Feel());
            StartCoroutine(WaitAndActivate_Feel(7.0f));
        }
    }
    private IEnumerator WaitAndActivate_Feel(float delay)
    {
        yield return new WaitForSeconds(delay);
        Feel_aloe.SetActive(true);
        isSoundPlaying = false;
    }

    public void Aloe_Play()
    {
        if (!isSoundPlaying)
        {
            isSoundPlaying = true;
            Feel_aloe.SetActive(false);
            aloe_images.SetActive(false);
            aloe.SetActive(false);
            StartCoroutine(Play_Play());
            StartCoroutine(WaitAndActivate_Play(8.3f));
        }
    }
    private IEnumerator WaitAndActivate_Play(float delay)
    {
        aloe_images.SetActive(false);
        StartCoroutine(ActivateObjectsSequentially(2));
        yield return new WaitForSeconds(delay);
        aloe.SetActive(true);
        isSoundPlaying = false;
    }

    IEnumerator Play_Apperance()
    {
        for (int i = 0; i < audiosApperance.Length; i++)
        {
            audioSource.clip = audiosApperance[i];
            audioSource.Play();
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
    IEnumerator Play_Character()
    {
        for (int i = 0; i < audiosCharacter.Length; i++)
        {
            audioSource.clip = audiosCharacter[i];
            audioSource.Play();
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
    IEnumerator Play_Feel()
    {
        for (int i = 0; i < audiosFeel.Length; i++)
        {
            audioSource.clip = audiosFeel[i];
            audioSource.Play();
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }

    IEnumerator Play_Play()
    {
        for (int i = 0; i < audiosPlay.Length; i++)
        {
            audioSource.clip = audiosPlay[i];
            audioSource.Play();
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
}