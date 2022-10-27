using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_Animation : MonoBehaviour
{
    public int updates_per_flash;
    public int updates_per_freq;

    private int flash_counter;
    private int freq_counter;
    private int current_flash;
    
    // Start is called before the first frame update
    void Start()
    {
        if(updates_per_flash == 0){
            updates_per_flash = 5;
        }
        if(updates_per_freq == 0){
            updates_per_freq = 50;
        }
        current_flash = 4;
        flash_counter = 0;
        freq_counter = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(freq_counter > updates_per_freq){
            if(flash_counter % updates_per_flash == 0){
                for(int i = 0; i < 5; i++){
                    if(i == current_flash){
                        change_color(0.6f, 0.6f, 1.0f, 0.4f, "Wind_ring_"+i);
                    }
                    else{
                        change_color(1.0f, 1.0f, 1.0f, 0.2f, "Wind_ring_"+i);
                    }
                }
                if(current_flash == -1){
                    current_flash = 4;
                }
                else{
                    current_flash += -1;
                }
                //Debug.Log("next flash: " + current_flash);
            }      
            if(current_flash == 4){
                freq_counter = 0;
            }
            flash_counter += 1;  
        }
        freq_counter += 1;    
        
    }

    private void change_color(float r, float g, float b, float a, string name){
        var Renderer = GetChildWithName(name).GetComponent<Renderer>();
        // Debug.Log("Finding Object" + name);
        Color customColor = new Color(r, g, b, a);
        Renderer.material.SetColor("_Color", customColor);
    }

    GameObject GetChildWithName(string name) {
        Transform childTrans = transform.Find(name);
        if (childTrans != null) {
            return childTrans.gameObject;
        } else {
            return null;
        }
    }
 
}
