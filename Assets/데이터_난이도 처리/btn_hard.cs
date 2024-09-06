using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_hard : MonoBehaviour
{
    public Image low;
    public Image mid;
    public Image hard;

    public bool isHard = false;


    public void OnBtnClick()
    {
        //¿¬°áµÊ
        Color color1;
        ColorUtility.TryParseHtmlString("#2AD994", out color1);

        //¿¬°á¾ÈµÊ
        Color color2;
        ColorUtility.TryParseHtmlString("#C2C7DE", out color2);

        low.color = color2;
        mid.color = color2;
        hard.color = color1;

        low.gameObject.GetComponent<btn_low>().isLow = false;
        mid.gameObject.GetComponent<btn_mid>().isMid = false;
        isHard = true;
    }
}
