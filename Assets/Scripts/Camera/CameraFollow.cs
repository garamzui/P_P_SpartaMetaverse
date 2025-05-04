using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 플레이어
    public Vector3 offset;

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
