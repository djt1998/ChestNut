using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSpin : MonoBehaviour
{
    public GameObject parent;
    private Vector3 scale = new Vector3(6.66f, 6.66f, 6.66f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!parent.GetComponent<StartLogo>().closeEnough) {
            transform.Rotate(0.0f, 2f, 0.0f, Space.Self);
            scale.x *= 0.999f;
            scale.y *= 0.999f;
            scale.z *= 0.999f;
            transform.localScale = scale;
        }
        else {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
