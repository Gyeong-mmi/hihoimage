using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textchange : MonoBehaviour
{
    public GameObject[] text;
    public float sec;
    void Start()
    {
        Invoke("Texton", sec);
        Invoke("Textoff", sec);
    }

    void Texton()
    {
        text[1].SetActive(true);
    }

    void Textoff()
    {
        text[0].SetActive(false);
    }
}
