using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Block : MonoBehaviour
{
    private Player player;
    public Rigidbody rb;
    public int min_mass;
    public int waitSec;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        if (min_mass == 0) { min_mass = 9; }
        if (waitSec == 0) { waitSec = 2; }
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void change_color(float r, float g, float b, float a){
        var renderer = GetComponent<Renderer>();
        Color customColor = new Color(r, g, b, a);
        renderer.material.SetColor("_Color", customColor);
    }

    IEnumerator FallingCoroutine()
    {
        Debug.Log("Started Count Falling at timestamp : " + Time.time);
        
        for(int i = 0; i < 3; i ++){
            change_color(0.6f, 0.6f, 1.0f, 0.4f);
            yield return new WaitForSeconds((float)waitSec/10.0f);
            change_color(0.6f, 0.6f, 1.0f, 0.2f);
            yield return new WaitForSeconds((float)waitSec/10.0f);
        }
        change_color(0.6f, 0.6f, 1.0f, 0.4f);
        yield return new WaitForSeconds((float)waitSec/10.0f * 4);
        rb.constraints = RigidbodyConstraints.None;
        rb.isKinematic = false;
        Debug.Log("Finished Falling at timestamp : " + Time.time);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player_model")
        {
            if (player.rb.mass > min_mass)
            {
                StartCoroutine(FallingCoroutine());
            }
        }
    }
}
