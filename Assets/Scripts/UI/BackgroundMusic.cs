using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    private static BackgroundMusic backgroundMusic;
    
    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (backgroundMusic == null)
        {
            backgroundMusic = this;
        }
        else
        {
            Destroy(transform.gameObject);
        }
        MusicVolumeSlider.UpdateMusicVolume += audioChanged;
        PlayerListManager.UpdateMusicVolume += audioChanged;
        audioSource = gameObject.GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            audioSource.volume = (float)PlayerPrefs.GetInt("musicVolume") / 100;
        }
        else
        {
            audioSource.volume = 0.5f;
        }
        audioSource.Play();
    }

    private void OnDestroy()
    {
        MusicVolumeSlider.UpdateMusicVolume -= audioChanged;
        PlayerListManager.UpdateMusicVolume -= audioChanged;
    }

    private void audioChanged()
    {
        audioSource.volume = (float)PlayerPrefs.GetInt("musicVolume") / 100;
    }
}
