using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fix_Setting : MonoBehaviour
{
    public GameObject Group_Level;
    // Start is called before the first frame update
    
    public void Active_GL()
    {
        Group_Level.SetActive(true);
    }
    public void InActive_GL()
    {
        Group_Level.SetActive(false);
    }
}
