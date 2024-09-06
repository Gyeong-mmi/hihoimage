using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
public class Set_Corn : MonoBehaviour
{
    public GameObject corn;
    public GameObject Character_corn;
    public GameObject Feel_corn;
    public List<GameObject> objectList;
    bool flag = false;
    bool Start_flag = false;
    private Vector3 initPos;
    private Vector3 initialLocalScale;
    AudioSource audioSource;
    public AudioClip[] audiosCharacter;
    public AudioClip[] audiosApperance;
    public AudioClip[] audiosFeel;
    public AudioClip[] audiosGame;

    public GameObject Gamecorn;
    public GameObject CornSet;  //옥수수 밭 제거
    private void Start()
    {
        initPos = corn.transform.position;
        initialLocalScale = corn.transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }
    public void Corn_Apperance()
    {
        Character_corn.SetActive(false);
        Feel_corn.SetActive(false);
        Gamecorn.SetActive(false);
        CornSet.SetActive(true);
        StartCoroutine("Play_Apperance");
        StartCoroutine(WaitAndActivate(8.0f));
    }
    private IEnumerator WaitAndActivate(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(ActivateObjectsSequentially(2));
        corn.SetActive(true);
    }
    public void Corn_Character()
    {
        StopAllCoroutines();
        foreach (GameObject obj in objectList)
        {
            obj.SetActive(false);
            flag = false;
            //Debug.Log("obj.SetActive(false); flag: " + flag);
        }

        Gamecorn.SetActive(false);
        CornSet.SetActive(true);
        corn.SetActive(false);
        Feel_corn.SetActive(false);
        Character_corn.SetActive(true);
        StartCoroutine("Play_Character");
        StartCoroutine(ActivateObjectsSequentially(1));
    }
    public void Corn_Feel()
    {
        Gamecorn.SetActive(false);
        CornSet.SetActive(true);
        corn.SetActive(false);
        Character_corn.SetActive(false);
        StartCoroutine("Play_Feel");
        StartCoroutine(WaitAndActivate_Feel(5.5f));
    }
    private IEnumerator WaitAndActivate_Feel(float delay)
    {
        yield return new WaitForSeconds(delay);
        Feel_corn.SetActive(true);
        StartCoroutine(ActivateObjectsSequentially(2));
    }
    public void Corn_Game()
    {
        gameSuccess_corn.flag = false;
        StartCoroutine("Play_Game");
        CornSet.SetActive(false);
        Feel_corn.SetActive(false);
        corn.SetActive(false);
        Character_corn.SetActive(false);
        StartCoroutine(WaitAndActivate_Game());
    }

    IEnumerator WaitAndActivate_Game()
    {
        yield return new WaitForSeconds(5.0f);
        Gamecorn.SetActive(true);
    }
    IEnumerator ActivateObjectsSequentially(int number)
    {
        if (number == 1)
        {
            for (int i = 0; i < objectList.Count; i++)
            {
                // //////////////////////////////
                switch (i)
                {
                    case 0:
                        yield return new WaitForSeconds(11f);
                        objectList[i].SetActive(true); //씨앗 생성
                        break;
                    case 1:
                        yield return new WaitForSeconds(10.6f);
                        //씨앗 낙하
                        GameObject gameObject = objectList[0];
                        Vector3 targetPosition = gameObject.transform.position - new Vector3(0f, 1.3f, 0f);
                        float lerpTime = 0f;
                        float duration = 3.8f;
                        while (lerpTime < duration)
                        {
                            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition, lerpTime / duration);
                            lerpTime += Time.deltaTime * 2.5f;
                            yield return null;
                        }
                        // 정확한 위치로 보정
                        gameObject.transform.position = targetPosition;
                        objectList[0].SetActive(false);
                        gameObject.transform.position = targetPosition + new Vector3(0f, 1.3f, 0f);
                        objectList[i].SetActive(true); //두번째 새싹 생성
                        break;
                    case 2:
                        yield return new WaitForSeconds(9.5f);
                        objectList[1].SetActive(false);
                        objectList[i].SetActive(true);
                        break;
                    case 3:
                        yield return new WaitForSeconds(5.2f);
                        objectList[2].SetActive(false);
                        objectList[i].SetActive(true);
                        break;
                }
            }
        }
        // ActivateObjectsSequentially 코루틴이 끝나면 모든 오브젝트를 비활성화합니다.
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
    IEnumerator Play_Apperance()
    {
        for (int i = 0; i < audiosApperance.Length; i++)
        {
            audioSource.clip = audiosApperance[i];
            audioSource.Play();
            // 현재 클립이 재생 중일 때까지 기다립니다.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
    IEnumerator Play_Character()
    {
        for (int i = 0; i < audiosCharacter.Length; i++)
        {
            audioSource.clip = audiosCharacter[i];
            audioSource.Play();
            // 현재 클립이 재생 중일 때까지 기다립니다.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
    IEnumerator Play_Feel()
    {
        for (int i = 0; i < audiosFeel.Length; i++)
        {
            audioSource.clip = audiosFeel[i];
            audioSource.Play();
            // 현재 클립이 재생 중일 때까지 기다립니다.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
    IEnumerator Play_Game()
    {
        for (int i = 0; i < audiosGame.Length; i++)
        {
            audioSource.clip = audiosGame[i];
            audioSource.Play();
            // 현재 클립이 재생 중일 때까지 기다립니다.
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
}