using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpackLargeCube : MonoBehaviour
{
    public GameObject destroyEffect;
    public Transform miniMapIcon;
    public GameObject[] respawnPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player_model"){
            GameMenu.sendData("item-largecube");
            Instantiate(destroyEffect, transform.position, transform.rotation);
            if (respawnPrefabs.Length > 0) {
                int idx = Random.Range(0, respawnPrefabs.Length);
                Instantiate(respawnPrefabs[idx], transform.position, respawnPrefabs[idx].transform.rotation);
            }
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy() {
        Destroy(miniMapIcon.gameObject);
    }
}
