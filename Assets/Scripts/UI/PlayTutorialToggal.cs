using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTutorialToggal : MonoBehaviour
{
    private Toggle toggle;
    public static event Action<PlayerData> UpdatePlayerData;
    public static event Action<string> ShowNotification;
    void Start()
    {
        toggle = gameObject.GetComponent<Toggle>();
        if (PlayerPrefs.GetInt("playTutorial") == 0)
        {
            toggle.isOn = false;
        }
        else
        {
            toggle.isOn = true;
        }
        toggle.onValueChanged.AddListener(ToggleValueChanged);
    }

    private void ToggleValueChanged(bool playTutorial)
    {
        if (PlayerPrefs.HasKey("name"))
        {
            if (playTutorial)
            {
                PlayerPrefs.SetInt("playTutorial", 1);
                
            }
            else
            {
                PlayerPrefs.SetInt("playTutorial", 0);
            }
            PlayerData pd = new PlayerData(PlayerPrefs.GetString("name"), PlayerPrefs.GetInt("gameVolume"),
            PlayerPrefs.GetInt("musicVolume"), PlayerPrefs.GetInt("playTutorial"), PlayerPrefs.GetInt("highScore"));
            UpdatePlayerData?.Invoke(pd);
        }
        else
        {
            ShowNotification?.Invoke("Please  choose  a  player!");
        }
    }
}
