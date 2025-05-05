using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
            target = playerObj.transform;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // ����: X,Y�� ���󰡴� ���
            Vector3 newPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}