using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public List<Tutorial> tutorials = new List<Tutorial>();
    public Text explanationText;
    private static TutorialManager instance;
    private Tutorial currentTutorial;
    public static TutorialManager Instance {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<TutorialManager>();
            }

            if (instance == null) {
                Debug.Log("Tutorial Manager Not Found!");
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetNextTutorial(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentTutorial) {
            currentTutorial.IsOnGoing();
        }
    }

    public void CompleteTutorial() {
        explanationText.text = "Task " + currentTutorial.order + " Accomplished!";
        SetNextTutorial(currentTutorial.order + 1);
    }

    public void SetNextTutorial(int currentOrder) {
        currentTutorial = GetTutorialByOrder(currentOrder);
        if (!currentTutorial) {
            AllTutorialsAccomplished();
            return;
        }
        explanationText.text = currentTutorial.explanation;
    }

    public void AllTutorialsAccomplished() {
        explanationText.text = "All Tutorials Accomplished!";
    }

    public Tutorial GetTutorialByOrder(int order) {
        for (int i = 0; i < tutorials.Count; i++) {
            if (tutorials[i].order == order) {
                return tutorials[i];
            }
        }
        return null;
    }
}
