using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    public float shakeSpeed = 15.0f; // ��鸲 �ӵ�
    public float shakeAmount = 0.0002f; // ��鸲�� ũ��

    private Quaternion originalRotation; // �ʱ� rotation ��
    private Vector3 originalPosition; // �ʱ� position ��

    void Start()
    {
        originalRotation = transform.rotation; // �ʱ� rotation �� ����
        originalPosition = transform.position; // �ʱ� position �� ����

        StartCoroutine(StartShakeAfterDelay(5.0f));
    }

    void Update()
    {
        /*// �¿�� ��鸮�� ȸ���� ���
        float shake = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

        // ���� rotation�� ��鸲�� ����
        Quaternion targetRotation = originalRotation * Quaternion.Euler(shake, 0, 0);
        transform.rotation = targetRotation;

        // position�� ������Ŵ
        transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);*/
        StartCoroutine(StartShakeAfterDelay(5.0f));
    }

    IEnumerator StartShakeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Shake ����
        // �¿�� ��鸮�� ȸ���� ���
        float shake = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

        // ���� rotation�� ��鸲�� ����
        Quaternion targetRotation = originalRotation * Quaternion.Euler(shake, 0, 0);
        transform.rotation = targetRotation;

        // position�� ������Ŵ
        transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
        enabled = true;
    }
}
