using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ReadPlayerData : MonoBehaviour
{
    public static event Action<ArrayList> AllPlayerData;

    private void Start()
    {
        LoadAllPlayersData();
    }

    public void LoadAllPlayersData()
    {
        ArrayList playerDataList = new ArrayList();
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*PlayerData.dat", SearchOption.TopDirectoryOnly);
        for (int i = 0; i < files.Length; i++)
        {
            string filePath = files[i];
            if (File.Exists(filePath))
            {
                FileStream file = File.OpenRead(filePath);
                BinaryFormatter bf = new BinaryFormatter();
                PlayerData pd = (PlayerData)bf.Deserialize(file);
                playerDataList.Add(pd);
                file.Close();
            }
        }
        AllPlayerData?.Invoke(playerDataList);
    }
}
