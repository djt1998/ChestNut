using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_cycle : MonoBehaviour
{
    public float x_speed;
    public float x_range_pos;
    public float x_range_neg;
    public float y_speed;
    public float y_range_pos;
    public float y_range_neg;
    public float z_speed;
    public float z_range_pos;
    public float z_range_neg;
    private float x_dir, y_dir, z_dir;
    private float x_diff, y_diff, z_diff;
    private float x_center, y_center, z_center;
    private float x_rot,y_rot,z_rot;
    // Start is called before the first frame update
    void Start()
    {
        x_center = transform.localRotation.eulerAngles.x;
        y_center = transform.localRotation.eulerAngles.y;
        z_center = transform.localRotation.eulerAngles.z;
        x_dir = 1.0f;
        y_dir = 1.0f;
        z_dir = 1.0f;
        x_rot = 0.0f;
        y_rot = 0.0f;
        z_rot = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(x_rot > x_range_pos){
            x_dir = -1.0f;
        }
        else if(x_rot < x_range_neg){
            x_dir = 1.0f;
        }
        if(y_rot > y_range_pos){
            y_dir = -1.0f;
        }
        else if(y_rot < y_range_neg){
            y_dir = 1.0f;
        }
        if(z_rot > z_range_pos){
            z_dir = -1.0f;
        }
        else if(z_rot < z_range_neg){
            z_dir = 1.0f;
        }
        //Debug.Log(diff);
        Vector3 rotation = new Vector3(x_speed*x_dir*Time.deltaTime, y_speed*y_dir*Time.deltaTime, z_speed*z_dir*Time.deltaTime);
        Debug.Log(rotation[1]);
        x_rot += rotation[0];
        y_rot += rotation[1];
        z_rot += rotation[2];
        transform.Rotate(rotation, Space.Self);
    }
}
