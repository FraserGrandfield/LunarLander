using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    private void Start()
    {
        MusicVolumeSlider.UpdatePlayerData += audioChanged;
        DontDestroyOnLoad(transform.gameObject);
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = (float)PlayerPrefs.GetInt("musicVolume") / 100;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        MusicVolumeSlider.UpdatePlayerData -= audioChanged;
    }

    private void audioChanged(PlayerData pd)
    {
        audioSource.volume = (float)PlayerPrefs.GetInt("musicVolume") / 100;
    }
}
