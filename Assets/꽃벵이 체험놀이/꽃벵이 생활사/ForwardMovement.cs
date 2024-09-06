using ArduinoBluetoothAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    // BT 통신용
    private BT_Comm bluetoothHelper;

    public Transform[] pathNodes;   // 노드 목록
    private float moveSpeed = 0.1f;  // 이동 속도
    public float rotationSpeed = 2.0f;  // 회전 속도

    private int currentNodeIndex = 0;  // 현재 노드 인덱스

    private bool isMoving = false;
    private bool isReversing = false;  // 역방향으로 이동 중인지 여부

    void Start()
    {
        bluetoothHelper = BT_Comm.Instance;

        Invoke("OnMove", 1.0f);  // 시작 2초 후부터 움직이도록 변경
    }

    public void OnMove()
    {
        isMoving = true;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            MoveAlongPath();
        }
    }

    void MoveAlongPath()
    {
        if (currentNodeIndex < pathNodes.Length && currentNodeIndex >= 0)
        {
            // 노드 방향으로 회전
            Vector3 direction = (pathNodes[currentNodeIndex].position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * rotationSpeed);

            // 회전 각도가 일정 범위 내에 있을 때만 앞으로 이동
            if (Quaternion.Angle(transform.rotation, lookRotation) < 10.0f) // 10도 이내면 이동
            {
                // 앞으로 이동
                transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime);
            }

            // 다음 노드에 도달했는지 확인
            float distanceToNextNode = Vector3.Distance(transform.position, pathNodes[currentNodeIndex].position);
            if (distanceToNextNode < 0.2f)  // 이 값은 필요에 따라 조정 가능 (기존 0.1f에서 0.2f로 변경)
            {
                // 다음 노드로 이동
                if (!isReversing)
                {
                    currentNodeIndex++;
                    if (currentNodeIndex >= pathNodes.Length)
                    {
                        currentNodeIndex = pathNodes.Length - 1;
                        isReversing = true;
                    }
                }
                else
                {
                    currentNodeIndex--;
                    if (currentNodeIndex < 0)
                    {
                        currentNodeIndex = 0;
                        isReversing = false;
                    }
                }
            }
        }
    }
}
