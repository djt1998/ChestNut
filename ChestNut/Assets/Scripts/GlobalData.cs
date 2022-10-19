using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GlobalData
{
    // readonly
    public static readonly string SESSION_ID;
    public static readonly string VERSION = "Chestnet_version_2_0_0";
    public static readonly int MAX_LEVEL = 5;

    public static bool FirstTimeEnterMenu = true;
    // public static readonly int MAX_NUM_CHECKPOINT_PER_LEVEL = 4;
    // public static readonly string URL = "https://docs.google.com/forms/d/e/1FAIpQLSePz3EsxIRK0KUICpWOA31I30ossPnruJ_Zai7Nz78bydreAA/formResponse";

    // game statistics
    // public static int currLevel = 0;
    // public static int[] levelStartTimes = new int[MAX_LEVEL + 1];
    // public static int[] levelDeathTimes = new int[MAX_LEVEL + 1];
    // public static int[] levelAccomplishTimes = new int[MAX_LEVEL + 1];
    // public static int[] levelRestartTimes = new int[MAX_LEVEL + 1];
    // public static int time = 0;
    // public static int[] checkpoint = new int[MAX_LEVEL + 1];
    // public static int numBlueCubes = 0;
    // public static int numRedCubes = 0;
    // public static Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();

    static GlobalData() {
        // check version; delete all cache
        if (!PlayerPrefs.HasKey("version") || PlayerPrefs.GetString("version") != GlobalData.VERSION) {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("version", GlobalData.VERSION);
            Debug.Log("New Version Released! Delete All Cache ...\nVersion: " + GlobalData.VERSION);
        }
        // playerpref
        // unlocked_level
        if (!PlayerPrefs.HasKey("uLevel")) {
            PlayerPrefs.SetInt("uLevel", 1);
        }
        // Session_ID
        if (!PlayerPrefs.HasKey("sessionID")) {
            SESSION_ID = DateTime.Now.Ticks.ToString().Substring(0, 15);
            PlayerPrefs.SetString("sessionID", SESSION_ID);
        }
        else {
            SESSION_ID = PlayerPrefs.GetString("sessionID");
        }
    }

    public static void getInfo() {
        Debug.Log("Session ID: " + SESSION_ID + "\nLevel At: " + PlayerPrefs.GetInt("uLevel"));
    }
}
