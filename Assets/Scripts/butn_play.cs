using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class butn_play : MonoBehaviour
{
    public Text subtitleText; // 자막을 표시할 UI 텍스트(Text) 변수

    public void ToggleSubtitle()
    {
        subtitleText.gameObject.SetActive(true);
    }
}
