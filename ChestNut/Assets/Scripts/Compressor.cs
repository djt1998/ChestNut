using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compressor : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    public float ratio;
    public int effect;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player_model")
        {
            // Debug.Log("Area Effected");
            player.detached_change_radius(ratio * Time.deltaTime);
            player.triger_effect(effect);
        }
    }
}


