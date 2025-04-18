using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1.5f)] public float volume = 1.0f; // Громкость от 0 до 1.5
}