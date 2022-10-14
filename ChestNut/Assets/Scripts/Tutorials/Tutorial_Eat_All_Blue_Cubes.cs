using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Eat_All_Blue_Cubes : Tutorial
{
    public List<Transform> objectTransforms = new List<Transform>();
    // Start is called before the first frame update
    public override void IsOnGoing()
    {
        for (int i  = 0; i < objectTransforms.Count; i++) {
            if (objectTransforms[i] == null) {
                setExplanation("You are getting heavier but still not enough to push away the heavy block.\nPlease eat more blue cubes.");
                objectTransforms.RemoveAt(i);
                break;
            }
        }
        if (objectTransforms.Count == 0) {
            TutorialManager.Instance.CompleteTutorial();
        }
    }
}
