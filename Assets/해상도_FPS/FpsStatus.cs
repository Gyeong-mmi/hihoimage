using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsStatus : MonoBehaviour
{
    private float deltaTime = 0f;

    [SerializeField] private int size = 38;
    [SerializeField] private Color color = Color.black;
    [SerializeField] private Vector3 position = new Vector3(-899.9504f, -425f, 0f);
    [SerializeField] private Font font; // 추가된 변수

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(position.x, position.y, Screen.width, Screen.height);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = size;
        style.normal.textColor = color;
        style.font = font; // 폰트 지정

        float ms = deltaTime * 1000f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);

        GUI.Label(rect, text, style);
    }
}
