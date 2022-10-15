using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Eat_Up_All_Cubes : Tutorial
{
    public List<Transform> objectTransforms = new List<Transform>();
    public int cubeType;   // 0: red, 1: blue
    public GameObject arrow ;
    public Vector3 offset = new Vector3(0, 1, 0);
    private List<GameObject> arrows = new List<GameObject>();
    private bool needInit = true;

    // Start is called before the first frame update
    public override void IsOnGoing()
    {
        if (needInit) {
            init();
        }

        for (int i  = 0; i < objectTransforms.Count; i++) {
            if (objectTransforms[i] == null) {
                // if (cubeType == 0) {
                //     setExplanation("You are getting heavier but still not enough to push away the heavy block.\nPlease eat more blue cubes.");
                // }
                // else if (cubeType == 1) {
                //     setExplanation("You are getting smaller but still not enough to go through the gap.\nPlease eat more red cubes.");
                // }
                objectTransforms.RemoveAt(i);
                Destroy(arrows[i]);
                arrows.RemoveAt(i);
                break;
            }
        }
        if (objectTransforms.Count == 0) {
            TutorialManager.Instance.CompleteTutorial();
        }
    }

    private void init() {
        foreach(Transform tf in objectTransforms) {
            if (tf != null) {
                GameObject cloneArrow = Instantiate(arrow, tf.position + offset, tf.rotation);
                cloneArrow.GetComponent<ArrowLook>().SetTarget(tf);
                arrows.Add(cloneArrow);
            }
        }
        needInit = false;
    }
}
