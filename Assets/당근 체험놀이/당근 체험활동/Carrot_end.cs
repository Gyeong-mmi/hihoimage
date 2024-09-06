using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot_end : MonoBehaviour
{
    public GameObject UI;
    public GameObject end;
    public AudioSource ending;

    public GameObject Horse;

    private bool isTrue = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!UI.transform.GetChild(1).gameObject.activeSelf && !UI.transform.GetChild(2).gameObject.activeSelf && !UI.transform.GetChild(3).gameObject.activeSelf && !UI.transform.GetChild(4).gameObject.activeSelf && !UI.transform.GetChild(5).gameObject.activeSelf && !UI.transform.GetChild(6).gameObject.activeSelf && !isTrue)
        {
            isTrue = true;
            Horse.SetActive(false);
            UI.SetActive(false);

            Invoke("accca", 4f);
            Invoke("encca", 11.9f);
        }
    }

    private void accca()
    {
        end.SetActive(true);
        ending.Play();
    }

    private void encca()
    {
        end.SetActive(false);
    }
}
