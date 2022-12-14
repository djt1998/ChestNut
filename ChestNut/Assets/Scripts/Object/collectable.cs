using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectable : MonoBehaviour
{
    public Transform item;
    public GameObject destroyEffect;
    private Player player;
    public float ratio;
    public Transform miniMapIcon;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(item != null)
        {
            item.transform.Rotate(0.0f, 0.05f, 0.0f, Space.Self);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player_model"){
            Debug.Log("Trigger");
            if (player.change_radius(ratio)) {
                if (this.gameObject.name == "Red Cube") {
                    GameMenu.sendData("item-redcube");
                }
                else if (this.gameObject.name == "Blue Cube") {
                    GameMenu.sendData("item-bluecube");
                }
                SoundEffectManger.PlaySound("CollectCube");
                Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
            // other.gameObject.transform.localScale = new Vector3(1,1,1);
        }
    }

    private void OnDestroy() {
        Destroy(miniMapIcon.gameObject);
    }

    // public void Interaction() {
    //     if (this.gameObject.name == "Blue Cube" || this.gameObject.name == "Red Cube") {
    //         player.change_radius(ratio);
    //     }
    // }
}
