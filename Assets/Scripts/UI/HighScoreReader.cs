using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class HighScoreReader : MonoBehaviour
{
    public static event Action<Dictionary<string, int>> DisplayHighScores;
    void Start()
    {
        ReadHighScoreFile();
    }

    private void ReadHighScoreFile()
    {
        string filePath = Application.persistentDataPath + "/highscores.dat";
        Dictionary<string, int> highscores;
        if (File.Exists(filePath))
        {
            FileStream file = File.OpenRead(filePath);
            BinaryFormatter bf = new BinaryFormatter();
            highscores = (Dictionary<string, int>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            highscores = new Dictionary<string, int>();
        }
        DisplayHighScores?.Invoke(highscores);
    }
}
