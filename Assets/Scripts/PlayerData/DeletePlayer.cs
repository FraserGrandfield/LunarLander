using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeletePlayer : MonoBehaviour
{
    public static event Action<PlayerData> DeletePlayerReplay;
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
        DeletePlayerReplay?.Invoke(player);
        string[] files = Directory.GetFiles(Application.persistentDataPath, player.getName() + "PlayerData.dat", SearchOption.TopDirectoryOnly);
        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
        }
    }
}
