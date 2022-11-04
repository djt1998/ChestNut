using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoReflectionProbe : MonoBehaviour
{
    public float top;
    public float down;
    public float moveSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y > top) {
            transform.position = new Vector3(transform.position.x, down, transform.position.z);
        }
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
    }
}
