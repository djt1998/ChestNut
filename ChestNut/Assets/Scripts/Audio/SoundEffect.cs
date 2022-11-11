using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundEffect
{
    public string name;
    public AudioClip clip;
    public bool loop;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    public float spatialBlend;
    [TextArea(3, 10)]
    public string description;
    [HideInInspector]
    public AudioSource source;
}
