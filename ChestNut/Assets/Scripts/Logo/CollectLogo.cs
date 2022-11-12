using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectLogo : MonoBehaviour
{
    public Transform item;
    public GameObject destroyEffect;
    // public GameObject absorbEffect;
    private Player player;
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
            item.transform.Rotate(0.0f, 0.1f, 0.0f, Space.Self);
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player_model"){
            Debug.Log("Collect Logo!");
            player.logoStatus += 1;
            GameMenu.sendData("logo");
            SoundEffectManger.PlaySound("CollectLogo");
            Instantiate(destroyEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            // StartCoroutine(collectLogo(1));
            // GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnDestroy() {
        Destroy(miniMapIcon.gameObject);
    }

    // IEnumerator collectLogo(int countDown) {
    //     Instantiate(destroyEffect, transform.position, transform.rotation);
    //     yield return new WaitForSeconds(countDown);
    //     Instantiate(absorbEffect, player.transform.position, player.transform.rotation);
    //     Destroy(gameObject);
    // }
}
