using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] private Camera canvasCamera;
    public GameObject clickEffect;
    public GameObject trailEffect;
    public Vector3 offset = new Vector3(0.5f, -0.5f, 0);
    private float nextTrailSpawn = 0.1f;
    private float nextTimeClick = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        // Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        nextTimeClick -= Time.deltaTime;
        nextTrailSpawn -= Time.deltaTime;
        Vector3 mouseWorldPosition = canvasCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = -0.1f;
        transform.position = mouseWorldPosition;
        if (nextTimeClick <= 0f && Input.GetMouseButton(0)) {
            nextTimeClick = 0.2f;
            Instantiate(clickEffect, transform.position, clickEffect.transform.rotation);
        }
        if (nextTrailSpawn <= 0f) {
            nextTrailSpawn = 0.1f;
            Instantiate(trailEffect, transform.position + offset, trailEffect.transform.rotation);
        }
    }
}
