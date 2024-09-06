using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameSuccess_worm : MonoBehaviour
{
    public GameObject worm1;
    public GameObject worm2;
    public GameObject worm3;

    public GameObject Success_text;

    public GameObject Success_Audio;

    

    public static bool flag = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (worm1.activeSelf && worm2.activeSelf && worm3.activeSelf && flag == false)
        {
            Success_Audio.SetActive(true);

            StartCoroutine(Success());
        }
    }

    IEnumerator Success()
    {
        
        Success_text.SetActive(true);
        yield return new WaitForSeconds(7f);

        Success_text.SetActive(false);
        Success_Audio.SetActive(false);
        
        flag = true;
    }
}
