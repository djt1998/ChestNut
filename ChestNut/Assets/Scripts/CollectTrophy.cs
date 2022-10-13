using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTrophy : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate() {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player_model"){
            Debug.Log("Trigger Win");
            GameMenu.IsWon = true;
            // Destroy(this.gameObject);
        }
    }
}
