using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;
public class DeviceManager : MonoBehaviour
{
    public Text btnText;
    public Text btnText2;
    public Image btnImage;
    public Image btnImage2;

    private BT_Comm bt_comm;


    // Start is called before the first frame update
    void Start()
    {
        bt_comm = BT_Comm.Instance;
        BT_Comm.OnConnectedEvent += OnConnected;
        BT_Comm.OnConnectionFailedEvent += OnConnFailed;

        btnText.text = "¿¬°á ¾È µÊ";
        btnText2.text = "¿¬°á ¾È µÊ";
    }

    // Update is called once per frame
    void Update()
    {
        //data send´Â ¾Æ·¡ÀÇ ÇÔ¼ö È°¿ë
        //bluetoothHelper.SendData(new Byte[] { 0, 0, 85, 0, 85 });
        // bluetoothHelper.SendData("This is a very long long long long text");
    }

    void OnConnected()
    {
        Color color;
        ColorUtility.TryParseHtmlString("#2AD994", out color);
        //btnImage.GetComponent<Renderer>().material.color = color;
        btnImage.color = color;
        btnText.text = "¿¬°áµÊ";
        btnImage2.color = color;
        btnText2.text = "¿¬°áµÊ";
    }
    void OnConnFailed()
    {
        Color color;
        ColorUtility.TryParseHtmlString("#C2C7DE", out color);
        btnImage.color = color;
        btnImage2.color = color;
        //btnImage.GetComponent<Renderer>().material.color = color;
        btnText.text = "¿¬°á ¾È µÊ";
        btnText2.text = "¿¬°á ¾È µÊ";
        Debug.Log("DEBUG: Connection Failed");
    }
    public void OnBtnClick()
    {

        Debug.Log("DEBUG:BTN Click");

        if (!bt_comm.isConnected())
        {
            Color color;
            ColorUtility.TryParseHtmlString("#2AD994", out color);
            Debug.Log("DEBUG:Try To Connection");
            if (bt_comm.isDevicePaired())
            {
                bt_comm.TryConnect();
                Debug.Log("DEBUG: Paired");
            }
                //bt_comm.TryConnect();
            else
            {
                Debug.Log("DEBUG: NOT Paired");
                btnImage.color = color;
                btnImage2.color = color;
            }
            //btnImage.GetComponent<Renderer>().material.color = color;
        }
        else
        {
            Color color;
            ColorUtility.TryParseHtmlString("#C2C7DE", out color);
            btnText.text = "¿¬°á ¾È µÊ";
            btnText2.text = "¿¬°á ¾È µÊ";
            bt_comm.Disconnect();
            btnImage.color = color;
            btnImage2.color = color;
            //btnImage.GetComponent<Renderer>().material.color = color;
        }
    }
}
