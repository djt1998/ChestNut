using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Block : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player_model")
        {
            GameMenu.IsDead = true;
        }
    }
}
