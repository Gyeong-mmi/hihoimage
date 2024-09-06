using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolActive : MonoBehaviour
{
    public Text btnText;
    public Image btnImage;
    private bool isClick = false;
    public GameObject left_right;

    void Start()
    {
        btnText.text = "1�� Ȱ��ȭ";
    }

    public void OnBtnClick()
    {
        if (!isClick)
        {
            Color color;
            ColorUtility.TryParseHtmlString("#2AD994", out color);
            btnImage.color = color;
            btnText.text = "3�� Ȱ��ȭ";
            isClick = true;
            left_right.SetActive(true);
        }
        else
        {
            Color color;
            ColorUtility.TryParseHtmlString("#C2C7DE", out color);
            btnImage.color = color;
            btnText.text = "1�� Ȱ��ȭ";
            isClick = false;
            left_right.SetActive(false);
        }
    }
}
