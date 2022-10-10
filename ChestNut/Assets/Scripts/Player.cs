using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


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
    public float min_radius;
    public float max_radius;

    public GameObject player_transform;
    private Vector3[] forceDir = {Vector3.left, Vector3.forward, Vector3.right, Vector3.back};
    private TextMeshProUGUI txt;

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
        if (force_coef == 0) { 
            force_coef = 25; 
        }
        if (density == 0) { 
            density = 0.25f;
        }
        if (rb.mass == 0) { 
            rb.mass = 10; 
        }
        if (min_radius == 0) { 
            min_radius = 0.4f;
        }
        if (max_radius == 0) {
            max_radius = 4f;
        }
        player_radius = (float)transform.localScale[0];
        txt = GameObject.Find("Canvas").transform.Find("InGameDisplay/PlayerInfo").GetComponent<TextMeshProUGUI>();
        // txt.text = "Speed: 00.00 m/s\nKeys: 0";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameMenu.GameIsPaused) {
            // if (!is_jump)
            // {
                // float alpha = (float) Math.Sqrt(player_radius);
                // alpha = 1f / alpha + 1f * alpha * (max_radius - alpha);
                float alpha = 2f / (1f + Mathf.Exp(2f * player_radius - 3f));
                Vector3 force_direction = new Vector3(0, 0, 0);
                if (Input.GetKey("a"))
                {
                    force_direction += forceDir[0];
                }
                if (Input.GetKey("w"))
                {
                    force_direction += forceDir[1];
                }
                if (Input.GetKey("d"))
                {
                    force_direction += forceDir[2];
                }
                if (Input.GetKey("s"))
                {
                    force_direction += forceDir[3];
                }

                rb.AddForce(force_direction.normalized * force_coef * alpha);

                // if (Input.GetKey("space"))
                // {
                //     is_jump = true;
                //     rb.AddForce(Vector3.up * force_coef * 200);
                // }
            // }
        }

        txt.text = "Speed: " + string.Format("{0:00}.{1:00} m/s\nKeys: {2}", Mathf.FloorToInt(rb.velocity.magnitude), (rb.velocity.magnitude % 1) * 100, keyStatus);

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

    // true: if changed; false: invalid
    public bool change_radius(float amount)
    {
        if(player_radius + amount < min_radius || player_radius + amount > max_radius)
        {
            // GameMenu.IsDead = true;
            return false;
        }
        player_radius += amount;
        transform.localScale = new Vector3(player_radius, player_radius, player_radius);
       /* float scale = transform.localScale[0];
        Vector3 pos = transform.position;
        pos[1] += scale / 2;
        transform.position = pos;*/
        rb.mass += amount / density;
        Debug.Log("radius: " + player_radius + "\nmass: " + rb.mass);
        return true;
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
