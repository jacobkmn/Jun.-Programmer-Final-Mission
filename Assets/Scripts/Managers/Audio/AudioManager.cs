using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        instance = this;

        foreach (Sound s in sounds)
        {
            if (s.source == null)
                s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound " + name + "wasn't found!");
            return;
        }
        s.source.Play();
    }

    public void PlaySoundDelayed(string name, float delay)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound " + name + "wasn't found!");
            return;
        }
        s.source.PlayDelayed(delay);
    }

    public void PauseSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.source.isPlaying);

        if (s == null)
        {
            Debug.LogWarning("Sound " + name + "wasn't found! Could not pause.");
            return;
        }

        s.source.Pause();
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.source.isPlaying);

        if (s == null)
        {
            Debug.LogWarning("Sound " + name + "wasn't found! Could not stop.");
            return;
        }

        s.source.Stop();
    }
}
