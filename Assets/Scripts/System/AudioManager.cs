using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    void Awake()
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.clip = sounds[i].clip;
            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
        }
    }

    public void Play(string soundName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            Sound s = Array.Find(sounds, sound => sound.name == soundName);
            if (s != null) {
                s.source.Play();
                return;
            }
        }
    }
}
