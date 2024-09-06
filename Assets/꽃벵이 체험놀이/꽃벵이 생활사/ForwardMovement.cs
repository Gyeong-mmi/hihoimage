using ArduinoBluetoothAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    // BT ��ſ�
    private BT_Comm bluetoothHelper;

    public Transform[] pathNodes;   // ��� ���
    private float moveSpeed = 0.1f;  // �̵� �ӵ�
    public float rotationSpeed = 2.0f;  // ȸ�� �ӵ�

    private int currentNodeIndex = 0;  // ���� ��� �ε���

    private bool isMoving = false;
    private bool isReversing = false;  // ���������� �̵� ������ ����

    void Start()
    {
        bluetoothHelper = BT_Comm.Instance;

        Invoke("OnMove", 1.0f);  // ���� 2�� �ĺ��� �����̵��� ����
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
            // ��� �������� ȸ��
            Vector3 direction = (pathNodes[currentNodeIndex].position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * rotationSpeed);

            // ȸ�� ������ ���� ���� ���� ���� ���� ������ �̵�
            if (Quaternion.Angle(transform.rotation, lookRotation) < 10.0f) // 10�� �̳��� �̵�
            {
                // ������ �̵�
                transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime);
            }

            // ���� ��忡 �����ߴ��� Ȯ��
            float distanceToNextNode = Vector3.Distance(transform.position, pathNodes[currentNodeIndex].position);
            if (distanceToNextNode < 0.2f)  // �� ���� �ʿ信 ���� ���� ���� (���� 0.1f���� 0.2f�� ����)
            {
                // ���� ���� �̵�
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
