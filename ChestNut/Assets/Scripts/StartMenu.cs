using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenu : MonoBehaviour
{
    // public static readonly int MAX_LEVEL = 4;
    // public static int unlocked_level = 1;
    public Button[] level_buttons;

    private void Awake() 
    {   
    	if (GlobalData.SESSION_ID == null) {
            Debug.Log("Session Error!");
        }
        else {
            GlobalData.getInfo();
        }
    }

    public void Start() {
        for (int i = 0; i < level_buttons.Length; i++) {
            // if (i + 1 > unlocked_level) {
            if (i + 1 > PlayerPrefs.GetInt("uLevel")) {
                level_buttons[i].interactable = false;
                level_buttons[i].GetComponentInChildren<TMP_Text>().text = "Locked";
            }
        }
    }

    public void PlayGame_Level (GameObject obj) {  // Button_"Level_X"
        SceneManager.LoadScene(obj.name.Substring(7));
    }
}
