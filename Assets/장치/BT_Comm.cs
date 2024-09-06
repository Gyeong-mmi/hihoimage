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
                // 인스턴스가 없다면 새로 생성
                GameObject obj = new GameObject("BT_Comm");
                instance = obj.AddComponent<BT_Comm>();
            }
            return instance;
        }
    }
    public BluetoothHelper bluetoothHelper;
    private string deviceName;
    public string received_message;

    /*CDS240109 이 부분 추가*/
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
    /*CDS240109 이 부분까지 추가*/

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeOnStart()
    {
        // 게임이 시작할 때 BT_Comm 인스턴스를 생성합니다.
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

        // 다른 초기화 로직
    }

    // Start is called before the first frame update
    void Start()
    {

        //Bluetooth 관련 permssion 획득 요청 (필수)
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
        deviceName = "HapticPad";   //연결할 장치의 이름 2번
        //deviceName = "PAD_V2";   //연결할 장치의 이름 1번
        //deviceName = "PAD_V3";   //연결할 장치의 이름
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
    /*CDS240109 이 함수 추가*/
    public void ParsingData()
    {
        string r_data = received_message;
        string modifiedString = r_data.Replace("$", "").Replace("#", ""); // "$,#"를 빈 문자열로 대체

        //// 데이터를 '*'와 ','를 기준으로 분할
        string[] parts = modifiedString.Split('*')[0].Split(',');

        //Stick1 쿼터니언 각도 추출
        float z1 = float.Parse(parts[0]);
        float y1 = float.Parse(parts[1]);
        float x1 = float.Parse(parts[2]);
        float w1 = float.Parse(parts[3]);

        //Stick2 가속도 값 추출
        float gx1 = float.Parse(parts[4]);
        float gy1 = float.Parse(parts[5]);
        float gz1 = float.Parse(parts[6]);



        if (((z1 < 1f) && (z1 > -1f) && (z1 != 0f)) &&  //x,y,z,w 정상 범위인 경우만 업데이트
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
        //Stick2 쿼터니언 각도 추출
        float z2 = float.Parse(parts[7]);
        float y2 = float.Parse(parts[8]);
        float x2 = float.Parse(parts[9]);
        float w2 = float.Parse(parts[10]);

        ////Stick2 가속도 값 추출
        float gx2 = float.Parse(parts[11]);
        float gy2 = float.Parse(parts[12]);
        float gz2 = float.Parse(parts[13]);

        if (((z2 < 1f) && (z2 > -1f) && (z2 != 0f)) && //x,y,z,w 정상 범위인 경우만 업데이트
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
            ParsingData();         /*CDS 이부분 수정*/
            Debug.Log("DEBUG: " + received_message);
        }
        //if (received_message.Length > 2) //일정 길이가 넘은 데이터만 출력
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