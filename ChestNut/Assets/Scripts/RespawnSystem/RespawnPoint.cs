using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public int id;
    public bool IsOn = false;
    private List<Transform> storedObject = new List<Transform>();
    private RespawnManager RM;

    // Start is called before the first frame update
    void Start()
    {
        RM = FindObjectOfType<RespawnManager>();
    }

    public void destroyAllGameObject() {
        foreach (Transform tf in storedObject) {
            Destroy(tf.gameObject);
        }
        storedObject.Clear();
    }

    public void storeGameObject(Transform other) {
        storedObject.Add(other);
        // other.gameObject.SetActive(false);
    }

    public void restoreAllGameOnject() {
        foreach (Transform tf in storedObject) {
            tf.gameObject.SetActive(true);
        }
        storedObject.Clear();
    }

    private void OnTriggerEnter(Collider other) {
        if (!IsOn && other.name == "Player_model") {
            RM.SetRespawnPointsActive(id);
            // GameMenu.sendData("rp-" + this.gameObject.name);
        }
    }
}
