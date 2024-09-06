using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmController : MonoBehaviour
{
    public GameObject Warm;

    private Vector3 initialLocalScale;

    void Start()
    {
        initialLocalScale = Warm.transform.localScale;
    }

    public void AppearanceBtnClick()
    {
        Warm.transform.localScale = initialLocalScale;
    }

    public void CharacterClick()
    {
        Warm.transform.localScale = new Vector3(30, 30, 30);

    }
}
