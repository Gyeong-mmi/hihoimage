using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class butn_play : MonoBehaviour
{
    public Text subtitleText; // �ڸ��� ǥ���� UI �ؽ�Ʈ(Text) ����

    public void ToggleSubtitle()
    {
        subtitleText.gameObject.SetActive(true);
    }
}
