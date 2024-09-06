using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatSpeed = 2.0f; // 떠다니는 속도 조절
    public float floatRange = 0.01f; // 떠다니는 범위 조절

    private float originalY; // 오브젝트의 초기 높이
    private float originalX; // 오브젝트의 초기 x 위치
    private float originalZ; // 오브젝트의 초기 z 위치

    void Start()
    {
        originalY = transform.position.y; // 오브젝트의 초기 높이 설정
        originalX = transform.position.x; // 오브젝트의 초기 x 위치 설정
        originalZ = transform.position.z; // 오브젝트의 초기 z 위치 설정
    }

    void FixedUpdate()
    {
        // 위아래로 둥둥 떠다니는 움직임 구현
        transform.position = new Vector3(originalX,
                                          originalY + Mathf.Sin(Time.time * floatSpeed) * floatRange,
                                          originalZ);
    }
}