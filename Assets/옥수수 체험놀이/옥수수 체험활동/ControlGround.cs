using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGround : MonoBehaviour
{
    public static bool ContectSeed1 = false;
    public static bool ContectSeed2 = false;
    public static bool ContectSeed3 = false;


    private void Start()
    {


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground1"))
        {
            ContectSeed1 = true;
        }

        if (other.gameObject.CompareTag("Ground2"))
        {
            ContectSeed2 = true;
        }
        if (other.gameObject.CompareTag("Ground3"))
        {
            ContectSeed3 = true;
        }
    }
}
