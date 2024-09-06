using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_false : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stick1") || other.gameObject.CompareTag("Stick2"))
        {
            Trigger_ddrum obj = GameObject.FindGameObjectWithTag("Music2").GetComponent<Trigger_ddrum>();
            obj.istrigger = false;
            Debug.Log("KKK_TRIGGER_false_area");
        }
    }
}
