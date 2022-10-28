using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionCell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame_Level (GameObject obj) {  // Button_"Level_X"
        GlobalData.FirstTimeEnterMenu = false;
        SceneManager.LoadScene(obj.name.Substring(7));
    }
}
