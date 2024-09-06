using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carrot_act : MonoBehaviour
{
    public GameObject carrot01;
    public GameObject carrot02;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("carrot", 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void carrot()
    {
        carrot01.SetActive(false);
        carrot02.SetActive(true);
    }
}
