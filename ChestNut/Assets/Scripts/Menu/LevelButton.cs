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
        levelIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + gameObject.name.Substring(7) + ".unity");
        if (levelIndex < 0) {
            levelIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Tutorial/" + gameObject.name.Substring(7) + ".unity");
        }
        if (levelIndex == 1 || (levelIndex > 1 && levelIndex <= 4 && PlayerPrefs.GetInt("Lv1") > 0) || PlayerPrefs.GetInt("Lv" + (levelIndex - 1).ToString()) > 0)
        {
            unlocked = true;
        }
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
        }
        else {
            SceneManager.LoadScene(levelIndex);
        }
    }

    IEnumerator LoadLevel(int level) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(level);
    }
}
