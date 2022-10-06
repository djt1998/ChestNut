using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTrophy : MonoBehaviour
{
    public Transform item;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(item != null)
        {
            item.transform.Rotate(0.0f, 0.05f, 0.0f, Space.Self);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player"){
            Debug.Log("Trigger Win");
            GameMenu.IsWon = true;
            Destroy(this.gameObject);
        }
    }
}
