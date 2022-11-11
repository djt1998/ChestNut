using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundEffectManger : MonoBehaviour
{
    [Range(0f, 1f)]
    public float volumeParam;
    public SoundEffect[] soundEffects;
    public static SoundEffectManger instacne;

    private void Awake() {
        if (instacne == null) {
            DontDestroyOnLoad(gameObject);
            instacne = this;
        }
        else if (instacne != this) {
            Destroy(gameObject);
            return;
        }

        foreach (SoundEffect se in soundEffects) {
            se.source = gameObject.AddComponent<AudioSource>();
            se.source.clip = se.clip;
            se.source.loop = se.loop;
            se.source.volume = se.volume * volumeParam;
            se.source.pitch = se.pitch;
            se.source.spatialBlend = se.spatialBlend;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Play("BGM");
    }

    public void Play(string name) {
        SoundEffect se = Array.Find(soundEffects, sound => sound.name == name);
        if (se == null) {
            Debug.Log("Sound effect " + name + " not found!");
            return;
        }
        se.source.Play();
    }

    public void VolumeChange(float volume) {
        volumeParam = volume;
        foreach (SoundEffect se in soundEffects) {
            se.source.volume = se.volume * volumeParam;
        }
    }
}
