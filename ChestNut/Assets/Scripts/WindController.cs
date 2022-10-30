using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    public float force_coef;

    // Start is called before the first frame update
    void Start()
    {
        if (force_coef == 0) { force_coef = 200; }
        SetWindForce(force_coef);
    }

    public void SetWindForce(float force) {
        Wind[] winds = GetComponentsInChildren<Wind>();
        foreach (Wind wind in winds) {
            wind.force_coef = force;
        }
    }
}
