using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class kiwro_collider : MonoBehaviour
{
    public AudioSource cornAudio;
    [Header("[Collider]")]
    public GameObject collider1;
    public GameObject collider2;
    public GameObject collider3;
    public GameObject collider4;
    public GameObject collider5;
    public GameObject collider6;
    public GameObject collider7;
    public GameObject collider8;
    public GameObject collider9;
    public GameObject collider10;
    public GameObject collider11;
    public GameObject collider12;
    private void Update()
    {
        float curTime = cornAudio.time;
        if (curTime >= 8.8f && curTime < 9.3f)
        {
            collider_disable();
        }
        else if (curTime >= 9.3f && curTime < 9.8f)
        {
            collider_disable();
        }
        else if (curTime >= 9.8f && curTime < 10.3f)
        {
            collider_disable();
        }
        else if (curTime >= 10.3f && curTime < 10.8f)
        {
            collider_disable();
        }
        else if (curTime >= 10.8f && curTime < 11.3f)
        {
            collider_disable();
        }
        else if (curTime >= 11.3f && curTime < 11.8f)
        {
            collider_disable();
        }
        else if (curTime >= 11.8f && curTime < 12.3f)
        {
            collider_disable();
        }
        else if (curTime >= 12.3f && curTime < 12.8f)
        {
            collider_disable();
        }
        else if (curTime >= 12.8f && curTime < 13.5f)
        {
            collider_disable();
        }
        else if (curTime >= 13.5f && curTime < 14.15f)
        {
            collider_disable();
        }
        else if (curTime >= 14.15f && curTime < 14.85f)
        {
            collider_disable();
        }
        else if (curTime >= 14.85f && curTime < 16.3f)
        {
            collider_disable();
        }
        else if (curTime >= 17.3f && curTime < 17.8f)
        {
            collider_disable();
        }
        else if (curTime >= 17.8f && curTime < 18.3f)
        {
            collider_disable();
        }
        else if (curTime >= 18.3f && curTime < 18.8f)
        {
            collider_disable();
        }
        else if (curTime >= 18.8f && curTime < 19.3f)
        {
            collider_disable();
        }
        else if (curTime >= 43.5f && curTime < 44f)
        {
            collider_disable();
        }
        else if (curTime >= 44f && curTime < 44.5f)
        {
            collider_disable();
        }
        else if (curTime >= 44.5f && curTime < 45f)
        {
            collider_disable();
        }
        else if (curTime >= 45f && curTime < 45.5f)
        {
            collider_disable();
        }
        else if (curTime >= 45.5f && curTime < 46.3f)
        {
            collider_disable();
        }
        else if (curTime >= 46.3f && curTime < 47.5f)
        {
            collider_disable();
        }
        else if (curTime >= 47.5f && curTime < 48.1f)
        {
            collider_disable();
        }
        else if (curTime >= 48.1f && curTime < 49.5f)
        {
            collider_disable();
        }
        else if (curTime >= 51.5f && curTime < 52.2f)
        {
            collider_disable();
        }
        else if (curTime >= 52.2f && curTime < 53.7f)
        {
            collider_disable();
        }
        //Ä¡±â
        if (curTime >= 16.3f && curTime < 16.6f)
        {
            collider_enable();
        }
        else if (curTime >= 16.6f && curTime < 16.9f)
        {
            collider_enable();
        }
        else if (curTime >= 16.9f && curTime < 17.3f)
        {
            collider_enable();
        }
        else if (curTime >= 24.8f && curTime < 25.1f)
        {
            collider_enable();
        }
        else if (curTime >= 25.1f && curTime < 25.4f)
        {
            collider_enable();
        }
        else if (curTime >= 25.4f && curTime < 25.7f)
        {
            collider_enable();
        }
        else if (curTime >= 28.3f && curTime < 28.85f)
        {
            collider_enable();
        }
        else if (curTime >= 28.85f && curTime < 29.4f)
        {
            collider_enable();
        }
        else if (curTime >= 29.4f && curTime < 29.95f)
        {
            collider_enable();
        }
        else if (curTime >= 29.95f && curTime < 30.5f)
        {
            collider_enable();
        }
        else if (curTime >= 33.9f && curTime < 34.9f)
        {
            collider_enable();
        }
        else if (curTime >= 38.1f && curTime < 39.1f)
        {
            collider_enable();
        }
        else if (curTime >= 42.3f && curTime < 42.7f)
        {
            collider_enable();
        }
        else if (curTime >= 42.7f && curTime < 43.1f)
        {
            collider_enable();
        }
        else if (curTime >= 43.1f && curTime < 43.5f)
        {
            collider_enable();
        }
        else if (curTime >= 50.8f && curTime < 51.5f)
        {
            collider_enable();
        }
        else if (curTime >= 55f && curTime < 55.7f)
        {
            collider_enable();
        }
    }
    public void collider_enable()
    {
        GameObject kiwro = GameObject.FindGameObjectWithTag("Kiwro");
        Collider kiwrocollider = kiwro.GetComponent<Collider>();
        kiwrocollider.enabled = true;
        collider1.GetComponent<Collider>().enabled = false;
        collider2.GetComponent<Collider>().enabled = false;
        collider3.GetComponent<Collider>().enabled = false;
        collider4.GetComponent<Collider>().enabled = false;
        collider5.GetComponent<Collider>().enabled = false;
        collider6.GetComponent<Collider>().enabled = false;
        collider7.GetComponent<Collider>().enabled = false;
        collider8.GetComponent<Collider>().enabled = false;
        collider9.GetComponent<Collider>().enabled = false;
        collider10.GetComponent<Collider>().enabled = false;
        collider11.GetComponent<Collider>().enabled = false;
        collider12.GetComponent<Collider>().enabled = false;
    }
    public void collider_disable()
    {
        GameObject kiwro = GameObject.FindGameObjectWithTag("Kiwro");
        Collider kiwrocollider = kiwro.GetComponent<Collider>();
        kiwrocollider.enabled = false;
        kiwro.GetComponent<Trigger_kkiwro>().isTrigger = false;
        collider1.GetComponent<Collider>().enabled = true;
        collider2.GetComponent<Collider>().enabled = true;
        collider3.GetComponent<Collider>().enabled = true;
        collider4.GetComponent<Collider>().enabled = true;
        collider5.GetComponent<Collider>().enabled = true;
        collider6.GetComponent<Collider>().enabled = true;
        collider7.GetComponent<Collider>().enabled = true;
        collider8.GetComponent<Collider>().enabled = true;
        collider9.GetComponent<Collider>().enabled = true;
        collider10.GetComponent<Collider>().enabled = true;
        collider11.GetComponent<Collider>().enabled = true;
        collider12.GetComponent<Collider>().enabled = true;
    }
}