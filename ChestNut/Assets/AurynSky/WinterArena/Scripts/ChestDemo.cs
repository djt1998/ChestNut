using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestDemo : MonoBehaviour
{

    //This script goes on the ChestComplete prefab;
    private Player player;
    public float ratio;
    public Rigidbody rb;
    public Animator chestAnim; //Animator for the chest;
    public ParticleSystem death;

    // Use this for initialization
    void Awake()
    {
        //get the Animator component from the chest;
        chestAnim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        rb.isKinematic = true;
        //start opening and closing the chest for demo purposes;
        //StartCoroutine(OpenCloseChest());
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.name == "Player_model")
        {
            StartCoroutine(OpenCloseChest());
        }
    }

    IEnumerator OpenCloseChest()
    {
        //play open animation;
        chestAnim.SetTrigger("open");
        //wait 2 seconds;
        yield return new WaitForSeconds(2);
        death.Play();
    }

}