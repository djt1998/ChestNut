using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int order;
    [TextArea(3, 10)]
    public string explanation;

    private void Awake() {
        TutorialManager.Instance.tutorials.Add(this);
    }

    public virtual void IsOnGoing() { }

    public void setExplanation(string exp) {
        TutorialManager.Instance.explanationText.text = exp;
    }
}
