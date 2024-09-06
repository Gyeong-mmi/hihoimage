using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_mid : MonoBehaviour
{
    public Image low;
    public Image mid;
    public Image hard;

    public bool isMid = false;


    public void OnBtnClick()
    {
        //¿¬°áµÊ
        Color color1;
        ColorUtility.TryParseHtmlString("#2AD994", out color1);

        //¿¬°á¾ÈµÊ
        Color color2;
        ColorUtility.TryParseHtmlString("#C2C7DE", out color2);

        low.color = color2;
        mid.color = color1;
        hard.color = color2;

        low.gameObject.GetComponent<btn_low>().isLow = false;
        isMid = true;
        hard.gameObject.GetComponent<btn_hard>().isHard = false;
    }
}
