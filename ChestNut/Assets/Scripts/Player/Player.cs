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
    public float radius {
        get {
            return player_radius;
        }
    }
    // private bool is_jump;
    public int keyStatus;
    public int logoStatus;
    public float min_radius;
    public float max_radius;
    public float max_density_diff;

    public float size_recover_coef;
    public ParticleSystem dust;
    public GameObject player_model;
    public LayerMask SnowGround;
    private float timeToplaySound = 0f;

    // private Vector3 force_direction = Vector3.zero;
    // public Vector3 ForceDir {
    //     get {
    //         return force_direction;
    //     }

    //     set {
    //         force_direction = value;
    //     }
    // }
    private ButtonMovement BM;

    private Vector3[] forceDir = {Vector3.left, Vector3.forward, Vector3.right, Vector3.back};
    private int player_effect;
    private int counter;
    private float r,g,b,a;


    // for statistics
    // public int blue_cube_num = 0;
    // public int red_cube_num = 0;

    // Start is called before the first frame update
    void Start()
    {
        keyStatus = 0;
        logoStatus = 0;
        // is_jump = false;
        speed = new Vector3(0, 0, 0);
        // spd_val = 5;
        rb.drag = 1;
        if (force_coef == 0) { 
            force_coef = 25; 
        }
        if (density == 0) { 
            density = 4f;
        }
        if (rb.mass == 0) { 
            rb.mass = 10; 
        }
        if (min_radius == 0) { 
            min_radius = 0.4f;
        }
        if (max_radius == 0) {
            max_radius = 3f;
        }
        if (size_recover_coef == 0) {
            size_recover_coef = 0.005f;
        }
        if (max_density_diff == 0) {
            max_density_diff = 2f;
        }
        player_radius = (float)transform.localScale[0];
        counter = 0;
        if (GlobalData.controlMode == 1) {
            BM = FindObjectOfType<ButtonMovement>();
        }
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
                // alpha = 2f / (1f + Mathf.Exp(2f * player_radius - 3f));
                Vector3 force_direction = Vector3.zero;
                if (GlobalData.controlMode == 0) {
                    if (Input.GetKey("a") || Input.GetKey("left"))
                    {
                        force_direction += forceDir[0];
                    }
                    if (Input.GetKey("w") || Input.GetKey("up"))
                    {
                        force_direction += forceDir[1];
                    }
                    if (Input.GetKey("d") || Input.GetKey("right"))
                    {
                        force_direction += forceDir[2];
                    }
                    if (Input.GetKey("s") || Input.GetKey("down"))
                    {
                        force_direction += forceDir[3];
                    }
                }
                else if (GlobalData.controlMode == 1) {
                    force_direction = BM.pos.x * forceDir[2] + BM.pos.y * forceDir[1];
                }

                rb.AddForce(force_direction.normalized * force_coef * Mathf.Max(sigmoid(rb.mass, 0.05f, 26f), sigmoid(rb.mass, 0.4f, 9f)) * 2f);

                // if (Input.GetKey("space"))
                // {
                //     is_jump = true;
                //     rb.AddForce(Vector3.up * force_coef * 200);
                // }
            // }
            if(counter > 100000){
                counter = 0;
            }
            counter += 1;
            normalization();
            effect_update();
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
        // if (rb.velocity.magnitude > 0.5f) {
        //     PlayDust();
        // }
        /********************** just for fun **********************/
        // timeToplaySound -= Time.deltaTime;
        // if (timeToplaySound < 0f) {
        //     timeToplaySound = 1f;
        //     if (IsOnSnowGround() && rb.velocity.magnitude > 0.5f) {
        //         SoundEffectManger.PlaySound("SnowBallMoveOnSnowGround");
        //     }
        //     else {
        //         SoundEffectManger.StopSound("SnowBallMoveOnSnowGround");
        //     }
        // }
    }

    public bool IsOnSnowGround() {
        return Physics.CheckSphere(player_model.transform.position, player_radius * 0.9f, SnowGround);
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
        rb.mass += amount * density;
        Debug.Log("radius: " + player_radius + "\nmass: " + rb.mass);
        return true;
    }

    // Try to normalized ball to default density,
    // But not exceed mass_recover_coef speed
    // return True if normalized, else return False
    public bool normalization()
    {
        float current_density = rb.mass / player_radius;
        if(Math.Abs(current_density-density) < 0.1){
            return true;
        }
        Debug.Log("current_density: " + current_density + "  Density: " + density);
        // float density_diff = current_density - density;
        float target_radius = rb.mass / density;
        float size_diff = target_radius - player_radius;
        triger_effect(4);
        if(Math.Abs(size_diff) > size_recover_coef){
            player_radius += (size_diff) / Math.Abs(size_diff) * size_recover_coef;
            transform.localScale = new Vector3(player_radius, player_radius, player_radius);
            Debug.Log("Normalizing" +(size_diff) / Math.Abs(size_diff) * size_recover_coef);
            return false;
        }
        else{
            player_radius = target_radius;
            transform.localScale = new Vector3(player_radius, player_radius, player_radius);
            Debug.Log("Normalizing" );
            return true;
        }
        
        
    }

    // detached_change_radius Method:
    // Same with change _radius Size Change,
    // Mass remain the same.
    public bool detached_change_radius(float amount){
        if(player_radius + amount < min_radius || player_radius + amount > max_radius)
        {
            // GameMenu.IsDead = true;
            return false;
        }
        player_radius += amount;
        transform.localScale = new Vector3(player_radius, player_radius, player_radius);
        Debug.Log("radius: " + player_radius + "\nmass: " + rb.mass);
        return true;
    }

    public bool detached_change_mass(float amount){
        float target_density = (rb.mass+amount) / player_radius;
        if(Math.Abs(target_density - density) > max_density_diff)
        {
            // GameMenu.IsDead = true;
            return false;
        }
        rb.mass += amount;
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

    private static float sigmoid(float x, float alpha, float beta) {
        return 1f / (1f + Mathf.Exp(alpha * (x - beta)));
    }


    public void triger_effect(int effect){
        // use mask: effect%10 is color, (int) effect/10 is transparecy
        switch(effect){
            case 1:
                player_effect = ((int)player_effect/10)*10 + 1;
                break;
            case 2:
                player_effect = ((int)player_effect/10)*10 + 2;
                break;
            case 3:
                player_effect = 10+(player_effect%10);
                break;
            case 4:
                player_effect = 20+(player_effect%10);
                break;
            default:
                break;
        }
            
        
    }

    private void change_color(float r, float g, float b, float a){
        var player_Renderer = GameObject.Find("Player_model").GetComponent<Renderer>();
        Color customColor = new Color(r, g, b, a);
        player_Renderer.material.SetColor("_Color", customColor);
        player_effect = 0;
    }

    private void effect_update(){
        float flashing_rate;
        switch (player_effect%10)
        {
            case 1: // Turn RED
                flashing_rate = 1.0f - (0.8f*(((float) counter%50)/50.0f));
                r = 1.0f;
                g = flashing_rate;
                b = flashing_rate;
                Debug.Log("change color: " + flashing_rate + "  Counter = " + counter);
                break;
            case 2:  // Turn Blue
                flashing_rate = 0.2f + (0.8f*(((float) counter%50)/50.0f));
                r = flashing_rate;
                g = flashing_rate;
                b = 1.0f;
                Debug.Log("change color: " + flashing_rate + "  Counter = " + counter);
                break;
            default:
                r = 1.0f;
                g = 1.0f;
                b = 1.0f;
                break;
        }
        switch ((int)player_effect/10){
            case 1:   //Turn Solid
                flashing_rate = 1.0f - (0.8f*(((float) counter%50)/50.0f));
                a = flashing_rate;
                Debug.Log("change transparent: " + flashing_rate + "  Counter = " + counter);
                break;
            case 2:   //Turn Transparent
                flashing_rate = 0.2f + (0.8f*(((float) counter%50)/50.0f));
                a = flashing_rate;
                Debug.Log("change transparent: " + flashing_rate + "  Counter = " + counter);
                break;
            default:
                a = 1.0f - sigmoid((rb.mass / player_radius), 2.0f, 4.0f);
                break;
        }
        change_color(r, g, b, a);
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

    private void PlayDust() {
        dust.Play();
    }
}
