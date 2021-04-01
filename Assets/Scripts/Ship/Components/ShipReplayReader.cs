using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ShipReplayReader : MonoBehaviour
{
    public static event Action<MemoryStream> ReplayMemoryStream;
    void Start()
    {
        ReadReplay();
    }

    private void ReadReplay()
    {
        string filePath = PlayerPrefs.GetString("replayPath");
        if (File.Exists(filePath))
        {
            FileStream file = File.OpenRead(filePath);
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream memoryStream = (MemoryStream) bf.Deserialize(file);
            file.Close();
            ReplayMemoryStream?.Invoke(memoryStream);
        }
        
    }
  
}
