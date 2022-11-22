using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool IsWon = false;
    public static bool IsDead = false;
    public GameObject SettingsMenuUI;
    public GameObject WinMenuUI;
    public GameObject LoseMenuUI;
    public GameObject InGameUI;
    public GameObject[] stars;
    public Sprite star;
    public Animator transition;
    public float transitionTime = 1f;

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
        SoundEffectManger.PauseSoundAll();
        sendData("settings");
    }

    private void win() {
        SoundEffectManger.PlaySound("PlayerWin");
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
        for (int i = 0; i < stars.Length && i < currentStarsNum; i++) {
            stars[i].gameObject.GetComponent<Image>().sprite = star;
        }
        float totTime = FindObjectOfType<TimerManager>().getTotalTime();    // display totTime to finish this level
        GameObject.Find("WinMenu/Score").GetComponentInChildren<TMP_Text>().text = "Scores: " + TimerManager.getFormatTime(totTime);
        // sendData
        sendData("success");
    }

    private void lose() {
        SoundEffectManger.PlaySound("PlayerDeath");
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
        SoundEffectManger.UnPauseSoundAll();
        sendData("resume");
    }

    public void NextLevel() {
        Debug.Log("next level");
        init();
        PlayGame_Level(levelIndex + 1);
    }

    public void LoadMenu(){
        sendData("quit");
        Debug.Log("loading menu");
        init();
        PlayGame_Level(0);
    }

    public void Restart() {
        sendData("quit");
        // IsRestart = false;
        Debug.Log("restart game");
        init();
        PlayGame_Level(levelIndex);
    }

    private void init() {  // init some static variables
        Time.timeScale = 1f;
        GameIsPaused = false;
        IsWon = false;
        IsDead = false;
    }

    private void PlayGame_Level(int level) {
        if (transition != null) {
            StartCoroutine(LoadLevel(level));
            StartCoroutine(AsyncLoadLevel(level));
        }
        else {
            SceneManager.LoadScene(level);
        }
    }

    IEnumerator LoadLevel(int level) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        // SceneManager.LoadScene(level);
    }

    IEnumerator AsyncLoadLevel(int level) {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(level);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            // m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                // m_Text.text = "Press the space bar to continue";
                // //Wait to you press the space key to activate the Scene
                // if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
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