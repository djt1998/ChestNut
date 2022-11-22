using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private bool unlocked;
    public GameObject[] stars;
    public Image lockImage;
    public Sprite star;
    public GameObject canvas;
    public Animator transition;
    public float transitionTime = 1f;
    private int levelIndex;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLevelStatus();
        UpdateLevelImage();
        canvas = GameObject.Find("Canvas");
    }

    private void UpdateLevelStatus()
    {
        string[] subs = gameObject.name.Split('_');
        levelIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + subs[1] + "/" + subs[2] + ".unity");
        if (levelIndex == 1 || (levelIndex > 1 && levelIndex <= 4 && PlayerPrefs.GetInt("Lv1") > 0) || PlayerPrefs.GetInt("Lv" + (levelIndex - 1).ToString()) > 0) {
            unlocked = true;
        }
        unlocked = true;
    }

    private void UpdateLevelImage()
    {
        if(!unlocked) {
            lockImage.gameObject.SetActive(true);
            for(int i = 0; i < stars.Length; i++) {
                stars[i].gameObject.SetActive(false);
            }
            GetComponent<Button>().interactable = false;
        }
        else {
            lockImage.gameObject.SetActive(false);
            for (int i = 0; i < stars.Length; i++) {
                stars[i].gameObject.SetActive(true);
            }

            for(int i = 0; i < PlayerPrefs.GetInt("Lv" + levelIndex); i++) {
                stars[i].gameObject.GetComponent<Image>().sprite = star;
            }
        }
    }

    public void PlayGame_Level () {  // Button_"Level_X"
        GlobalData.FirstTimeEnterMenu = false;
        if (transition == null && canvas != null) {
            transition = canvas.GetComponent<Animator>();
        }
        if (transition != null) {
            StartCoroutine(LoadLevel(levelIndex));
            StartCoroutine(AsyncLoadLevel(levelIndex));
        }
        else {
            SceneManager.LoadScene(levelIndex);
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
}
