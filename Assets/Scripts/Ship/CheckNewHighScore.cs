using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class CheckNewHighScore : MonoBehaviour
{
    public static event Action<PlayerData> SavePlayerData; 
    private void OnEnable()
    {
        ShipCrashed.EndGame += CheckIfNewHighScore;
        ShipLanded.EndGame += CheckIfNewHighScore;
    }

    private void OnDisable()
    {
        ShipCrashed.EndGame -= CheckIfNewHighScore;
        ShipLanded.EndGame -= CheckIfNewHighScore;
    }

    private void CheckIfNewHighScore(int score)
    {
        if (score > PlayerPrefs.GetInt("highscore") && PlayerPrefs.GetInt("playTutorial") == 0)
        {
            PlayerPrefs.SetInt("highscore", score);
            BinaryFormatter bf = new BinaryFormatter();
            string filePath = Application.persistentDataPath + "/highscores.dat";
            Dictionary<string, int> highScores = new Dictionary<string, int>();
            FileStream file;
            if (File.Exists(filePath))
            {
                file = File.OpenRead(filePath);
                if (file.Length != 0)
                {
                    Dictionary<string, int> tempHighscores = (Dictionary<string, int>)bf.Deserialize(file);
                    highScores = highScores.Concat(tempHighscores).ToDictionary(k => k.Key, v => v.Value);
                }
                if (highScores.ContainsKey(PlayerPrefs.GetString("name")))
                {
                    highScores[PlayerPrefs.GetString("name")] = score;
                }
                else
                {
                    highScores.Add(PlayerPrefs.GetString("name"), score);
                }
                file.Close();
                file = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            }
            else
            {
                highScores.Add(PlayerPrefs.GetString("name"), score);
                file = File.Create(filePath);
            }
            bf.Serialize(file, highScores);
            PlayerData pd = new PlayerData(PlayerPrefs.GetString("name"), PlayerPrefs.GetInt("gameVolume"),
                PlayerPrefs.GetInt("musicVolume"), PlayerPrefs.GetInt("playTutorial"), PlayerPrefs.GetInt("highscore"));
            SavePlayerData?.Invoke(pd);
            file.Close();
        }
    }
}
