using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Move_To_Cube : Tutorial
{
    private bool isCurrentTutorial = false;
    public Transform objectTransform;
    public GameObject arrow;
    public Transform target;
    public Vector3 offset = new Vector3(-1, 1, 0);
    private GameObject cloneArrow;
    private bool needInit = true;

    public override void IsOnGoing()
    {
        isCurrentTutorial = true;
        if (needInit) {
            cloneArrow = Instantiate(arrow, target.position + offset, target.rotation);
            cloneArrow.GetComponent<ArrowLook>().SetTarget(target);
            needInit = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!isCurrentTutorial) {
            return;
        }

        if (other.transform == objectTransform) {
            TutorialManager.Instance.CompleteTutorial();
            Destroy(cloneArrow);
            isCurrentTutorial = false;
        }
    }
}
