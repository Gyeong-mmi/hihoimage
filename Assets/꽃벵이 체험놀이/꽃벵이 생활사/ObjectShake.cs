using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    public float shakeSpeed = 15.0f; // 흔들림 속도
    public float shakeAmount = 0.0002f; // 흔들림의 크기

    private Quaternion originalRotation; // 초기 rotation 값
    private Vector3 originalPosition; // 초기 position 값

    void Start()
    {
        originalRotation = transform.rotation; // 초기 rotation 값 저장
        originalPosition = transform.position; // 초기 position 값 저장

        StartCoroutine(StartShakeAfterDelay(5.0f));
    }

    void Update()
    {
        /*// 좌우로 흔들리는 회전값 계산
        float shake = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

        // 현재 rotation에 흔들림을 적용
        Quaternion targetRotation = originalRotation * Quaternion.Euler(shake, 0, 0);
        transform.rotation = targetRotation;

        // position은 고정시킴
        transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);*/
        StartCoroutine(StartShakeAfterDelay(5.0f));
    }

    IEnumerator StartShakeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Shake 시작
        // 좌우로 흔들리는 회전값 계산
        float shake = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

        // 현재 rotation에 흔들림을 적용
        Quaternion targetRotation = originalRotation * Quaternion.Euler(shake, 0, 0);
        transform.rotation = targetRotation;

        // position은 고정시킴
        transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
        enabled = true;
    }
}
