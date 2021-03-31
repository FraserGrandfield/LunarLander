using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    private Slider slider;

    private void OnEnable()
    {
        SaveSettingsButton.SaveSettings += saveVolume;
    }
    
    private void OnDisable()
    {
        SaveSettingsButton.SaveSettings -= saveVolume;
    }

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        int volume = PlayerPrefs.GetInt("volume");
        slider.value = volume;
    }

    private void saveVolume()
    {
        PlayerPrefs.SetInt("volume", (int)slider.value);
    }
}
