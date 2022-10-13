using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int order;
    public string explanation;

    private void Awake() {
        TutorialManager.Instance.tutorials.Add(this);
    }

    public virtual void IsOnGoing() { }
}
