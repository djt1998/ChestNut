using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    public TextMeshProUGUI textPlayerInfo;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player) {
            textPlayerInfo.text = string.Format("{0, -10}{1:00}.{2:00}\n{3, -8}{4:00}.{5:00}", "SIZE:", Mathf.FloorToInt(player.getRadius()), (player.getRadius() % 1) * 100, "MASS:", Mathf.FloorToInt(player.rb.mass), (player.rb.mass % 1) * 100);
        }
    }
}
