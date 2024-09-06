using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_smalldrum : MonoBehaviour
{
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;

    private int small_count1 = 0;
    private int small_count2 = 0;
    private int small_count3 = 0;

    public GameObject good;
    public GameObject bad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Drum"))
        {
            if (text1.activeSelf)
            {
                small_count1++;
                CheckAndUpdateStatus(small_count1, 8);
            }
            else if (text2.activeSelf)
            {
                small_count2++;
                CheckAndUpdateStatus(small_count2, 8);
            }
            else if (text3.activeSelf)
            {
                small_count3++;
                CheckAndUpdateStatus(small_count3, 8);
            }
        }
    }

    private void CheckAndUpdateStatus(int count, int targetCount)
    {
        if (count == targetCount)
        {
            bad.SetActive(false);
            good.SetActive(true);
        }
        else
        {
            good.SetActive(false);
            bad.SetActive(true);
        }
    }
}
