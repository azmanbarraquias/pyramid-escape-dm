using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    public AudioMixerGroup audioMixer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        { 
            instance = this;
        }
        else
        { 
            Destroy(gameObject); 
        }

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.audioClip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

            sound.source.outputAudioMixerGroup = audioMixer;

            if (sound.playThisOnAwake == true)
                sound.source.Play();
        }
    }

    public void Play(string soundTitle)
    {
        Sound sound = Array.Find(sounds, s => s.title == soundTitle);

        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundTitle + " not found!");
            return;
        }

        sound.source.Play();
    }

    public void Stop(string soundTitle)
    {
        Sound sound = Array.Find(sounds, s => s.title == soundTitle);

        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundTitle + " not found!");
            return;
        }

        sound.source.Stop();
    }

    public Sound GetSound(string soundTitle)
    {
        Sound sound = Array.Find(sounds, s => s.title == soundTitle);

        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundTitle + " not found!");
            return null;
        }

        return sound;
    }
}
