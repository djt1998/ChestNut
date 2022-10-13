using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Move_To_Cube : Tutorial
{
    private bool isCurrentTutorial = false;
    public Transform objectTransform;

    public override void IsOnGoing()
    {
        isCurrentTutorial = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (!isCurrentTutorial) {
            return;
        }

        if (other.transform == objectTransform) {
            TutorialManager.Instance.CompleteTutorial();
            isCurrentTutorial = false;
        }
    }
}
