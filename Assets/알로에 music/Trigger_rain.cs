using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;

public class Trigger_rain : MonoBehaviour
{
    //BT 통신용
    private BT_Comm bluetoothHelper;

    public Text text;
    private int prevAngle = 0;  //이전 각도
    private int currAngle = 0;  //현재 각도
    private float timer = 0.0f;      //시간을 측정하는 변수
    private float interval = 1.2f;  //움직이지 않더라도 2초동안은 진동을 유지시켜야함.
    private bool isTilting = false;
    private bool isShaked = false;              /*  CDS 수정*/

    public AudioSource aloeAudio;

    private float acc = 0.0f;

    float[] activeTimes = new float[] {
         13.38f, 13.65f, 13.97f, 17f,
         17.61f, 18.21f, 25.38f, 26.03f,
         33.75f, 34.28f, 34.91f, 35.49f,
         36.09f, 36.73f, 37.35f, 37.95f,
         38.58f, 39.12f, 39.79f, 40.33f,
         41.33f, 41.54f, 42.08f, 42.68f,
         50.59f, 60.13f, 69.75f, 70.39f,
         71.01f, 74.63f, 75.16f, 75.76f
    };
    float[] inactiveTimes = new float[] {
         13.38f, 13.65f, 13.97f, 17f,
         17.61f, 18.21f, 25.38f, 26.03f,
         33.75f, 34.28f, 34.91f, 35.49f,
         36.09f, 36.73f, 37.35f, 37.95f,
         38.58f, 39.12f, 39.79f, 40.33f,
         41.33f, 41.54f, 42.08f, 42.68f,
         57.21f, 66.86f, 69.75f, 70.39f,
         71.01f, 74.63f, 75.16f, 75.76f
    };
    public int idx = 0;

    public void init()
    {
        idx = 0;
        isTilting = false;
        isShaked = false;
        timer = 0.0f;
        isLogging = false;
    }

    private bool isLogging = false;
    private float state = 0; //cw = 0, ccw = 1, shake = -1

    void Start()
    {
        bluetoothHelper = BT_Comm.Instance;
    }

    void DelayShake()
    {
        isShaked = false;
    }

    void Update()
    {
        float curTime = aloeAudio.time;

        if (curTime >= activeTimes[idx] - 1 && !isLogging)
        {
            isLogging = true;
        }
        else if (isLogging && curTime >= inactiveTimes[idx] + 1)
        {
            isLogging = false;
            idx++;
        }
        text.text = bluetoothHelper.received_message;
        currAngle = Calculate_angle();
        acc = Calculate_Accel();

        if (Mathf.Abs(currAngle - prevAngle) < 15)
        {    //각도가 10도 이하로 바뀌었으면,

            if (Mathf.Abs(acc - 0.98f) >= 0.25f && !isTilting && !isShaked) //진동 가속도 변화가 일정이상이면,
            {
                Tactile_shake();    //shaking 진동 호출
                isShaked = true;
                if (isLogging)
                {    //기록이 시작되었다면,
                    curTime = aloeAudio.time;    //흔들기 시간 기록                    
                    state = -1;
                    //Data 저장 코드 추가
                    //curTime, state, acc 기록
                    Manager_ResultInDetail.instance.Add_RIDdata(idx + 1, curTime, acc, state);  //시간, 강도, 방향.기울기 확인
                }
                Invoke("DelayShake", 0.1f); //0.1초 후에  다시 진동이 발생할 수 있도록
            }

            timer += Time.deltaTime;    //시간을 누적시켜서,
            if (timer >= interval)  //interval 이상 지나면(모래가 다 쏟아지면) 진동하지 않도록 막아야함.
            {
                Tactile_off();
                isTilting = false;
                // 타이머 초기화
                timer = 0.0f;
            }
        }
        else //15도 이상 움직임.
        {
            if (isLogging)
            {    //기록이 시작되었다면,
                curTime = aloeAudio.time;    //15도 이상 움직인 시각 기록.
                if (prevAngle > currAngle)   //회전 방향 기록
                {
                    state = 1;
                }
                else
                {
                    state = 0;
                }
                //Data 저장 코드 추가
                //curTime, state 기록
                Manager_ResultInDetail.instance.Add_RIDdata(idx + 1, curTime, acc, state);  //시간, 강도, 방향.기울기 확인
            }

            timer = 0.0f;
            prevAngle = currAngle;
            interval = 0.4f + (0.8f * ((90.0f - currAngle) / 90.0f));
            isTilting = true;         //isTilting을 True로 변환 (진동 시작)
        }
        if (isTilting)
        {
            Tactile_tilt(currAngle);    //진동 발생 
        }

    }

    public void Tactile_shake()  //흔들기
    {
        //Debug.Log("SHAKE: " + Time.time);
        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('9');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);


        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x52, 0x00, 0xFE };

        bluetoothHelper.SendData(bytestosend);
    }
    private float Calculate_Accel()
    {
        return bluetoothHelper.acc_Stick1.magnitude;
    }
    public int Calculate_angle()
    {
        /*CDS240109 이부분 주석*/

        //string r_data = bluetoothHelper.received_message;
        //string modifiedString = r_data.Replace("$", "").Replace("#", ""); // "$,#"를 빈 문자열로 대체

        ////// 데이터를 '*'와 ','를 기준으로 분할
        //string[] parts = modifiedString.Split('*')[0].Split(',');

        ////쿼터니언 각도 추출
        //float x1 = float.Parse(parts[0]);
        //float y1 = float.Parse(parts[1]);
        //float z1 = float.Parse(parts[2]);
        //float w1 = float.Parse(parts[3]);

        ////가속도 값 추출
        //float gx1 = float.Parse(parts[4]);
        //float gy1 = float.Parse(parts[5]);
        //float gz1 = float.Parse(parts[6]);
        //Vector3 accVec = new Vector3(gx1, gy1, gz1);

        /*CDS240109 이부분 주석*/

        /*CDS240109 이부분 수정*/
        float localRotation = bluetoothHelper.quat_Stick1.eulerAngles.x;
        /*CDS240109 이부분 수정*/

        /*
         *         0(360)
                    |
             90  --  -- 270
                    |
                   180
         */

        //0 (약) -> 90(강)으로 설정    
        int angle = (int)localRotation;

        if ((angle / 90) % 2 == 0)
        {    //방향 반전.
            angle %= 90;
            angle = 90 - angle;
        }
        else
        {
            angle %= 90;
        }
        //Debug.Log("ANGLE:" + localRotation);

        return angle;

    }

    public int Calculate_angle_CV()
    {
        //손 트래킹이 안될 수 도 있으므로 (트래킹이 안되는 경우 오브젝트가 disable 되는 것 같음) 매 프레임 해당 오브젝트를 찾아야함.
        Transform rainstickTransform = GameObject.FindWithTag("Rain").transform;
        float localRotation = rainstickTransform.localEulerAngles.x;

        //0 (약) -> 90(강)으로 설정
        if (localRotation >= 270) // 
        {
            localRotation -= 360;
        }
        if (localRotation < 0) localRotation *= -1; //부호 반대
                                                    //float tmp = ProcessReceivedData();

        // text.text = timer.ToString() + " " + currAngle.ToString() + " / " + stickRotation.ToString() + " / " + isTilting.ToString();
        return (int)localRotation;
    }

    public void Tactile_off()
    {
        byte temp_mode_stx = (byte)'<';
        byte temp_mode_etx = (byte)'>';


        byte[] bytestosend = { temp_mode_stx, (byte)'0', temp_mode_etx, 0xFF, 0x00, 0x00, 0xFE };

        string hexString = BitConverter.ToString(bytestosend).Replace("-", " ");


        //Debug.Log("SEND DATA1:" + hexString);

        bluetoothHelper.SendData(bytestosend);
    }

    public void Tactile_tilt(int rotation)  //기울이기
    {

        byte temp_mode_stx = (byte)'<';
        byte temp_mode_etx = (byte)'>';
        byte b = (byte)rotation;

        byte[] bytestosend = { temp_mode_stx, (byte)'0', temp_mode_etx, 0xFF, b, 0x00, 0xFE };


        string hexString = BitConverter.ToString(bytestosend).Replace("-", " ");
        //Debug.Log("SEND DATA2: " + hexString);

        bluetoothHelper.SendData(bytestosend);
    }

}
