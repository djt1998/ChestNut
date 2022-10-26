using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    // public GameObject respawnPrefab;
    public Transform player;
    private RespawnPoint[] respawnPoints;
    private int currActiveRespawnPointID = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (respawnPoints == null) {
            respawnPoints = FindObjectsOfType<RespawnPoint>();
        }
        // sort by id
        // foreach (GameObject rp in  respawnPoints) {
        //     if ()
        // }
    }

    public void SetRespawnPointsActive(int id) {
        respawnPoints[currActiveRespawnPointID].IsOn = false;
        respawnPoints[currActiveRespawnPointID].destroyAllGameObject();
        respawnPoints[id].IsOn = true;
        currActiveRespawnPointID = id;
    }

    public void Respawn() {     // respawn the player at currentActiveRespawnPoint
        
    }
}
