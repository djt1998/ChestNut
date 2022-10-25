using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowFPS : MonoBehaviour
{
    public TextMeshProUGUI textFPS;
    private float deltaTime;

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        textFPS.text = "FPS: " + Mathf.Ceil(1f / deltaTime);
    }
}
