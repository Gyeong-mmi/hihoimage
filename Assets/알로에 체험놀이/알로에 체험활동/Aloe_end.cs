using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aloe_end : MonoBehaviour
{
    public GameObject UI;
    public Text BNU;
    public Text SNU;
    public GameObject end;
    public AudioSource ending;

    public GameObject Aloe;
    public GameObject accct;
    public GameObject aloe_active;

    private bool isTrue = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(float.Parse(BNU.text.ToString()) >= 3f && float.Parse(SNU.text.ToString()) >= 3f && !isTrue)
        {
            isTrue = true;
            aloe_active.SetActive(false);
            accct.SetActive(false);
            Aloe.SetActive(false);
            UI.SetActive(false);

            Invoke("acaalo", 3f);
            Invoke("enaalo", 12.9f);
        }
    }

    private void acaalo()
    {
        end.SetActive(true);
        ending.Play();
    }

    private void enaalo()
    {
        end.SetActive(false);
    }
}
