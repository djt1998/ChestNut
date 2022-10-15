using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Confine_Ball : Tutorial
{
    public override void IsOnGoing()
    {
        GetComponent<SphereCollider>().enabled = false;
            TutorialManager.Instance.CompleteTutorial();
    }
}
