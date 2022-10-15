using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Stay_In_Red_Area : Tutorial
{
    public GameObject player;
    public float maxSize = 0.75f;
    public GameObject arrow;
    public Transform target;
    public Vector3 offset = new Vector3(-1, 1, 0);
    private GameObject cloneArrow;
    private bool needInit = true;

    public override void IsOnGoing()
    {
        if (needInit) {
            cloneArrow = Instantiate(arrow, target.position + offset, target.rotation);
            cloneArrow.GetComponent<ArrowLook>().SetTarget(target);
            needInit = false;
        }

        if (player.GetComponent<Player>().getRadius() < maxSize) {
            Destroy(cloneArrow);
            TutorialManager.Instance.CompleteTutorial();
        }
    }
}
