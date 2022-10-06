using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject[] cameras;
    public string[] shotcuts;
    // public bool change = true;
    void Start() {
        Switch(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < cameras.Length; i++) {
            if (Input.GetKey(shotcuts[i])) {
                Switch(i);
            }
        }
    }

    void Switch(int index) {
        for (int i = 0; i < cameras.Length; i++) {
            if (i != index) {
                // if (change) {
                //     cameras[i].GetComponent<AudioListener>().enabled = false;
                // }
                cameras[i].GetComponent<Camera>().enabled = false;
            }
            else {
                // if (change) {
                //     cameras[i].GetComponent<AudioListener>().enabled = true;
                // }
                cameras[i].GetComponent<Camera>().enabled = true;
            }
        }
    }
}
