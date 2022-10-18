using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLogo : MonoBehaviour
{
    private Vector3 startPoint = new Vector3(-8.444445f + 10f, -3.777778f, -10);
    private Vector3 destination = new Vector3(-8.444445f, -3.777778f, 0);
    public bool closeEnough = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPoint;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, destination) > 0.5f) {
            transform.Translate(new Vector3(-1, 0, 1) * Time.deltaTime);
        }
        else {
            transform.position = destination;
            closeEnough = true;
        }
    }
}
