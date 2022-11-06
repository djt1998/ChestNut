using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool IsWon = false;
    public static bool IsDead = false;

    // public static bool IsRestart = false;
    public GameObject SettingsMenuUI;

    public GameObject WinMenuUI;
    public GameObject LoseMenuUI;
    public GameObject InGameUI;

    private int levelIndex;

    void Start() {
        sendData("start");
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsWon) {
            win();
        }
        else if (IsDead) {
            lose();
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P) && !IsWon && !IsDead)
        {
            if (GameIsPaused) {
                Resume();
            }
            else {
                showSettings();
            }
        }
    }
    public void showSettings()
    {
        SettingsMenuUI.SetActive(true);
        InGameUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        sendData("settings");
    }

    private void win() {
        WinMenuUI.SetActive(true);
        InGameUI.SetActive(false);
        PlayerPrefs.SetInt("uLevel", Math.Min(Math.Max(PlayerPrefs.GetInt("uLevel"), levelIndex + 1), GlobalData.MAX_LEVEL));  // unlock level
        Debug.Log("level: " + PlayerPrefs.GetInt("uLevel"));
        // update star and level
        int currentStarsNum = 1;
        currentStarsNum += Math.Min(FindObjectOfType<Player>().logoStatus, 2);
        if(currentStarsNum > PlayerPrefs.GetInt("Lv" + levelIndex))
        {
            PlayerPrefs.SetInt("Lv" + levelIndex, currentStarsNum);
        }

        Time.timeScale = 0f;
        float totTime = FindObjectOfType<TimerManager>().getTotalTime();    // display totTime to finish this level
        GameObject.Find("WinMenu/Score").GetComponentInChildren<TMP_Text>().text = "Scores: " + TimerManager.getFormatTime(totTime);
        // sendData
        sendData("success");
    }

    private void lose() {
        LoseMenuUI.SetActive(true);
        InGameUI.SetActive(false);
        Time.timeScale = 0f;
        sendData("death");
    }

    public void Resume(){
        SettingsMenuUI.SetActive(false);
        InGameUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        sendData("resume");
    }

    public void NextLevel() {
        Debug.Log("next level");
        init();
        SceneManager.LoadScene(levelIndex + 1);
    }

    public void LoadMenu(){
        sendData("quit");
        Debug.Log("loading menu");
        init();
        SceneManager.LoadScene(0);
    }

    public void Restart() {
        sendData("quit");
        // IsRestart = false;
        Debug.Log("restart game");
        init();
        SceneManager.LoadScene(levelIndex);
    }

    private void init() {  // init some static variables
        Time.timeScale = 1f;
        GameIsPaused = false;
        IsWon = false;
        IsDead = false;
    }
    
    public static void sendData(String tag) {
        // Debug.Log("time1: " + DateTime.Now.Ticks.ToString());
        DataSenderController sendObject = FindObjectOfType<DataSenderController>();
        if (sendObject) {
            string _tag = "lv" + (SceneManager.GetActiveScene().buildIndex - 1) + "-" + tag;
            string _time = DateTime.Now.Ticks.ToString().Substring(0, 15);
            sendObject.Send(_tag, _time);
        }
        // Debug.Log("time2:" + DateTime.Now.Ticks.ToString());
    }
}