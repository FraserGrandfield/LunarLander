using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DeletePlayerHighScore : MonoBehaviour
{
    private void OnEnable()
    {
        DeletePlayer.DeletePlayerInfo += DeleteHighScore;
    }

    private void OnDisable()
    {
        DeletePlayer.DeletePlayerInfo -= DeleteHighScore;
    }

    private void DeleteHighScore(PlayerData player)
    {
        string filePath = Application.persistentDataPath + "/highscores.dat";
        if (File.Exists(filePath))
        {
            FileStream file = File.OpenRead(filePath);
            if (file.Length != 0)
            {
                BinaryFormatter bf = new BinaryFormatter();
                Dictionary<string, int> highscores = (Dictionary<string, int>)bf.Deserialize(file);
                file.Close();
                if (highscores.ContainsKey(player.GetName()))
                {
                    highscores.Remove(player.GetName());
                    file = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                    bf.Serialize(file, highscores);
                    file.Close();
                }
            }
        }
    }
}
