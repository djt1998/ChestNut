using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenu : MonoBehaviour
{
    private void Awake() 
    {   
    	if (GlobalData.SESSION_ID == null) {
            Debug.Log("Session Error!");
        }
        else {
            GlobalData.getInfo();
        }
    }
}
