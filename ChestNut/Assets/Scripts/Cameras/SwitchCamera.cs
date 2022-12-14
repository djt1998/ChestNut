using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchCamera : MonoBehaviour
{
    public GameObject[] cameras;
    public string[] shotcuts;
    public bool change = true;
    private Animation levelPreview;
    private bool switch_enabled = true;
    private TextMeshProUGUI textPressAnyKeyToSkip;
    void Start() {
        Switch(0);
        textPressAnyKeyToSkip = GameObject.Find("Canvas").transform.Find("InGameDisplay/PressAnyKeyToSkip").GetComponent<TextMeshProUGUI>();
        textPressAnyKeyToSkip.enabled = false;
        levelPreview = cameras[0].GetComponent<Animation>();
        if (levelPreview != null)  {
            levelPreview.Play();
            StartCoroutine(Preview((levelPreview.clip.length)));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < cameras.Length; i++) {
            if (switch_enabled && Input.GetKey(shotcuts[i])) {
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
                if (cameras[i].GetComponent<Follower>() != null) {
                    cameras[i].GetComponent<Follower>().recoverObstructions();
                    cameras[i].GetComponent<Follower>().is_active = false;
                }
            }
            else {
                if (change) {
                    cameras[i].GetComponent<AudioListener>().enabled = true;
                }
                cameras[i].GetComponent<Camera>().enabled = true;
                if (cameras[i].GetComponent<Follower>() != null) {
                    cameras[i].GetComponent<Follower>().is_active = true;
                }
            }
        }
    }

    IEnumerator Preview(float countDown) {
        textPressAnyKeyToSkip.enabled = true; // = "Press Any Key To Skip";
        float alpha = textPressAnyKeyToSkip.alpha;
        float beta = -1f;
        // TextMeshProUGUI textInstructions = GameObject.Find("Canvas").transform.Find("InGameDisplay/Instructions").GetComponent<TextMeshProUGUI>();
        // textInstructions.text = "";
        Player player = FindObjectOfType<Player>();
        TimerManager TM = FindObjectOfType<TimerManager>();
        player.enabled = false;
        TM.enabled = false;
        switch_enabled = false;
        for (int i = 0; i < cameras.Length; i++) {
            if (cameras[i].GetComponent<Follower>() != null) {
                cameras[i].GetComponent<Follower>().lockCamera();
                cameras[i].GetComponent<Follower>().enabled = false;
            }
        }

        while (countDown >= 0f) {
            if (Input.anyKey && !(Input.GetKey(KeyCode.P) || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2)) && !GameMenu.GameIsPaused) {
                levelPreview.Stop();
                break;
            }
            countDown -= Time.deltaTime;
            if (textPressAnyKeyToSkip.alpha < 0.3f || textPressAnyKeyToSkip.alpha > alpha) {
                beta = -beta;
            }
            textPressAnyKeyToSkip.alpha += beta * Time.deltaTime;
            yield return null;
        }
        for (int i = 0; i < cameras.Length; i++) {
            if (cameras[i].GetComponent<Follower>() != null) {
                cameras[i].GetComponent<Follower>().enabled = true;
            }
        }
        textPressAnyKeyToSkip.enabled = false;
        textPressAnyKeyToSkip.alpha = alpha;
        // textInstructions.text = "Settings: P";
        yield return new WaitForSeconds(0.5f);

        player.enabled = true;
        TM.enabled = true;
        switch_enabled = true;
        for (int i = 0; i < cameras.Length; i++) {
            if (cameras[i].GetComponent<Follower>() != null) {
                cameras[i].GetComponent<Follower>().unlockCamera();
            }
        }
    }
}
