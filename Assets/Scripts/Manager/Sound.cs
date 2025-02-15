using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public AudioClip audioClip;

    public string name;

    [Range(0,1)]
    public float volume;

    [Range(0,1)]
    public float pitch;

    public bool playOnAwake;

    public bool mute;

    public AudioSource audioSource;
}
