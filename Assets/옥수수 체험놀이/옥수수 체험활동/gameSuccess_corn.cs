using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class gameSuccess_corn : MonoBehaviour
{
    public GameObject corn_active1;
    public GameObject corn_active2;
    public GameObject corn_active3;

    public GameObject audio;

    

    public static bool flag = false;

    public GameObject Success_text;


    private void Update()
    {
        if (corn_active1.activeSelf && corn_active2.activeSelf && corn_active3.activeSelf && flag == false)
        {
            audio.SetActive(true);
            Success_text.SetActive(true);

            StartCoroutine(Success());
        }
    }

    IEnumerator Success()
    {

        
        yield return new WaitForSeconds(8f);
        Success_text.SetActive(false);
        audio.SetActive(false);
        

        flag = true;
    }
}
