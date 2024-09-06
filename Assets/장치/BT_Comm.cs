using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;
public class BT_Comm : MonoBehaviour
{
    public static event Action OnConnectedEvent;
    public static event Action OnConnectionFailedEvent;
    private static BT_Comm instance;
    public static BT_Comm Instance
    {
        get
        {
            if (instance == null)
            {
                // �ν��Ͻ��� ���ٸ� ���� ����
                GameObject obj = new GameObject("BT_Comm");
                instance = obj.AddComponent<BT_Comm>();
            }
            return instance;
        }
    }
    public BluetoothHelper bluetoothHelper;
    private string deviceName;
    public string received_message;

    /*CDS240109 �� �κ� �߰�*/
    public Quaternion quat_Stick1;
    public Vector3 acc_Stick1;


    private float cx1 = 0f;
    private float cy1 = 0f;
    private float cz1 = 0f;
    private float cw1 = 0f;

    public Quaternion quat_Stick2;
    public Vector3 acc_Stick2;

    private float cx2 = 0f;
    private float cy2 = 0f;
    private float cz2 = 0f;
    private float cw2 = 0f;
    /*CDS240109 �� �κб��� �߰�*/

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeOnStart()
    {
        // ������ ������ �� BT_Comm �ν��Ͻ��� �����մϴ�.
        var instance = BT_Comm.Instance;
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // �ٸ� �ʱ�ȭ ����
    }

    // Start is called before the first frame update
    void Start()
    {

        //Bluetooth ���� permssion ȹ�� ��û (�ʼ�)
#if UNITY_2020_2_OR_NEWERrh
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation)
          || !Permission.HasUserAuthorizedPermission(Permission.FineLocation)
          || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_SCAN")
          || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_ADVERTISE")
          || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT"))
            Permission.RequestUserPermissions(new string[] {
                Permission.CoarseLocation,
                Permission.FineLocation,
                "android.permission.BLUETOOTH_SCAN",
                "android.permission.BLUETOOTH_ADVERTISE",
                "android.permission.BLUETOOTH_CONNECT"
              });
#endif
#endif
        received_message = "";
        deviceName = "HapticPad";   //������ ��ġ�� �̸� 2��
        //deviceName = "PAD_V2";   //������ ��ġ�� �̸� 1��
        //deviceName = "PAD_V3";   //������ ��ġ�� �̸�
        try
        {
            bluetoothHelper = BluetoothHelper.GetInstance(deviceName);
            bluetoothHelper.OnConnected += OnConnected;
            bluetoothHelper.setTerminatorBasedStream("\n");
            bluetoothHelper.OnDataReceived += OnMessageReceived; //read the data
            bluetoothHelper.OnConnectionFailed += OnConnFailed;

            LinkedList<BluetoothDevice> ds = bluetoothHelper.getPairedDevicesList();

            foreach (BluetoothDevice d in ds)
            {
                Debug.Log("BT_DEBUG: " + $"{d.DeviceName} {d.DeviceAddress}");
            }
        }
        catch (Exception ex)
        {
            //btnImage.GetComponent<Renderer>().material.color = Color.yellow;
            Debug.Log(ex.Message);
            //text.text = ex.Message;
        }
    }
    /*CDS240109 �� �Լ� �߰�*/
    public void ParsingData()
    {
        string r_data = received_message;
        string modifiedString = r_data.Replace("$", "").Replace("#", ""); // "$,#"�� �� ���ڿ��� ��ü

        //// �����͸� '*'�� ','�� �������� ����
        string[] parts = modifiedString.Split('*')[0].Split(',');

        //Stick1 ���ʹϾ� ���� ����
        float z1 = float.Parse(parts[0]);
        float y1 = float.Parse(parts[1]);
        float x1 = float.Parse(parts[2]);
        float w1 = float.Parse(parts[3]);

        //Stick2 ���ӵ� �� ����
        float gx1 = float.Parse(parts[4]);
        float gy1 = float.Parse(parts[5]);
        float gz1 = float.Parse(parts[6]);



        if (((z1 < 1f) && (z1 > -1f) && (z1 != 0f)) &&  //x,y,z,w ���� ������ ��츸 ������Ʈ
         ((x1 < 1f) && (x1 > -1f) && (x1 != 0f)) &&
         ((y1 < 1f) && (y1 > -1f) && (y1 != 0f)) &&
         ((w1 < 1f) && (w1 > -1f) && (w1 != 0f)))
        {

            cz1 = z1;
            cy1 = y1;
            cx1 = x1;
            cw1 = w1;
        }

        //quat_Stick1 = new Quaternion(-cx1, cz1, cy1, cw1);
        quat_Stick1 = new Quaternion(-cx1, cz1, cy1, cw1);
        acc_Stick1 = new Vector3(gx1, gy1, gz1);


        // Debug.Log("DEBUG2: " + quat_Stick1 + tmp);
        //Stick2 ���ʹϾ� ���� ����
        float z2 = float.Parse(parts[7]);
        float y2 = float.Parse(parts[8]);
        float x2 = float.Parse(parts[9]);
        float w2 = float.Parse(parts[10]);

        ////Stick2 ���ӵ� �� ����
        float gx2 = float.Parse(parts[11]);
        float gy2 = float.Parse(parts[12]);
        float gz2 = float.Parse(parts[13]);

        if (((z2 < 1f) && (z2 > -1f) && (z2 != 0f)) && //x,y,z,w ���� ������ ��츸 ������Ʈ
       ((x2 < 1f) && (x2 > -1f) && (x2 != 0f)) &&
       ((y2 < 1f) && (y2 > -1f) && (y2 != 0f)) &&
       ((w2 < 1f) && (w2 > -1f) && (w2 != 0f)))
        {

            cz2 = z2;
            cy2 = y2;
            cx2 = x2;
            cw2 = w2;
        }
        quat_Stick2 = new Quaternion(-cx2, cz2, cy2, cw2);
        acc_Stick2 = new Vector3(gx2, gy2, gz2);

    }


    void OnConnFailed(BluetoothHelper helper)
    {
        OnConnectionFailedEvent?.Invoke();
    }
    public void SendData(byte[] data)
    {
        bluetoothHelper.SendData(data);
    }
    public bool isConnected()
    {
        return bluetoothHelper.isConnected();
    }
    public bool isDevicePaired()
    {
        return bluetoothHelper.isDevicePaired();
    }
    void OnConnected(BluetoothHelper helper)
    {
        try
        {
            helper.StartListening();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        OnConnectedEvent?.Invoke();

    }
    public void TryConnect()
    {
        if (!bluetoothHelper.isConnected())
        {
            if (bluetoothHelper.isDevicePaired())
                bluetoothHelper.Connect(); // tries to connect                                      
        }
        else
        {
            bluetoothHelper.Disconnect();
        }
    }
    public void Disconnect()
    {
        bluetoothHelper.Disconnect();
    }
    //Asynchronous method to receive messages
    void OnMessageReceived(BluetoothHelper helper)
    {
        received_message = helper.Read();
        //StartCoroutine(blinkSphere());
        if (received_message.Length > 2)
        {
            ParsingData();         /*CDS �̺κ� ����*/
            Debug.Log("DEBUG: " + received_message);
        }
        //if (received_message.Length > 2) //���� ���̰� ���� �����͸� ���
        //text.text = received_message;
        //text.text = "recived";
        // Debug.Log(received_message);
    }
    void OnDestroy()
    {
        if (bluetoothHelper != null)
            bluetoothHelper.Disconnect();
    }

}