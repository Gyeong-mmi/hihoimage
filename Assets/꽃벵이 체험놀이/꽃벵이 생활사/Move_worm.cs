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

    public float maxGrowth = 0.6f; // �ִ� ũ��
    public float growthRate = 0.0001f; // ũ�� ���� �ӵ�

    public GameObject targetPosition;

    private void Start()
    {
        // �ʱ� ��ġ ����
        initPos = transform.position;
    }

    void Update()
    {
        MoveTowardsTarget();
        StartCoroutine(GrowAfterDelay(3.0f));
    }

    void MoveTowardsTarget()
    {
        // ���� ��ġ���� ��ǥ ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.transform.position, moveSpeed * Time.deltaTime);


    }

    IEnumerator GrowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // ũ�� ���� ����
        while (transform.localScale.x < maxGrowth)
        {
            // ���� ũ�� ����
            Vector3 currentScale = transform.localScale;

            // ũ�� ����
            currentScale += Vector3.one * growthRate * Time.deltaTime;

            // �ִ� ũ�⸦ ���� �ʵ��� ����
            currentScale = Vector3.Min(currentScale, Vector3.one * maxGrowth);

            // ������Ʈ�� ���ο� ũ�� ����
            transform.localScale = currentScale;

            yield return null;
        }
    }
}