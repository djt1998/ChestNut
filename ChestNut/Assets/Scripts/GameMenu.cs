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
    public GameObject PauseMenuUI;
    public GameObject SettingsMenuUI;

    public GameObject WinMenuUI;
    public GameObject LoseMenuUI;
    public GameObject InGameUI;

    void Start() {
        // GlobalData.levelStartTimes[SceneManager.GetActiveScene().buildIndex]++;  // start++
        // InGameUI.SetActive(true);
        // if (IsRestart) {
        //     sendData("restart");
        //     IsRestart = false;
        // }
        // else {
        //     sendData("start");
        // }
        sendData("start");
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
        // else if (Input.GetKey(KeyCode.P) && !GameIsPaused) {
        //     Pause();
        // }
        // else if (Input.GetKeyDown(KeyCode.P))
        // {
        //     if (GameIsPaused) {
        //         Resume();
        //     }
        //     else {
        //         showSettings();
        //     }
        // }
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
        // if (Input.GetKeyDown(KeyCode.P) && GameIsPaused && !IsWon && !IsDead)
        // {
        //     Resume();
        // }
    }
    private void showSettings()
    {
        SettingsMenuUI.SetActive(true);
        InGameUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        sendData("settings");
    }
    private void Pause(){
        PauseMenuUI.SetActive(true);
        InGameUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        sendData("pause");
    }

    private void win() {
        WinMenuUI.SetActive(true);
        InGameUI.SetActive(false);
        // StartMenu.unlocked_level = Math.Min(Math.Max(StartMenu.unlocked_level, SceneManager.GetActiveScene().buildIndex + 1), GlobalData.MAX_LEVEL);  // unlock level
        // Debug.Log("level: " + StartMenu.unlocked_level);
        PlayerPrefs.SetInt("uLevel", Math.Min(Math.Max(PlayerPrefs.GetInt("uLevel"), SceneManager.GetActiveScene().buildIndex + 1), GlobalData.MAX_LEVEL));  // unlock level
        Debug.Log("level: " + PlayerPrefs.GetInt("uLevel"));
        Time.timeScale = 0f;
        float totTime = FindObjectOfType<TimerManager>().getTotalTime();    // display totTime to finish this level
        GameObject.Find("WinMenu/Score").GetComponentInChildren<TMP_Text>().text = "Scores: " + TimerManager.getFormatTime(totTime);
        // GlobalData.levelAccomplishTimes[SceneManager.GetActiveScene().buildIndex]++;  // win++
        // sendData
        sendData("success");
    }

    private void lose() {
        LoseMenuUI.SetActive(true);
        InGameUI.SetActive(false);
        Time.timeScale = 0f;
        // GlobalData.levelDeathTimes[SceneManager.GetActiveScene().buildIndex]++;  // death++
        sendData("death");
    }

    public void Resume(){
        PauseMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        InGameUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        sendData("resume");
    }

    public void NextLevel() {
        Debug.Log("next level");
        init();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        // GlobalData.levelRestartTimes[SceneManager.GetActiveScene().buildIndex]++;  // restart++
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void init() {  // init some static variables
        Time.timeScale = 1f;
        GameIsPaused = false;
        IsWon = false;
        IsDead = false;
    }
    
    public static void sendData(String tag) {
        // Debug.Log("time1: " + DateTime.Now.Ticks.ToString());
        // DataSenderController sendObject = FindObjectOfType<DataSenderController>();
        string _tag = "lv" + (SceneManager.GetActiveScene().buildIndex - 1) + "-" + tag;
        string _time = DateTime.Now.Ticks.ToString().Substring(0, 15);
        FindObjectOfType<DataSenderController>().Send(_tag, _time);
        // Debug.Log("time2:" + DateTime.Now.Ticks.ToString());
    }
}