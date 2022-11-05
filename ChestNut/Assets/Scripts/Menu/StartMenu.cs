using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenu : MonoBehaviour
{
    // public Button[] level_buttons;

    private void Awake() 
    {   
    	if (GlobalData.SESSION_ID == null) {
            Debug.Log("Session Error!");
        }
        else {
            GlobalData.getInfo();
        }
    }

    // public void Start() {
    //     for (int i = 1; i < level_buttons.Length; i++) {
    //         if (2 > PlayerPrefs.GetInt("uLevel")) {
    //             level_buttons[i].interactable = false;
    //             level_buttons[i].GetComponentInChildren<TMP_Text>().text = "Locked";
    //         }
    //     }
    // }

    public void PlayGame_Level (GameObject obj) {  // Button_"Level_X"
        GlobalData.FirstTimeEnterMenu = false;
        SceneManager.LoadScene(obj.name.Substring(7));
    }
}
