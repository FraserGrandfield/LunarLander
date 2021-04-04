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
        Debug.Log("here" + score + " " + PlayerPrefs.GetInt("highScore"));
        if (score > PlayerPrefs.GetInt("highScore") && PlayerPrefs.GetInt("playTutorial") == 0)
        {
            PlayerPrefs.SetInt("highScore", score);
            BinaryFormatter bf = new BinaryFormatter();
            string filePath = Application.persistentDataPath + "/highscores.dat";
            Dictionary<string, int> highScores = new Dictionary<string, int>();
            FileStream file;
            Debug.Log("here1");
            if (File.Exists(filePath))
            {
                file = File.OpenRead(filePath);
                if (file.Length != 0)
                {
                    Debug.Log("here2");
                    Dictionary<string, int> tempHighscores = (Dictionary<string, int>)bf.Deserialize(file);
                    highScores = highScores.Concat(tempHighscores).ToDictionary(k => k.Key, v => v.Value);
                }
                if (highScores.ContainsKey(PlayerPrefs.GetString("name")))
                {
                    Debug.Log("here3");
                    highScores[PlayerPrefs.GetString("name")] = score;
                }
                else
                {
                    Debug.Log("here4");
                    highScores.Add(PlayerPrefs.GetString("name"), score);
                }
                file.Close();
                file = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            }
            else
            {
                Debug.Log("here5");
                highScores.Add(PlayerPrefs.GetString("name"), score);
                file = File.Create(filePath);
            }
            Debug.Log("here6");
            bf.Serialize(file, highScores);
            file.Close();
            PlayerData pd = new PlayerData(PlayerPrefs.GetString("name"), PlayerPrefs.GetInt("gameVolume"),
                PlayerPrefs.GetInt("musicVolume"), PlayerPrefs.GetInt("playTutorial"), PlayerPrefs.GetInt("highScore"));
            SavePlayerData?.Invoke(pd);
        }
    }
}
