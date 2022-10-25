using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform player;
    public Camera topDown;

    private Vector3 offset = new Vector3(0, 7, 0);

    private void Awake() {
        transform.position = player.position + offset;
    }

    void LateUpdate()
    {
        transform.position = player.position + offset;
        transform.rotation = topDown.transform.rotation;
    }
}
