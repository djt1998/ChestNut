using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Stay_In_Red_Area : Tutorial
{
    public GameObject player;
    public float maxSize = 0.75f;
    public override void IsOnGoing()
    {
        if (player.GetComponent<Player>().getRadius() < maxSize) {
            TutorialManager.Instance.CompleteTutorial();
        }
    }
}
