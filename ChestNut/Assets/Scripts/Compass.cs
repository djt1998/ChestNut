using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public Vector3 northDirection;
    public Transform player;
    public Quaternion targetDirection;
    public RectTransform northLayer;
    public RectTransform targetLayer;
    public Transform target;

    void FixedUpdate()
    {
        changeNorthDirection();
        changeTargetDirection();
    }

    private void changeNorthDirection() {
        northDirection.z = player.eulerAngles.y;
        northLayer.localEulerAngles = northDirection;
    }

    private void changeTargetDirection() {
        Vector3 dir = transform.position - target.position;
        targetDirection = Quaternion.LookRotation(dir);
        targetDirection.z = -targetDirection.y;
        targetDirection.x = 0f;
        targetDirection.y = 0f;
        targetLayer.localRotation = targetDirection * Quaternion.Euler(northDirection);
    }
}
