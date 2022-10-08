using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public int force_coef;
    public float density;
    private Vector3 speed;
    // private int spd_val;
    private float player_radius;
    // private bool is_jump;
    public int keyStatus;
    private Vector3[] forceDir = {Vector3.left, Vector3.forward, Vector3.right, Vector3.back};

    // for statistics
    // public int blue_cube_num = 0;
    // public int red_cube_num = 0;

    // Start is called before the first frame update
    void Start()
    {
        keyStatus = 0;
        // is_jump = false;
        speed = new Vector3(0, 0, 0);
        // spd_val = 5;
        rb.drag = 1;
        if (force_coef == 0) { force_coef = 25; }
        if (density == 0) { density = 0.25f; }
        if (rb.mass == 0) { rb.mass = 10; }
        player_radius = (float)transform.localScale[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameMenu.GameIsPaused) {
            // if (is_jump == false)
            // {
                if (Input.GetKey("a"))
                {
                    rb.AddForce(forceDir[0] * force_coef * (float)Math.Sqrt(player_radius));
                }
                else if (Input.GetKey("w"))
                {
                    rb.AddForce(forceDir[1] * force_coef * (float)Math.Sqrt(player_radius));
                }
                else if (Input.GetKey("d"))
                {
                    rb.AddForce(forceDir[2] * force_coef * (float)Math.Sqrt(player_radius));
                }
                else if (Input.GetKey("s"))
                {
                    rb.AddForce(forceDir[3] * force_coef * (float)Math.Sqrt(player_radius));
                }

                // if (Input.GetKey("space"))
                // {
                //     is_jump = true;
                //     rb.AddForce(Vector3.up * force_coef * 200);
                // }
            // }
        }

        /********************** just for fun **********************/
        // if (Input.GetKey("r"))
        // {
        //     change_radius(0.2f);
        // }
        // else if (Input.GetKey("f"))
        // {
        //     change_radius(-0.2f);
        // }
        // if (Input.GetKey("c")) {
        //     rb.position = GameObject.Find("Trophy").transform.position + new Vector3(0, 5, 0);
        // }
        /********************** just for fun **********************/
    }

    public void change_radius(float amount)
    {
        if(player_radius + amount < 0.1)
        {
            // GameMenu.IsDead = true;
            return;
        }
        player_radius += amount;
        transform.localScale = new Vector3(player_radius, player_radius, player_radius);
        Debug.Log("radius: " + player_radius + "\nmass: " + rb.mass);
       /* float scale = transform.localScale[0];
        Vector3 pos = transform.position;
        pos[1] += scale / 2;
        transform.position = pos;*/
        rb.mass += amount / density;
    }

    public float getRadius() {
        return player_radius;
    }

    public void force_direction_shift(float angle) {
        for (int i = 0; i < 4; i++) {
            forceDir[i] = Quaternion.Euler(0, angle, 0) * forceDir[i];
            // Debug.Log(i + ": " + forceDir[i]);
        }
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     is_jump = false;
    // }

    // private void OnTriggerEnter(Collider other) {
    //     if (other.name == "Blue Cube"){
    //         blue_cube_num++;
    //         other.gameObject.GetComponent<collectable>().Interaction();
    //         Destroy(other.gameObject);
    //         Debug.Log("Eat blue:\nBule: " + blue_cube_num + "\nRed: " + red_cube_num);
    //     }
    //     else if (other.name == "Red Cube"){
    //         red_cube_num++;
    //         other.gameObject.GetComponent<collectable>().Interaction();
    //         Destroy(other.gameObject);
    //         Debug.Log("Eat red:\nBule: " + blue_cube_num + "\nRed: " + red_cube_num);
    //     }
    //     else if (other.name == "Trophy"){
    //         Destroy(other.gameObject);
    //         Debug.Log("Collect Trophy");
    //         GameMenu.IsWon = true;
    //     }
    // }
}
