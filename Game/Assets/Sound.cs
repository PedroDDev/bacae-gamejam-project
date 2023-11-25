using UnityEngine.Audio;
using UnityEngine;

// This class represents a sound object in Unity.
[System.Serializable]
public class Sound
{
    /** Determines whether the sound should loop when played. */
    public bool loop;

    /** The name of the sound. */
    public string Name;

    /** The audio clip associated with this sound. */
    public AudioClip clip;

    /** The volume of the sound, with a range between 0 and 1. */
    [Range(0f, 1f)]
    public float volume;

    /** The pitch of the sound, with a range between 0.1 and 3. */
    [Range(0.1f, 3f)]
    public float pitch;

    /** This field holds the AudioSource component that plays the sound. */
    [HideInInspector]
    public AudioSource source;
}