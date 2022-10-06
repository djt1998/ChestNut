using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Transform item;
    private Player player;
    public float ratio;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(item != null)
        {
            item.transform.Rotate(0.0f, 0.8f, 0.0f, Space.Self);
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player"){
            Debug.Log("Key +1");
            Destroy(this.gameObject);
            player.keyStatus += 1;
            // other.gameObject.transform.localScale = new Vector3(1,1,1);
        }
    }
}
