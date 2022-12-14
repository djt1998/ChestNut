using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public Transform item;
    private Player player;
    public float ratio;
    public float force_coef;
    private float timeToplaySound = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        if (force_coef == 0) { force_coef = 200; }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (item != null)
        {
            item.transform.Rotate(0.0f, 2f, 0.0f, Space.Self);
        }
        timeToplaySound -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.attachedRigidbody){
            Debug.Log("Wind Active");
            other.attachedRigidbody.AddForce(transform.up * force_coef);
        }

        if (timeToplaySound < 0f && other.name == "Player_model") {
            timeToplaySound = 1f;
            SoundEffectManger.PlaySound("Wind");
        }
        
        // if (other.name == "Player_model")
        // {
            // Debug.Log("Wind Active");
            // player.rb.AddForce(transform.up * force_coef);
        // other.gameObject.transform.localScale = new Vector3(1,1,1);
        // }
    }
}
