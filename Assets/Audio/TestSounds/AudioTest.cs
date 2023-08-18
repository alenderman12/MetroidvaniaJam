using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    public string soundName;
    public float SFXVolume;
    public float MusicVolume;
    public bool testVolume;

    // Start is called before the first frame update

    private void Awake()
    {
        if (testVolume)
        {
            PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
            PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        }
    }
    void Start()
    {
        FindObjectOfType<AudioManager>().PlaySound(soundName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
