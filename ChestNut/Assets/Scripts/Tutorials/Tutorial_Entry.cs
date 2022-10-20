using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Entry : Tutorial
{
    public string key;
    private bool needInit = true;
    private Player player;
    private TimerManager TM;
    public override void IsOnGoing()
    {
        if (needInit) {
            player = FindObjectOfType<Player>();
            player.enabled = false;
            TM = FindObjectOfType<TimerManager>();
            TM.enabled = false;
            needInit = false;
        }
    }

    public override void IsOnGoingUpdate()
    {
        if (Input.GetKeyDown(key)) {
            player.enabled = true;
            TM.enabled = true;
            TutorialManager.Instance.CompleteTutorial();
        }
    }
}
