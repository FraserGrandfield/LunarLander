using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeletePlayer : MonoBehaviour
{
    public static event Action<PlayerData> DeletePlayerInfo;
    private void OnEnable()
    {
        PlayerListManager.DeletePlayer += DeleteAllPlayerData;
    }

    private void OnDisable()
    {
        PlayerListManager.DeletePlayer -= DeleteAllPlayerData;
    }

    private void DeleteAllPlayerData(PlayerData player)
    {
        DeletePlayerInfo?.Invoke(player);
        string[] files = Directory.GetFiles(Application.persistentDataPath, player.GetName() + "PlayerData.dat", SearchOption.TopDirectoryOnly);
        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
        }
        string filePath = Application.persistentDataPath + "/" + player.GetName() + "PlayerAchievmentData.dat";
        File.Delete(filePath);
    }
}
