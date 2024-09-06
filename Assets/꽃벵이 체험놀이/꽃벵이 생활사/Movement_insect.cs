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
    //BT 통신용
    private BT_Comm bluetoothHelper;


    public Transform[] pathNodes;   //노드 목록
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
    private float period = 0.2f;  //진동 간격
    private float timer = 0.0f;      //시간을 측정하는 변수


    void Start()
    {
        bluetoothHelper = BT_Comm.Instance;

        initPos = this.transform.localPosition;
        initRotation = transform.localRotation;
        targetPos = initPos + Vector3.back * distanceToMoveStraight;

        // 첫 번째 노드와 마지막 노드 사이의 거리를 계산
        totalPathDistance = Vector3.Distance(pathNodes[0].position, pathNodes[pathNodes.Length - 1].position);
        Invoke("OnMove", 2.0f);  //시작 2초 후부터 움직이도록 변경
    }
    public void OnMove()
    {
        isMoving = true;
    }
    void Calculate_position()
    {
        // 현재 위치와 첫 번째 노드 사이의 거리를 계산
        currentDistance = Vector3.Distance(transform.position, pathNodes[0].position);

        // 현재 위치를 전체 경로에 대한 비율로 표현
        progress = currentDistance / totalPathDistance;
    }
    void FixedUpdate()
    {
        if (isMoving)
        {
            MoveAlongPath();
            Calculate_position();

            //위치 전송
            int pos = (int)(progress * 100);
            pos %= 100;
            timer += Time.deltaTime;    //시간을 누적시켜서,
            if (timer <= period / 3)  //period의 1/3 동안 휴식
            {
                Send_pos(0);
            }
            else if (timer >= period) //period 에 도달하면
            {
                timer = 0.0f;
            }
            else
            {      // 2/3 시간 동안은 진동 생성
                Send_pos(pos);

            }
        }
    }
    void MoveAlongPath()    //노드를 따라 움직이게 하는 함수
    {
        Transform targetNode = pathNodes[currentNodeIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetNode.position, moveSpeed * Time.deltaTime);

        // 마지막 노드에 도달했는지 확인
        bool isLastNode = currentNodeIndex == pathNodes.Length - 1;

        // 마지막 노드가 아니면, 다음 노드를 바라보도록 회전
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

        // 현재 위치가 목표 노드에 도달했을 경우, 다음 노드로 인덱스 업데이트
        if (transform.position == targetNode.position && !isLastNode)
        {
            currentNodeIndex = (currentNodeIndex + 1) % pathNodes.Length;
        }

        /*if(isLastNode)
        {
            currentNodeIndex = 0;
        }*/
    }

    public void Send_pos(int pos)  //위치 전송 (0~100)
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
