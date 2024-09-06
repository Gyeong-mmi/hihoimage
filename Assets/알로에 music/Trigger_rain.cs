using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;

public class Trigger_rain : MonoBehaviour
{
    //BT ��ſ�
    private BT_Comm bluetoothHelper;

    public Text text;
    private int prevAngle = 0;  //���� ����
    private int currAngle = 0;  //���� ����
    private float timer = 0.0f;      //�ð��� �����ϴ� ����
    private float interval = 1.2f;  //�������� �ʴ��� 2�ʵ����� ������ �������Ѿ���.
    private bool isTilting = false;
    private bool isShaked = false;              /*  CDS ����*/

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
        {    //������ 10�� ���Ϸ� �ٲ������,

            if (Mathf.Abs(acc - 0.98f) >= 0.25f && !isTilting && !isShaked) //���� ���ӵ� ��ȭ�� �����̻��̸�,
            {
                Tactile_shake();    //shaking ���� ȣ��
                isShaked = true;
                if (isLogging)
                {    //����� ���۵Ǿ��ٸ�,
                    curTime = aloeAudio.time;    //���� �ð� ���                    
                    state = -1;
                    //Data ���� �ڵ� �߰�
                    //curTime, state, acc ���
                    Manager_ResultInDetail.instance.Add_RIDdata(idx + 1, curTime, acc, state);  //�ð�, ����, ����.���� Ȯ��
                }
                Invoke("DelayShake", 0.1f); //0.1�� �Ŀ�  �ٽ� ������ �߻��� �� �ֵ���
            }

            timer += Time.deltaTime;    //�ð��� �������Ѽ�,
            if (timer >= interval)  //interval �̻� ������(�𷡰� �� �������) �������� �ʵ��� ���ƾ���.
            {
                Tactile_off();
                isTilting = false;
                // Ÿ�̸� �ʱ�ȭ
                timer = 0.0f;
            }
        }
        else //15�� �̻� ������.
        {
            if (isLogging)
            {    //����� ���۵Ǿ��ٸ�,
                curTime = aloeAudio.time;    //15�� �̻� ������ �ð� ���.
                if (prevAngle > currAngle)   //ȸ�� ���� ���
                {
                    state = 1;
                }
                else
                {
                    state = 0;
                }
                //Data ���� �ڵ� �߰�
                //curTime, state ���
                Manager_ResultInDetail.instance.Add_RIDdata(idx + 1, curTime, acc, state);  //�ð�, ����, ����.���� Ȯ��
            }

            timer = 0.0f;
            prevAngle = currAngle;
            interval = 0.4f + (0.8f * ((90.0f - currAngle) / 90.0f));
            isTilting = true;         //isTilting�� True�� ��ȯ (���� ����)
        }
        if (isTilting)
        {
            Tactile_tilt(currAngle);    //���� �߻� 
        }

    }

    public void Tactile_shake()  //����
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
        /*CDS240109 �̺κ� �ּ�*/

        //string r_data = bluetoothHelper.received_message;
        //string modifiedString = r_data.Replace("$", "").Replace("#", ""); // "$,#"�� �� ���ڿ��� ��ü

        ////// �����͸� '*'�� ','�� �������� ����
        //string[] parts = modifiedString.Split('*')[0].Split(',');

        ////���ʹϾ� ���� ����
        //float x1 = float.Parse(parts[0]);
        //float y1 = float.Parse(parts[1]);
        //float z1 = float.Parse(parts[2]);
        //float w1 = float.Parse(parts[3]);

        ////���ӵ� �� ����
        //float gx1 = float.Parse(parts[4]);
        //float gy1 = float.Parse(parts[5]);
        //float gz1 = float.Parse(parts[6]);
        //Vector3 accVec = new Vector3(gx1, gy1, gz1);

        /*CDS240109 �̺κ� �ּ�*/

        /*CDS240109 �̺κ� ����*/
        float localRotation = bluetoothHelper.quat_Stick1.eulerAngles.x;
        /*CDS240109 �̺κ� ����*/

        /*
         *         0(360)
                    |
             90  --  -- 270
                    |
                   180
         */

        //0 (��) -> 90(��)���� ����    
        int angle = (int)localRotation;

        if ((angle / 90) % 2 == 0)
        {    //���� ����.
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
        //�� Ʈ��ŷ�� �ȵ� �� �� �����Ƿ� (Ʈ��ŷ�� �ȵǴ� ��� ������Ʈ�� disable �Ǵ� �� ����) �� ������ �ش� ������Ʈ�� ã�ƾ���.
        Transform rainstickTransform = GameObject.FindWithTag("Rain").transform;
        float localRotation = rainstickTransform.localEulerAngles.x;

        //0 (��) -> 90(��)���� ����
        if (localRotation >= 270) // 
        {
            localRotation -= 360;
        }
        if (localRotation < 0) localRotation *= -1; //��ȣ �ݴ�
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

    public void Tactile_tilt(int rotation)  //����̱�
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
