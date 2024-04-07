using UnityEngine.Audio;
using UnityEngine;
using System;

/**
 * The sound class is used to add sounds to the audio manager, easier than some choppy methods of
 * adding sounds by e.g. instantiating empties with an audio source
 */

[Serializable]
public class Sound
{
    public String name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
