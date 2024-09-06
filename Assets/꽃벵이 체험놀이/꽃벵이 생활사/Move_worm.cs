using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_worm : MonoBehaviour
{
    private Vector3 initPos;
    private Quaternion initRotation;
    private Vector3 targetPos;

    public float moveSpeed = 0.05f;
    public float rotationSpeed = 0.1f;
    public float distanceToMoveStraight = 0.05f;
    public float returnToInitPosThreshold = 0.1f;

    private bool isMovingStraight = false;
    private bool isRotating = false;
    private bool movingRight = true;

    public float maxGrowth = 0.6f; // 최대 크기
    public float growthRate = 0.0001f; // 크기 증가 속도

    public GameObject targetPosition;

    private void Start()
    {
        // 초기 위치 설정
        initPos = transform.position;
    }

    void Update()
    {
        MoveTowardsTarget();
        StartCoroutine(GrowAfterDelay(3.0f));
    }

    void MoveTowardsTarget()
    {
        // 현재 위치에서 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.transform.position, moveSpeed * Time.deltaTime);


    }

    IEnumerator GrowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 크기 증가 시작
        while (transform.localScale.x < maxGrowth)
        {
            // 현재 크기 저장
            Vector3 currentScale = transform.localScale;

            // 크기 증가
            currentScale += Vector3.one * growthRate * Time.deltaTime;

            // 최대 크기를 넘지 않도록 보정
            currentScale = Vector3.Min(currentScale, Vector3.one * maxGrowth);

            // 오브젝트에 새로운 크기 적용
            transform.localScale = currentScale;

            yield return null;
        }
    }
}