using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameDisPlay : MonoBehaviour
{
    // public TextMeshProUGUI textPlayerInfo;
    public TextMeshProUGUI textKeyStatus;
    public TextMeshProUGUI textLogoStatus;
    public GameObject Movement;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        if (GlobalData.controlMode != 1) {
            Movement.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player) {
            // textPlayerInfo.text = string.Format("{0, -10}{1:00}.{2:00}\n{3, -8}{4:00}.{5:00}", "SIZE:", Mathf.FloorToInt(player.radius), (player.radius % 1) * 100, "MASS:", Mathf.FloorToInt(player.rb.mass), (player.rb.mass % 1) * 100);
            textKeyStatus.text = player.keyStatus.ToString();
            textLogoStatus.text = player.logoStatus.ToString();
        }
    }
}
