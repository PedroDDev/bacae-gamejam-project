using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // An array of Sound objects to manage different sounds.
    public Sound[] sounds;

    // A reference to the AudioManager instance.
    public static AudioManager instance;

    // This method is called when the object is created.
    void Awake()
    {
        // Check if there is already an AudioManager instance.
        if (instance == null)
        {
            // If not, set this instance as the AudioManager.
            instance = this;
        }
        else
        {
            // If an AudioManager instance already exists, destroy this object.
            Destroy(gameObject);
            return;
        }

        // Make the AudioManager persist across scenes.
        DontDestroyOnLoad(gameObject);

        // Initialize each Sound object.
        foreach (Sound s in sounds)
        {
            // Create an AudioSource component for each sound.
            s.source = gameObject.AddComponent<AudioSource>();

            // Assign the AudioClip to the AudioSource.
            s.source.clip = s.clip;

            // Set volume, pitch, and loop properties for the AudioSource.
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Play a sound by name.
    public void Play(string name)
    {
        // Find the Sound object with the specified name.
        Sound s = Array.Find(sounds, sound => sound.Name == name);

        // Play the sound if it exists.
        s.source.Play();
    }

    // Stop playing a sound by name.
    public void StopPlaying(string name)
    {
        // Find the Sound object with the specified name.
        Sound s = Array.Find(sounds, sound => sound.Name == name);

        // Check if the sound was found.
        if (s == null)
        {
            // Log a warning if the sound was not found.
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // Stop playing the sound.
        s.source.Stop();
    }
}