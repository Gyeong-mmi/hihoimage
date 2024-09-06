using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;


public class Movement_insect : MonoBehaviour
{
    //BT ��ſ�
    private BT_Comm bluetoothHelper;


    public Transform[] pathNodes;   //��� ���
    private int currentNodeIndex = 0;

    private Vector3 initPos;
    private Quaternion initRotation;
    private Vector3 targetPos;

    public float moveSpeed = 0.005f;
    public float rotationSpeed = 0.1f;
    public float distanceToMoveStraight = 0.05f;
    public float returnToInitPosThreshold = 0.1f;

    private bool isMoving = false;
    private float totalPathDistance;
    private float currentDistance;
    private float progress;
    private float period = 0.2f;  //���� ����
    private float timer = 0.0f;      //�ð��� �����ϴ� ����


    void Start()
    {
        bluetoothHelper = BT_Comm.Instance;

        initPos = this.transform.localPosition;
        initRotation = transform.localRotation;
        targetPos = initPos + Vector3.back * distanceToMoveStraight;

        // ù ��° ���� ������ ��� ������ �Ÿ��� ���
        totalPathDistance = Vector3.Distance(pathNodes[0].position, pathNodes[pathNodes.Length - 1].position);
        Invoke("OnMove", 2.0f);  //���� 2�� �ĺ��� �����̵��� ����
    }
    public void OnMove()
    {
        isMoving = true;
    }
    void Calculate_position()
    {
        // ���� ��ġ�� ù ��° ��� ������ �Ÿ��� ���
        currentDistance = Vector3.Distance(transform.position, pathNodes[0].position);

        // ���� ��ġ�� ��ü ��ο� ���� ������ ǥ��
        progress = currentDistance / totalPathDistance;
    }
    void FixedUpdate()
    {
        if (isMoving)
        {
            MoveAlongPath();
            Calculate_position();

            //��ġ ����
            int pos = (int)(progress * 100);
            pos %= 100;
            timer += Time.deltaTime;    //�ð��� �������Ѽ�,
            if (timer <= period / 3)  //period�� 1/3 ���� �޽�
            {
                Send_pos(0);
            }
            else if (timer >= period) //period �� �����ϸ�
            {
                timer = 0.0f;
            }
            else
            {      // 2/3 �ð� ������ ���� ����
                Send_pos(pos);

            }
        }
    }
    void MoveAlongPath()    //��带 ���� �����̰� �ϴ� �Լ�
    {
        Transform targetNode = pathNodes[currentNodeIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetNode.position, moveSpeed * Time.deltaTime);

        // ������ ��忡 �����ߴ��� Ȯ��
        bool isLastNode = currentNodeIndex == pathNodes.Length - 1;

        // ������ ��尡 �ƴϸ�, ���� ��带 �ٶ󺸵��� ȸ��
        if (!isLastNode)
        {
            Vector3 targetDirection = targetNode.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        /*else
        {
            currentNodeIndex = 0;
            this.transform.position = initPos;
        }*/

        // ���� ��ġ�� ��ǥ ��忡 �������� ���, ���� ���� �ε��� ������Ʈ
        if (transform.position == targetNode.position && !isLastNode)
        {
            currentNodeIndex = (currentNodeIndex + 1) % pathNodes.Length;
        }

        /*if(isLastNode)
        {
            currentNodeIndex = 0;
        }*/
    }

    public void Send_pos(int pos)  //��ġ ���� (0~100)
    {

        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('1');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);
        byte[] b = BitConverter.GetBytes(pos);
        byte[] c = BitConverter.GetBytes(0x00);

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, b[0], c[0], 0xFE };

        bluetoothHelper.SendData(bytestosend);
    }
}
