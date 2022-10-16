using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public static MusicPlayer instacne;

    private void Awake() {
        if (instacne == null) {
            DontDestroyOnLoad(gameObject);
            instacne = this;
        }
        else if (instacne != this) {
            Destroy(gameObject);
        }
    }
}
