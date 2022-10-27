using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Linear_cycle : MonoBehaviour
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
    private Vector3 center_position;
    private float x_dir, y_dir, z_dir;
    // Start is called before the first frame update
    void Start()
    {
        center_position = transform.position;
        x_dir = 1.0f;
        y_dir = 1.0f;
        z_dir = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 diff = transform.position - center_position;
        if(diff[0] > x_range_pos){
            x_dir = -1.0f;
        }
        else if(diff[0] < x_range_neg){
            x_dir = 1.0f;
        }
        if(diff[1] > y_range_pos){
            y_dir = -1.0f;
        }
        else if(diff[1] < y_range_neg){
            y_dir = 1.0f;
        }
        if(diff[2] > z_range_pos){
            z_dir = -1.0f;
        }
        else if(diff[2] < z_range_neg){
            z_dir = 1.0f;
        }
        Debug.Log(diff);
        Vector3 movement = new Vector3(x_speed*x_dir, y_speed*y_dir, z_speed*z_dir);
        Debug.Log(movement);
        transform.Translate(movement * Time.deltaTime, Space.World);
    }

    
}
