using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_subset_a : MonoBehaviour
{
    public int hit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stick"))
        {
            Trigger_Kiwro obj = GameObject.FindGameObjectWithTag("Kiwro").GetComponent<Trigger_Kiwro>();
            obj.Tactile();
        }
    }
}
