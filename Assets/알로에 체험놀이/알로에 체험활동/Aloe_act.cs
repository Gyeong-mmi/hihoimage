using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aloe_act : MonoBehaviour
{
    public GameObject accct;
    public GameObject Aloe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(accct.activeSelf)
        {
            Aloe.SetActive(true);
        }
    }
}
