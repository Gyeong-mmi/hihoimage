using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_low : MonoBehaviour
{
    public Image low;
    public Image mid;
    public Image hard;

    public bool isLow = false;

    public void OnBtnClick()
    {
        //¿¬°áµÊ
        Color color1;
        ColorUtility.TryParseHtmlString("#2AD994", out color1);

        //¿¬°á¾ÈµÊ
        Color color2;
        ColorUtility.TryParseHtmlString("#C2C7DE", out color2);

        low.color = color1;
        mid.color = color2;
        hard.color = color2;

        isLow = true;
        mid.gameObject.GetComponent<btn_mid>().isMid = false;
        hard.gameObject.GetComponent<btn_hard>().isHard = false;
    }
}
