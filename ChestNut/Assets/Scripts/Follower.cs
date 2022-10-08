using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject player;
    public float zoom_in_param = 1.25f;
    public float zoom_out_param = 1.25f;
    public bool is_active = false;  // is in use
    public float rotateDeltaX = 1f;
    public float rotateDeltaY = 3f;
    public float distance = 5f;
    public float minDistance;
    public float maxDistance = 20f;
    public float myRotationX = 30f;
    public float myRotationY = 0f;
    public float minAngleX = 10f;
    public float maxAngleX = 90f;
    public float damping = 10f;
    public int mode = 0;    // 0: normal; 1: topdown
    
    private float orignal_distance;
    private Player p;
    // private float myTime = 0.0f;
    // private float nextRotate = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();
        orignal_distance = distance;
        minDistance = p.getRadius();
        if (mode == 1) {
            minAngleX = 90f;
        }
        cameraTransformation();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        minDistance = p.getRadius();

        // zooming
        while (distance < minDistance) {
            distance *= zoom_out_param;
        }
        if (Input.GetKey("=") && distance / zoom_in_param > minDistance) {    // zoom in
            distance /= zoom_in_param;
        }
        else if (Input.GetKey("-") && distance * zoom_out_param < maxDistance) {    // zoom out
            distance *= zoom_out_param;
        }
        else if (Input.GetKey("0") && orignal_distance > minDistance) {
            distance = orignal_distance;
        }

        // rotation
        if (Input.GetKey("l")) {    // rotate right
            myRotationY += rotateDeltaY;
            if (is_active == true) {
                p.force_direction_shift(rotateDeltaY);
            }
        }
        if (Input.GetKey("j")) {    // rotate left
            myRotationY -= rotateDeltaY;
            if (is_active == true) {
                p.force_direction_shift(-rotateDeltaY);
            }
        }
        if (Input.GetKey("i")) {    // rotate up
            myRotationX += rotateDeltaX;
        }
        if (Input.GetKey("k")) {    // rotate down
            myRotationX -= rotateDeltaX;
        }
        myRotationX = Mathf.Clamp(myRotationX, minAngleX, maxAngleX);
        cameraTransformation();
    }

    private void cameraTransformation() {
        Quaternion myRotation = Quaternion.Euler(myRotationX, myRotationY, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, myRotation, Time.deltaTime * damping);
        Vector3 myPosition = myRotation * new Vector3(0.0F, 0.0F, -distance) + player.transform.position;
        transform.position = Vector3.Lerp(transform.position, myPosition, Time.deltaTime * damping);
    }
}
