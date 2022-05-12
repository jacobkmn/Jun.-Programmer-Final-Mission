using UnityEngine.Audio;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;
    private List<Sound> Sourceless = new List<Sound>();

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
            {
                //Debug.LogWarning("sound: " + s.name + ", does not have a source! Please add one in AudioManager");
                Sourceless.Add(s); //adds sound to a list to be called on when the game is reset
            }
            else if (s.source != null)
            {
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.spatialBlend = s.spatialBlend;
                s.source.playOnAwake = s.playOnAwake;
            }
        }
    }

    public void Reset()
    {
        foreach (Sound s in Sourceless)
        {
            s.source = null;
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

    //used to change which chef plays the backofhouse audio + sets that chefs respective doors to play its audioFX
    public void ChangeSource(string name, GameObject sourceObj)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound " + s.name + "wasn't found! Could not change source.");
            return;
        }

        //first door clicked: sound has no source and neither does door
        if (s.source == null && sourceObj.GetComponent<AudioSource>() == null)
        {
            s.source = sourceObj.gameObject.AddComponent<AudioSource>();
            s.source.enabled = true;
        }
        //if sound is already linked to a door's source, but a new door is clicked that doesn't yet have an audiosource
        else if (s.source != null && sourceObj.GetComponent<AudioSource>() == null)
        {
            //disable the previous door's audiosource and reset the sound's source
            s.source.gameObject.GetComponent<AudioSource>().enabled = false;
            s.source = null;
            //the selected door becomes the new source
            s.source = sourceObj.gameObject.AddComponent<AudioSource>();
            s.source.enabled = true;
        }
        //if another door is already the audiosource and the selected door has its own source
        else if (s.source != null && sourceObj.GetComponent<AudioSource>() != null)
        {
            //disable the previous door's audiosource and reset the sound's source
            s.source.gameObject.GetComponent<AudioSource>().enabled = false;
            s.source = null;
            //the selected door becomes the new source
            s.source = sourceObj.GetComponent<AudioSource>();
            s.source.enabled = true;
        }

        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.spatialBlend = s.spatialBlend;
        s.source.playOnAwake = s.playOnAwake;
    }

    public void ChangeClip(string name, GameObject sourceObj)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (sourceObj.GetComponent<AudioSource>() != null)
        {
            s.source = sourceObj.GetComponent<AudioSource>();
        }

        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.spatialBlend = s.spatialBlend;
        s.source.playOnAwake = s.playOnAwake;
    }
}
