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
    private int levelIndex;

    // Start is called before the first frame update
    void Start()
    {
        // if (2 > PlayerPrefs.GetInt("uLevel")) {
        //     levelButton.interactable = false;
        //     lockImage.enabled = true;
        // }
        UpdateLevelStatus();
        UpdateLevelImage();
    }

    private void UpdateLevelStatus()
    {
        levelIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + gameObject.name.Substring(7) + ".unity");
        if (levelIndex < 0) {
            levelIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Tutorial/" + gameObject.name.Substring(7) + ".unity");
        }
        if (levelIndex == 1 || PlayerPrefs.GetInt("Lv" + (levelIndex - 1).ToString()) > 0)
        {
            unlocked = true;
        }
    }

    private void UpdateLevelImage()
    {
        if(!unlocked) {
            lockImage.gameObject.SetActive(true);
            for(int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(false);
            }
        }
        else {
            lockImage.gameObject.SetActive(false);
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(true);
            }

            for(int i = 0; i < PlayerPrefs.GetInt("Lv" + levelIndex); i++)
            {
                stars[i].gameObject.GetComponent<Image>().sprite = star;
            }
        }
    }
}
