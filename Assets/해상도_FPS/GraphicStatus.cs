using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicStatus : MonoBehaviour
{
    public Text graphic;

    private int width;
    private int height;

    // Start is called before the first frame update
    void Start()
    {
        width = Screen.width;
        height = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        width = Screen.width;
        height = Screen.height;

        graphic.text = "ÇØ»óµµ: " + width.ToString() + "X" + height.ToString();
    }
}
