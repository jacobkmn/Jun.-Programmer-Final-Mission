using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;
    [Range(0f, 1f)]
    public float spatialBlend;
    public bool playOnAwake;
    public bool loop;

    public AudioSource source;

    //remember: playOnAwake only makes sense if an object is instantiated

}
