using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door: MonoBehaviour
{
    private Player player;
    // public GameObject cylinder;
    public Rigidbody rb;
    public GameObject destroyEffect;
    private bool isLocked;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        rb.isKinematic = true;
        isLocked = true;
    }

    // Update is called once per frame
    void FiexedUpdate()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player_model")
        {
            if (player.keyStatus > 0 && isLocked == true)
            {
                // rb.constraints = RigidbodyConstraints.FreezePositionY;
                // rb.isKinematic = false;
                // isLocked = false;
                player.keyStatus -= 1;
                SoundEffectManger.PlaySound("UnlockDoor");
                Instantiate(destroyEffect, transform.position, transform.rotation);
                // Destroy(cylinder);
                Destroy(gameObject);
            }
        }
    }
}
