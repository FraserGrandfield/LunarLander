using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class WritePlayerAchievmentData : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerListManager.AddNewPlayerAchievments += savePlayerAchievmentData;
    }

    private void OnDisable()
    {
        PlayerListManager.AddNewPlayerAchievments -= savePlayerAchievmentData;
    }
    
    private void savePlayerAchievmentData(Dictionary<string, bool> achievments, string name)
    {
        string filePath = Application.persistentDataPath + "/" + name + "PlayerAchievmentData.dat";
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
        bf.Serialize(file, achievments);
        file.Close();
    }
}
