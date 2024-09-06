using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatSpeed = 2.0f; // ���ٴϴ� �ӵ� ����
    public float floatRange = 0.01f; // ���ٴϴ� ���� ����

    private float originalY; // ������Ʈ�� �ʱ� ����
    private float originalX; // ������Ʈ�� �ʱ� x ��ġ
    private float originalZ; // ������Ʈ�� �ʱ� z ��ġ

    void Start()
    {
        originalY = transform.position.y; // ������Ʈ�� �ʱ� ���� ����
        originalX = transform.position.x; // ������Ʈ�� �ʱ� x ��ġ ����
        originalZ = transform.position.z; // ������Ʈ�� �ʱ� z ��ġ ����
    }

    void FixedUpdate()
    {
        // ���Ʒ��� �յ� ���ٴϴ� ������ ����
        transform.position = new Vector3(originalX,
                                          originalY + Mathf.Sin(Time.time * floatSpeed) * floatRange,
                                          originalZ);
    }
}