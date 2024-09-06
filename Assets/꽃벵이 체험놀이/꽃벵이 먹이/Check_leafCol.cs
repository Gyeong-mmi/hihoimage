using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_leafCol : MonoBehaviour
{
    public static bool CheckLeaf1 = false;
    public static bool CheckLeaf2 = false;
    public static bool CheckLeaf3 = false;


    private void Start()
    {


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("leaf1"))
        {
            CheckLeaf1 = true;
        }

        if (other.gameObject.CompareTag("leaf2"))
        {
            CheckLeaf2 = true;
        }
        if (other.gameObject.CompareTag("leaf3"))
        {
            CheckLeaf3 = true;
        }
    }
}
