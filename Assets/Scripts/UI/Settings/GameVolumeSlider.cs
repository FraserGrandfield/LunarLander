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
        SaveSettingsButton.SaveSettings += SaveVolume;
    }
    
    private void OnDisable()
    {
        SaveSettingsButton.SaveSettings -= SaveVolume;
    }

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        int volume = PlayerPrefs.GetInt("gameVolume");
        slider.value = volume;
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetInt("gameVolume", (int)slider.value);
        PlayerData pd = new PlayerData(PlayerPrefs.GetString("name"), PlayerPrefs.GetInt("gameVolume"),
            PlayerPrefs.GetInt("musicVolume"), PlayerPrefs.GetInt("playTutorial"), PlayerPrefs.GetInt("highscore"));
        UpdatePlayerData?.Invoke(pd);
    }
}
