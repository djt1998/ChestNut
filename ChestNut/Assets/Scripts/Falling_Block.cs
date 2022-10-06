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

    IEnumerator FallingCoroutine()
    {
        Debug.Log("Started Count Falling at timestamp : " + Time.time);
        yield return new WaitForSeconds(waitSec);
        rb.constraints = RigidbodyConstraints.None;
        rb.isKinematic = false;
        Debug.Log("Finished Falling at timestamp : " + Time.time);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player")
        {
            if (player.rb.mass > min_mass)
            {
                StartCoroutine(FallingCoroutine());
            }
        }
    }

}
