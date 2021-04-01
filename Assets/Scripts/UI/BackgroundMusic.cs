using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    private void Start()
    {
        SaveSettingsButton.SaveSettings += audioChanged;
        DontDestroyOnLoad(transform.gameObject);
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = (float)PlayerPrefs.GetInt("musicVolume") / 100;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        SaveSettingsButton.SaveSettings -= audioChanged;
    }

    private void audioChanged()
    {
        audioSource.volume = (float)PlayerPrefs.GetInt("musicVolume") / 100;
    }
}
