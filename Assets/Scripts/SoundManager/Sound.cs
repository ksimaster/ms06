using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string nameSound;

    //public AudioClip clip;
    public AudioSource AudioSource;

    [Range(0f, 1f)]
    public float Volume = 1f;

    public bool Mute = false;
}
