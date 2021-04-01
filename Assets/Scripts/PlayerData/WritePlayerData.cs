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
        PlayerListManager.AddNewPlayer += savePlayerData;
        VolumeUI.UpdatePlayerData += savePlayerData;
    }
    private void OnDisable()
    {
        PlayerListManager.AddNewPlayer -= savePlayerData;
        VolumeUI.UpdatePlayerData -= savePlayerData;
    }

    private void savePlayerData(PlayerData playerData)
    {
        string filePath = Application.persistentDataPath + "/" + playerData.getName() + "PlayerData.dat";
        FileStream file;

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
