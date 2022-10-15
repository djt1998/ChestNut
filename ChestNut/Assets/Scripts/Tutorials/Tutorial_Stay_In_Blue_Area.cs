using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Stay_In_Blue_Area : Tutorial
{
    public GameObject player;
    public float minWeight = 8f;
    public override void IsOnGoing()
    {
        if (player.GetComponent<Player>().rb.mass > minWeight) {
            TutorialManager.Instance.CompleteTutorial();
        }
    }
}
