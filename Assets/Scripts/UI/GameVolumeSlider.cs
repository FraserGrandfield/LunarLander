using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVolumeSlider : MonoBehaviour
{
    private Slider slider;

    public static event Action<PlayerData> UpdatePlayerData; 

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
        int volume = PlayerPrefs.GetInt("gameVolume");
        slider.value = volume;
    }

    private void saveVolume()
    {
        PlayerPrefs.SetInt("gameVolume", (int)slider.value);
        PlayerData pd = new PlayerData(PlayerPrefs.GetString("name"), PlayerPrefs.GetInt("gameVolume"),
            PlayerPrefs.GetInt("musicVolume"), PlayerPrefs.GetInt("playTutorial"));
        UpdatePlayerData?.Invoke(pd);
    }
}
