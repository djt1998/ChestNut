using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Key : Tutorial
{
    public List<string> keys = new List<string>();

    public override void IsOnGoing()
    {
        for (int i = 0; i < keys.Count; i++) {
            if (Input.GetKey(keys[i])) {
                keys.RemoveAt(i);
                break;
            }
        }
        if (keys.Count == 0) {
            TutorialManager.Instance.CompleteTutorial();
        }
    }
}
