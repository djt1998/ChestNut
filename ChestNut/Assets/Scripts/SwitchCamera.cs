using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject[] cameras;
    public string[] shotcuts;
    public bool change = true;
    private Player player;
    private TimerManager TM;
    private Animation levelPreview;
    void Start() {
        Switch(0);
        levelPreview = cameras[0].GetComponent<Animation>();
        if (levelPreview != null)  {
            StartCoroutine(Preview((int) Mathf.Ceil(levelPreview.clip.length)));
        }
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
                if (change) {
                    cameras[i].GetComponent<AudioListener>().enabled = false;
                }
                cameras[i].GetComponent<Camera>().enabled = false;
            }
            else {
                if (change) {
                    cameras[i].GetComponent<AudioListener>().enabled = true;
                }
                cameras[i].GetComponent<Camera>().enabled = true;
            }
        }
    }

    IEnumerator Preview(int countDown) {
        player = FindObjectOfType<Player>();
        TM = FindObjectOfType<TimerManager>();
        player.GetComponent<Player>().enabled = false;
        TM.enabled = false;
        cameras[0].GetComponent<Follower>().enabled = false;
        yield return new WaitForSeconds(countDown);
        player.GetComponent<Player>().enabled = true;
        TM.enabled = true;
        cameras[0].GetComponent<Follower>().enabled = true;
    }
}