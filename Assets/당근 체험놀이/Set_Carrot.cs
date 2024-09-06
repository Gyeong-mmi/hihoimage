using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Set_Carrot : MonoBehaviour
{
    public GameObject Apperance_carrot;
    public GameObject map;
    public GameObject Feel_carrot;
    public GameObject Move_carrot;
    public List<GameObject> objectList;

    bool flag = false;
    bool Start_flag = false;
    private Vector3 initPos;
    private Vector3 initialLocalScale;
    private Vector3 position;

    public GameObject targetPosition; // �̵��� ��ǥ ��ġ
    public GameObject carrotPrefab; // ������ ��� ������
    Quaternion rotation;

    AudioSource audioSource;
    public AudioClip[] audiosCharacter;
    public AudioClip[] audiosApperance;
    public AudioClip[] audiosFeel;
    public AudioClip[] audiosPlay;
    private void Start()
    {
        initPos = Apperance_carrot.transform.position;
        initialLocalScale = Apperance_carrot.transform.localScale;
        audioSource = GetComponent<AudioSource>();

        position = targetPosition.transform.position;
        rotation = Quaternion.Euler(0, 90f, 0);
    }
    public void Carrot_Apperance()
    {
        Feel_carrot.SetActive(false);
        StartCoroutine("Play_Apperance");
        StartCoroutine(WaitAndActivate_Appearance(8.3f));
    }
    private IEnumerator WaitAndActivate_Appearance(float delay)
    {
        map.SetActive(false);
        StartCoroutine(ActivateObjectsSequentially(2));
        yield return new WaitForSeconds(delay);
        Apperance_carrot.SetActive(true);
    }
    public void Carrot_Character()
    {
        Apperance_carrot.SetActive(false);
        Feel_carrot.SetActive(false);
        StartCoroutine("Play_Character");
        StartCoroutine(WaitAndActivate_Character(3.2f));
    }
    private IEnumerator WaitAndActivate_Character(float delay)
    {
        yield return new WaitForSeconds(delay);
        map.SetActive(true);
        StartCoroutine(ActivateObjectsSequentially(1)); // Ȱ��ȭ number = 1
    }
    IEnumerator ActivateObjectsSequentially(int number)
    {
        if (number == 1)
        {
            foreach (GameObject obj in objectList)
            {
                yield return new WaitForSeconds(1.5f);
                obj.SetActive(true);
                flag = true;
                //Debug.Log("obj.SetActive(true); flag: " + flag);
            }
        }
        // ActivateObjectsSequentially �ڷ�ƾ�� ������ ��� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
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

    private GameObject currentCarrotInstance;
    public void Feel_Carrot()
    {
        Apperance_carrot.SetActive(false);
        map.SetActive(false);
        StartCoroutine("Play_Feel");
        StartCoroutine(WaitAndActivate_Feel(5.5f));
    }
    private IEnumerator WaitAndActivate_Feel(float delay)
    {
        yield return new WaitForSeconds(delay);

        Move_carrot.SetActive(false);
        Feel_carrot.SetActive(true);
        StartCoroutine(ActivateObjectsSequentially(2));
    }

    public void Carrot_Play()
    {
        Feel_carrot.SetActive(false);
        StartCoroutine("Play_Play");
        StartCoroutine(WaitAndActivate_Play(7.3f));
    }
    private IEnumerator WaitAndActivate_Play(float delay)
    {
        map.SetActive(false);
        StartCoroutine(ActivateObjectsSequentially(2));
        yield return new WaitForSeconds(delay);
    }

    IEnumerator Play_Apperance()
    {
        for (int i = 0; i < audiosApperance.Length; i++)
        {
            audioSource.clip = audiosApperance[i];
            audioSource.Play();
            // ���� Ŭ���� ��� ���� ������ ��ٸ��ϴ�.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
    IEnumerator Play_Character()
    {
        for (int i = 0; i < audiosCharacter.Length; i++)
        {
            audioSource.clip = audiosCharacter[i];
            audioSource.Play();
            // ���� Ŭ���� ��� ���� ������ ��ٸ��ϴ�.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
    IEnumerator Play_Feel()
    {
        for (int i = 0; i < audiosFeel.Length; i++)
        {
            audioSource.clip = audiosFeel[i];
            audioSource.Play();
            // ���� Ŭ���� ��� ���� ������ ��ٸ��ϴ�.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }

    IEnumerator Play_Play()
    {
        for (int i = 0; i < audiosPlay.Length; i++)
        {
            audioSource.clip = audiosPlay[i];
            audioSource.Play();
            // ���� Ŭ���� ��� ���� ������ ��ٸ��ϴ�.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
}