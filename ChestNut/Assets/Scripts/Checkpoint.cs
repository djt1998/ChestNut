using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool IsOn = true;
    public void CheckpointSet() {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (Checkpoint cp in checkpoints) {
            cp.IsOn = true;
        }
        IsOn = false;
    }
    private void OnTriggerEnter(Collider other) {
        if (IsOn && other.name == "Player_model") {
            Debug.Log("Checkpoint: cp-" + this.gameObject.name);
            CheckpointSet();
            GameMenu.sendData("cp-" + this.gameObject.name);
        }
    }
}
