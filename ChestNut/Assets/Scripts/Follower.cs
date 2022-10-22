using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject player;
    public float zoom_in_param = 1.25f; // key board zoom
    public float zoom_out_param = 1.25f;
    public bool is_active = false;  // is in use
    public float rotateDeltaX = 1f; // key board rotate
    public float rotateDeltaY = 3f;
    public float mouseZoomSpeed = 2f;  // mouse zoom
    public float mouseSensitivityX = 3f;   // mouse rotate
    public float mouseSensitivityY = 1f;
    public float distance = 5f;
    public float minDistance;
    public float maxDistance = 20f;
    public float myRotationX = 30f;
    public float myRotationY = 0f;
    public float minAngleX = 10f;
    public float maxAngleX = 90f;
    public float damping = 10f;
    public int mode = 0;    // 0: normal; 1: topdown; 2: first person
    private float orignal_distance;
    private Player p;
    private bool zoom_enabled = true;
    private bool xrotation_enabled = true;
    private bool yrotation_enabled = true;
    private bool damping_enabled = true;
    // private float myTime = 0.0f;
    // private float nextRotate = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();
        orignal_distance = distance;
        minDistance = p.getRadius();
        if (mode == 1) {    // topdown
            myRotationX = 90f;
            xrotation_enabled = false;
        }
        if (mode == 2) {    // first person
            minAngleX = -10f;
            maxAngleX = 20f;
            zoom_enabled = false;
            distance = minDistance;
            damping_enabled = false;
        }
        cameraTransformation();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        minDistance = p.getRadius();
        if (mode == 2) {
            distance = p.getRadius();
        }

        // zooming
        if (zoom_enabled){
            if (Input.GetKey("=")) {    // zoom in
                distance /= zoom_in_param;
            }
            else if (Input.GetKey("-")) {    // zoom out
                distance *= zoom_out_param;
            }
            else if (Input.GetKey("0")) {
                distance = orignal_distance;
            }
            distance -= Input.GetAxis("Mouse ScrollWheel") * mouseZoomSpeed;
        }

        // rotation
        if (yrotation_enabled) {
            float delta = 0f;
            if (Input.GetKey("l")) {    // rotate right
                delta += rotateDeltaY;
            }
            if (Input.GetKey("j")) {    // rotate left
                delta -= rotateDeltaY;
            }
            if (Input.GetMouseButton(1)) {  // mouse right click
                delta += Input.GetAxis("Mouse X") * mouseSensitivityX;
            }
            myRotationY += delta;
            if (is_active == true) {
                p.force_direction_shift(delta);
            }
        }
        if (xrotation_enabled) {
            if (Input.GetKey("i")) {    // rotate up
                myRotationX += rotateDeltaX;
            }
            if (Input.GetKey("k")) {    // rotate down
                myRotationX -= rotateDeltaX;
            }
            if (Input.GetMouseButton(1)) {  // mouse right click
                myRotationX -= Input.GetAxis("Mouse Y") * mouseSensitivityY;
            }
        }
        cameraTransformation();
    }

    public void lockCamera() {
        zoom_enabled = false;
        xrotation_enabled = false;
        yrotation_enabled = false;
    }

    public void unlockCamera() {
        zoom_enabled = !(mode == 2);
        xrotation_enabled = !(mode == 1);
        yrotation_enabled = true;
    }

    private void cameraTransformation() {
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        myRotationX = Mathf.Clamp(myRotationX, minAngleX, maxAngleX);
        Quaternion myRotation = Quaternion.Euler(myRotationX, myRotationY, 0);
        transform.rotation = damping_enabled ? Quaternion.Lerp(transform.rotation, myRotation, Time.deltaTime * damping) : myRotation;
        Vector3 myPosition = myRotation * new Vector3(0, 0, -distance) + player.transform.position;
        if (mode == 2) {
            myPosition += new Vector3(0, distance / 2, 0);
        }
        transform.position = damping_enabled ? Vector3.Lerp(transform.position, myPosition, Time.deltaTime * damping) : myPosition;
    }
}