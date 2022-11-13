using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBox : MonoBehaviour
{
    public float ratio;
    public Rigidbody rb;
    public Animator chestAnim; //Animator for the chest;
    private bool triggered;

    // Use this for initialization
    void Awake()
    {
        chestAnim = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        rb.isKinematic = true;
        triggered = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (!triggered && other.name == "Player_model")
        {
            triggered= true;
            StartCoroutine(OpenCloseChest());
        }
    }

    IEnumerator OpenCloseChest()
    {
        SoundEffectManger.PlaySound("OpenChestBox");
        //play open animation;
        chestAnim.SetTrigger("open");
        //wait 2 seconds;
        yield return new WaitForSeconds(2);
    }

}
