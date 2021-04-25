using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class WritePlayerData : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerListManager.AddNewPlayer += SavePlayerData;
        GameVolumeSlider.UpdatePlayerData += SavePlayerData;
        MusicVolumeSlider.UpdatePlayerData += SavePlayerData;
        PlayTutorialToggal.UpdatePlayerData += SavePlayerData;
        CheckNewHighScore.SavePlayerData += SavePlayerData;
    }
    private void OnDisable()
    {
        PlayerListManager.AddNewPlayer -= SavePlayerData;
        GameVolumeSlider.UpdatePlayerData -= SavePlayerData;
        MusicVolumeSlider.UpdatePlayerData -= SavePlayerData;
        PlayTutorialToggal.UpdatePlayerData -= SavePlayerData;
        CheckNewHighScore.SavePlayerData -= SavePlayerData;
    }

    private void SavePlayerData(PlayerData playerData)
    {
        string filePath = Application.persistentDataPath + "/" + playerData.GetName() + "PlayerData.dat";
        FileStream file;
        Debug.Log("Save highscore" + playerData.GetHighScore());
        if (File.Exists(filePath))
        {
            file = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        }
        else
        {
            file = File.Create(filePath);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, playerData);
        file.Close();
    }
}
