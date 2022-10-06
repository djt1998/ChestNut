using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0, 3, -5);
    public float zoom_in_param = 1.25f;
    public float zoom_out_param = 1.25f;
    private Vector3 orignal_offset;

    private Player p;

    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();
        orignal_offset = offset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        while (offset.magnitude < p.getRadius()) {
            offset = new Vector3(offset.x * zoom_out_param, offset.y * zoom_out_param, offset.z * zoom_out_param);
        }
        if (Input.GetKey("=") && offset.magnitude / zoom_in_param > p.getRadius()) {    // zoom in
            offset = new Vector3(offset.x / zoom_in_param, offset.y / zoom_in_param, offset.z / zoom_in_param);
        }
        else if (Input.GetKey("-")) {    // zoom out
            offset = new Vector3(offset.x * zoom_out_param, offset.y * zoom_out_param, offset.z * zoom_out_param);
        }
        else if (Input.GetKey("0") && orignal_offset.magnitude > p.getRadius()) {
            offset = orignal_offset;
        }
        // Offset camera
        transform.position = player.transform.position + offset;
    }
}
