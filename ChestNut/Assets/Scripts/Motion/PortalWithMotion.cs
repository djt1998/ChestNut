using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalWithMotion : MonoBehaviour
{
    public string portal_name;
    public string target_portal_name;
    public ParticleSystem sendingEffect;

    private PortalWithMotion[] portal_list;
    private PortalWithMotion target_portal;
    private int portal_lock;
    private int time_counter;

    // Start is called before the first frame update
    void Start()
    {
        change_color(0.2f, 0.6f, 1.0f, 0.5f);
        if(target_portal_name == ""){
            target_portal_name = "empty";
            lock_portal();
        }
        if(portal_name == ""){
            portal_name = "empty";
        }
        portal_lock = 0;
        time_counter = 0;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(time_counter != 0){
            time_counter += 1;
            if(time_counter == 100){
                unlock_portal();
            }
        }
    }

    // Freeze Portal: This will auto unlock after 2 seconds
    void freeze_portal()
    {
        time_counter = 1;
        portal_lock = 1;
        change_color(0.1f, 0.1f, 0.1f, 0.5f);
    }

    // Lock Portal: This will not auto unlock, have to use unlock method to unlock
    void lock_portal()
    {
        time_counter = 0;
        portal_lock = 1;
        change_color(0.1f, 0.1f, 0.1f, 0.5f);
    }

    void unlock_portal()
    {
        if(target_portal_name != "empty"){
            portal_lock = 0;
            time_counter = 0;
            change_color(0.2f, 0.6f, 1.0f, 0.5f); 
        }
              
    }

    private void OnTriggerStay(Collider other)
    {
        if(portal_lock == 0 && target_portal_name != "empty"){
            if (other.attachedRigidbody){
                Debug.Log("Portal Active");
                portal_list = FindObjectsOfType<PortalWithMotion>();
                foreach (PortalWithMotion portalWithMotion in portal_list){
                    if(portalWithMotion.portal_name == target_portal_name){
                        portalWithMotion.freeze_portal();
                        // freeze_portal();
                        other.attachedRigidbody.transform.position = portalWithMotion.transform.position;
                        portalWithMotion.sendingEffect.Play();
                        if (other.name == "Player_model") {
                            StartCoroutine(respawnPlayer(other.attachedRigidbody, 0.5f));
                        }
                        break;
                    }
                }
                
            }
        }
    }

    private void change_color(float r, float g, float b, float a){
        var Renderer = transform.GetComponent<Renderer>();
        // Debug.Log("Finding Object" + name);
        Color customColor = new Color(r, g, b, a);
        Renderer.material.SetColor("_Color", customColor);
    }

    IEnumerator respawnPlayer(Rigidbody rb, float countDown) {
        Player player = FindObjectOfType<Player>();
        player.portalTransmissionEffect.Play();
        player.GetComponentInChildren<MeshRenderer>().enabled = false;
        // player.rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(countDown);
        player.GetComponentInChildren<MeshRenderer>().enabled = true;
    }
}
