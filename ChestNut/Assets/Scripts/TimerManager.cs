using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;   // text for display

    [Header("Timer Settings (max 500)")]
    public float maxTime;       // max time for count down

    [Header("Limit Settings")]
    public float timerLimit;    // time for bg color change
    public float deathTime;     // time for death

    private bool countdown = true;

    private Image img;              // parameters for bg color change
    private float alpha = 0.01f;
    private bool increase = true;

    private float currTime;

    // Start is called before the first frame update
    void Start()
    {
        // enabled = false;
        currTime = maxTime = 0f;
        countdown = false;
        displayTime();
        img = GameObject.Find("InGameDisplay").GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currTime += Time.deltaTime;     // countdown

        if (currTime <= deathTime) {    // game over, stop watch
            currTime = deathTime;
            GameMenu.IsDead = true;
            enabled = false;
        }
        else if (currTime <= timerLimit && countdown) { // change bg color
            displayTime();
            timerText.fontSize = 40;
            timerText.color = Color.red;
            // change panel image cover
            Color c = img.color;
            alpha = increase ? alpha + 0.01f : alpha - 0.01f;
            if (alpha > 0.5) {
                increase = false;
            }
            if (alpha < 0.1) {
                increase = true;
            }
            c.a = alpha;
            img.color = c;
        }
        else {
            displayTime();
        }
    }

    private void displayTime() {
        timerText.text = "TIME: " + getFormatTime(currTime);
    }

    public float getFinishTime() {  // get finishing time
        return currTime;
    }

    public float getTotalTime() {   // total time for finish this level
        return Mathf.Abs(maxTime - currTime);
    }

    public static string getFormatTime(float time) {  // in seconds
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float hundreths = (time % 1) * 100;
        return string.Format("{0:00} : {1:00}.{2:00}", minutes, seconds, hundreths);
    }
}
