using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Eat_Up_All_Cubes : Tutorial
{
    public List<Transform> objectTransforms = new List<Transform>();
    public int cubeType;   // 0: red, 1: blue
    // Start is called before the first frame update
    public override void IsOnGoing()
    {
        for (int i  = 0; i < objectTransforms.Count; i++) {
            if (objectTransforms[i] == null) {
                if (cubeType == 0) {
                    setExplanation("You are getting heavier but still not enough to push away the heavy block.\nPlease eat more blue cubes.");
                }
                else if (cubeType == 1) {
                    setExplanation("You are getting smaller but still not enough to go through the gap.\nPlease eat more red cubes.");
                }
                objectTransforms.RemoveAt(i);
                break;
            }
        }
        if (objectTransforms.Count == 0) {
            TutorialManager.Instance.CompleteTutorial();
        }
    }
}
