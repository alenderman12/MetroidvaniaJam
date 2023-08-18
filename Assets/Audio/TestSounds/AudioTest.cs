using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    public string soundName;
    [Range(0.0f, 1.0f)]
    public float SFXVolume;
    [Range(0.0f, 1.0f)]
    public float MusicVolume;
    public bool testVolume;

    // Start is called before the first frame update

    private void Awake()
    {
        if (testVolume)
        {
            SetPrefs();
        }
    }
    void Start()
    {
        FindObjectOfType<AudioManager>().PlaySound(soundName);
    }

    // Update is called once per frame
    private void OnValidate()
    {
        SetPrefs();
        FindObjectOfType<AudioManager>().UpdateSoundVolumes();
    }

    private void SetPrefs()
    {
        if (testVolume)
        {
            PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
            PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        } else
        {
            // Load saved volume settings
            SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }
    }
}
