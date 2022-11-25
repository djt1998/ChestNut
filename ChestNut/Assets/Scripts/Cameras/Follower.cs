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
    public int mode = 0;    // 0: normal; 1: topdown; 2: first person; 3: consistent to player's motion
    private float orignal_distance;
    private Player p;
    private bool zoom_enabled = true;
    private bool xrotation_enabled = true;
    private bool yrotation_enabled = true;
    private bool damping_enabled = true;

    private HashSet<Transform> Obstructions = new HashSet<Transform>();
    private HashSet<string> nonObstructionNames = new HashSet<string>();
    private int renderingMode = 2;    // 0: no transparency; 1: shadow only; 2: semi transparent; 3: enable render
    private float transparentRate = 5f;
    private Physics physics;
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
        // if (mode == 3) {    // player motion
        //     yrotation_enabled = false;
        //     // myRotationY = -Mathf.Acos(Vector3.Dot(p.rb.velocity.normalized, Vector3.right)) * Mathf.Rad2Deg;
        // }
        nonObstructionNames.Add("Red Cube");
        nonObstructionNames.Add("Blue Cube");
        nonObstructionNames.Add("Trophy");
        nonObstructionNames.Add("Logo");
        nonObstructionNames.Add("logo_chestnut");
        nonObstructionNames.Add("YellowRod");
        nonObstructionNames.Add("Player");
        nonObstructionNames.Add("Player_model");
        nonObstructionNames.Add("Large Cube");
        cameraTransformation();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        minDistance = p.getRadius();
        if (mode == 2) {
            distance = p.getRadius();
        }
        // else if (mode == 3) {
        //     float newRotationY = Mathf.Acos(Vector3.Dot(p.rb.velocity.normalized, Vector3.right)) * Mathf.Rad2Deg;
        //     if (is_active == true) {
        //         p.force_direction_shift(newRotationY - myRotationY);
        //         myRotationY = newRotationY;
        //     }
        // }

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
            if (is_active) {
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
        if (is_active) {
            detectObstructions();
        }
    }

    public void lockCamera() {
        zoom_enabled = false;
        xrotation_enabled = false;
        yrotation_enabled = false;
    }

    public void unlockCamera() {
        zoom_enabled = !(mode == 2);
        xrotation_enabled = !(mode == 1);
        yrotation_enabled = true;   // !(mode == 3)
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

    private void detectObstructions() {
        HashSet<Transform> newHits = new HashSet<Transform>();
        // RaycastHit[] hits = Physics.RaycastAll(transform.position, player.transform.position - transform.position, Vector3.Distance(player.transform.position, transform.position));
        RaycastHit[] hits = physics.FollowSpotCastAll(transform.position, player.transform.position, 3f);
        foreach (RaycastHit hit in hits) {     // hide
            var renderer = hit.collider.gameObject.GetComponent<Renderer>();
            if (!nonObstructionNames.Contains(hit.collider.gameObject.name) && renderer != null && renderer.enabled) {
                if (!Obstructions.Contains(hit.transform)) {
                    if (renderingMode == 1) {
                        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    }
                    else if (renderingMode == 2 && renderer.material != null) {
                        if (!renderer.material.HasProperty("_Mode") || renderer.material.GetFloat("_Mode") != 0) {
                            continue;
                        }
                        // Material[] ms = renderer.materials;
                        // for (int i = 0; i < ms.Length; i++) {
                        //     MaterialMode.SetMaterialRenderingMode(ms[i], MaterialMode.BlendMode.Transparent);
                        //     Color c = ms[i].color;
                        //     c.a /= transparentRate;
                        //     ms[i].SetColor("_Color", c);
                        // }
                        // if (renderer.material.GetFloat("_Mode") == 0) {
                        MaterialMode.SetMaterialRenderingMode(renderer.material, MaterialMode.BlendMode.Transparent);
                        // }
                        Color c = renderer.material.color;
                        c.a /= transparentRate;
                        // c.a = 0.2f;
                        renderer.material.SetColor("_Color", c);
                    }
                    else if (renderingMode == 3) {
                        renderer.enabled = false;
                    }
                }
                newHits.Add(hit.collider.gameObject.transform);
            }
        }
        Obstructions.ExceptWith(newHits);    // recover
        recoverObstructions();
        Obstructions.UnionWith(newHits);
    }

    public void recoverObstructions() {
        foreach (var ob in Obstructions) {
            if (ob != null) {
                var renderer = ob.GetComponent<Renderer>();
                if (renderingMode == 1) {
                    renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                }
                else if (renderingMode == 2 && renderer.material != null) {
                    // if (renderer.material.GetFloat("_Mode") == 0) {
                    MaterialMode.SetMaterialRenderingMode(renderer.material, MaterialMode.BlendMode.Opaque);
                    // }
                    Color c = renderer.material.color;
                    c.a *= transparentRate;
                    // c.a = 1f;
                    renderer.material.SetColor("_Color", c);
                }
                else if (renderingMode == 3) {
                    renderer.enabled = true;
                }
            }
        }
        Obstructions.Clear();
    }
}