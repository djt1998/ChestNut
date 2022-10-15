using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Any_Key : Tutorial
{
    public override void IsOnGoing()
    {
        if (Input.anyKey) {
            TutorialManager.Instance.CompleteTutorial();
        }
    }
}
