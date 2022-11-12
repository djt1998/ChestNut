using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MassBar : MonoBehaviour
{
    public float MIN_SIZE;
    public float MAX_SIZE;
    public float criticalPoint;
    public float turningPoint;
    private Image barImage;
    private Player player;

    private void Awake() {
        barImage = transform.Find("Bar").GetComponent<Image>();
        barImage.fillAmount = 0.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player && player.rb.mass >= MIN_SIZE) {
            barImage.color = player.rb.mass <= criticalPoint ? new Color32(255, 102, 102, 255) : new Color32(110, 163, 255, 255);
            barImage.fillAmount = Mathf.Min((player.rb.mass - MIN_SIZE) / (turningPoint - MIN_SIZE) / 2f, (player.rb.mass - turningPoint) / (MAX_SIZE - turningPoint) / 2f + 0.5f);
        }
    }
}
