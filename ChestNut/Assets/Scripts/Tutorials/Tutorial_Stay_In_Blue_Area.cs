using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Stay_In_Blue_Area : Tutorial
{
    public GameObject player;
    public float minWeight = 8f;
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

        if (player.GetComponent<Player>().rb.mass > minWeight) {
            Destroy(cloneArrow);
            TutorialManager.Instance.CompleteTutorial();
        }
    }
}
