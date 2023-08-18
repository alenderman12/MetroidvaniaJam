using System;
using UnityEngine;

// To call a sound, use the following within a script:
// FindObjectOfType<AudioManager>().PlaySound("NAME");
public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public Sound[] sounds;

    private const string SFXVolumeKey = "SFXVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const float DefaultVolume = 1.0f;

    private float savedSFXVolume;
    private float savedMusicVolume;

    void Awake()
    {
        GetAudioPlayerPrefs();

        // Checks if an instance of the AudioManager already exists in a scene.
        // Used when transitioning between scenes to avoid restarting music.
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // Attaches AudioSource component from each sound to the AudioManager game object.
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            if (sound.isSFX)
            {
                sound.source.volume = sound.volume * savedSFXVolume;
            } else if (sound.isMusic)
            {
                sound.source.volume = sound.volume * savedMusicVolume;
            }
        }
    }

    private void GetAudioPlayerPrefs()
    {
        // Check if SFXVolume and MusicVolume keys exist
        if (!PlayerPrefs.HasKey(SFXVolumeKey))
        {
            PlayerPrefs.SetFloat(SFXVolumeKey, DefaultVolume);
        }

        if (!PlayerPrefs.HasKey(MusicVolumeKey))
        {
            PlayerPrefs.SetFloat(MusicVolumeKey, DefaultVolume);
        }
        UpdateAudioPlayerPrefs();
    }

    private void UpdateAudioPlayerPrefs()
    {
        // Load saved volume settings
        savedSFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey);
        savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
    }

    public void PlaySound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound not found: " + name + ".");
            return;
        }
        sound.source.Play();
    }

    public void UpdateSoundVolumes()
    {
        UpdateAudioPlayerPrefs();
        foreach (Sound sound in sounds)
        {
            // Find the correct AudioSource
            string clipName = sound.clip.name;
            AudioSource currentAudioSource = null; // Initialize to null
            AudioSource[] audioSources = GetComponents<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource.clip != null && audioSource.clip.name == clipName)
                {
                    currentAudioSource = audioSource; // Assign the matching AudioSource
                    break; // Break out of the loop once a match is found
                }
            }

            if (currentAudioSource != null)
            {
                if (sound.isSFX)
                {
                    currentAudioSource.volume = sound.volume * savedSFXVolume;
                }
                else if (sound.isMusic)
                {
                    currentAudioSource.volume = sound.volume * savedMusicVolume;
                }
            }
        }
    }
}
