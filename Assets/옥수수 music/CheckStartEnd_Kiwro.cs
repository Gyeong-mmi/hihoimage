using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckStartEnd_Kiwro : MonoBehaviour
{
    public AudioSource cornAudio;
    public float triggertime;
    public bool isScratch = false;
    public bool isEntered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stick"))
        {
            triggertime = cornAudio.time;

            isEntered = true;
            if (this.gameObject.tag == "col1")
            {
                CheckStartEnd_Kiwro chk = GameObject.FindGameObjectWithTag("col2").GetComponent<CheckStartEnd_Kiwro>();
                if (chk.isEntered) isScratch = true;
            }
            else if (this.gameObject.tag == "col2")
            {
                CheckStartEnd_Kiwro chk = GameObject.FindGameObjectWithTag("col1").GetComponent<CheckStartEnd_Kiwro>();
                if (chk.isEntered) isScratch = true;
            }
        }
    }
}