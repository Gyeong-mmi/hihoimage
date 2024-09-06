using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class active : MonoBehaviour
{
    public GameObject lyrics;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void act()
    {
        lyrics.SetActive(true);
    }
    public void notact()
    {
        lyrics.SetActive(false);
    }
}
