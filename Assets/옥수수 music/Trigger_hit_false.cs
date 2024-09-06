using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_hit_false : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stick"))
        {
            Trigger_kkiwro obj = GameObject.FindGameObjectWithTag("Kiwro").GetComponent<Trigger_kkiwro>();
            obj.isTrigger = false;
        }
    }
}
