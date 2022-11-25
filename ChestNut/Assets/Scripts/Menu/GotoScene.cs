using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GotoScene : MonoBehaviour
{
    [SerializeField]
    private int levelIndex = -1;
    public string levelName;
    public Animator transition;
    public float transitionTime = 1f;

    void Start() {
        if (levelIndex < 0) {
            levelIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + levelName + ".unity");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player_model"){
            PlayerPrefs.SetInt("uLevel", Math.Min(Math.Max(PlayerPrefs.GetInt("uLevel"), levelIndex + 1), GlobalData.MAX_LEVEL));
            int currentStarsNum = 1;
            currentStarsNum += Math.Min(FindObjectOfType<Player>().logoStatus, 2);
            if(currentStarsNum > PlayerPrefs.GetInt("Lv" + levelIndex))
            {
                PlayerPrefs.SetInt("Lv" + levelIndex, currentStarsNum);
            }
            if (transition != null) {
                StartCoroutine(LoadLevel(levelIndex));
            }
            else {
                SceneManager.LoadScene(levelIndex);
            }
        }
    }

    IEnumerator LoadLevel(int level) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        // SceneManager.LoadScene(level);
        yield return StartCoroutine(AsyncLoadLevel(level));
    }

    IEnumerator AsyncLoadLevel(int level) {
        yield return null;
        TextMeshProUGUI loadingProgress = transform.Find("TransitionUI/LoadingProgress").GetComponent<TextMeshProUGUI>();

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(level);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            if (loadingProgress) {
                loadingProgress.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";
            }

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
}
