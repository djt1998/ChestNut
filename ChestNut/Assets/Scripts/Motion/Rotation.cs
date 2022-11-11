using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float x_speed;
    public float y_speed;
    public float z_speed;
    private Vector3 center_position;
    private float x_dir, y_dir, z_dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(x_speed, y_speed, z_speed, Space.Self);
    }
}
