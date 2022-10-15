using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Confine_Ball : Tutorial
{
    public override void IsOnGoing()
    {
        BoxCollider[] colliders = GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < colliders.Length; i++) {
            colliders[i].enabled = false;
        }
        TutorialManager.Instance.CompleteTutorial();
    }
}
