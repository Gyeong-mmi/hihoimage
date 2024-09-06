using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmButtonController : MonoBehaviour
{
    public GameObject Ground;
    private bool StartSet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WarmStart()
    {
        if(!StartSet)
        {
            Ground.SetActive(true);
            StartSet = true;
        }
    }
}
