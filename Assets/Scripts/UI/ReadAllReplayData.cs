using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ReadAllReplayData : MonoBehaviour
{
    public static event Action<ArrayList> AllReplayData;
    void Start()
    {
        LoadAllReplayData();
    }
    
    public void LoadAllReplayData()
    {
        ArrayList replayDataList = new ArrayList();
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*Replay" + PlayerPrefs.GetString("name") + ".dat", SearchOption.TopDirectoryOnly);
        for (int i = 0; i < files.Length; i++)
        {
            string filePath = files[i];
            if (File.Exists(filePath))
            {
                replayDataList.Add(filePath);
            }
        }
        Debug.Log(replayDataList.Count);
        AllReplayData?.Invoke(replayDataList);
    }
}
