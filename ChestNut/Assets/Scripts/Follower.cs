using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0, 3, -5);
    public float zoom_in_param = 1.25f;
    public float zoom_out_param = 1.25f;

    public float zoom_out_max = 20f;
    public bool is_active = false;  // is in use
    
    private Vector3 orignal_offset;
    private Player p;
    private float myTime = 0.0f;
    private float nextRotate = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();
        orignal_offset = offset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myTime = myTime + Time.deltaTime;
        while (offset.magnitude < p.getRadius()) {
            offset = new Vector3(offset.x * zoom_out_param, offset.y * zoom_out_param, offset.z * zoom_out_param);
        }
        if (Input.GetKey("=") && offset.magnitude / zoom_in_param > p.getRadius()) {    // zoom in
            offset = new Vector3(offset.x / zoom_in_param, offset.y / zoom_in_param, offset.z / zoom_in_param);
        }
        else if (Input.GetKey("-") && offset.magnitude * zoom_out_param < zoom_out_max) {    // zoom out
            offset = new Vector3(offset.x * zoom_out_param, offset.y * zoom_out_param, offset.z * zoom_out_param);
        }
        else if (Input.GetKey("0") && orignal_offset.magnitude > p.getRadius()) {
            offset = orignal_offset;
        }
        else if (Input.GetKey("l") && myTime > nextRotate) {    // rotate right
            myTime = 0.0f;
            offset = Quaternion.Euler(0, 90, 0) * offset;
            var rot = transform.rotation.eulerAngles;
            rot.y += 90;
            transform.rotation = Quaternion.Euler(rot);
            if (is_active == true) {
                p.force_direction_shift(90);
            }
        }
        else if (Input.GetKey("j") && myTime > nextRotate) {    // rotate left
            myTime = 0.0f;
            offset = Quaternion.Euler(0, -90, 0) * offset;
            var rot = transform.rotation.eulerAngles;
            rot.y -= 90;
            transform.rotation = Quaternion.Euler(rot);
            if (is_active == true) {
                p.force_direction_shift(-90);
            }
        }
        // Offset camera
        transform.position = player.transform.position + offset;
    }
}
