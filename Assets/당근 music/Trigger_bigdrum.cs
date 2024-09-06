using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_bigdrum : MonoBehaviour
{
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;

    private int big_count1 = 0;
    private int big_count2 = 0;
    private int big_count3 = 0;

    public GameObject good;
    public GameObject bad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Drum"))
        {
            if (text1.activeSelf == true)
            {
                big_count1++;
                CheckAndUpdateStatus(big_count1, 8);
            }
            else if (text2.activeSelf == true)
            {
                big_count2++;
                CheckAndUpdateStatus(big_count2, 6);
            }
            else if (text3.activeSelf == true)
            {
                big_count3++;
                CheckAndUpdateStatus(big_count3, 6);
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
