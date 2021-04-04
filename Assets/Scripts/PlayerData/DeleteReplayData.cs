using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeleteReplayData : MonoBehaviour
{
    private void OnEnable()
    {
        DeletePlayer.DeletePlayerReplay += DeleteAllReplays;
        ReplaysListManager.DeleteReplayFile += DeleteReplay;
    }

    private void OnDisable()
    { 
        DeletePlayer.DeletePlayerReplay -= DeleteAllReplays;
        ReplaysListManager.DeleteReplayFile -= DeleteReplay;
    }

    private void DeleteAllReplays(PlayerData player)
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*Replay" + player.getName() + ".dat", SearchOption.TopDirectoryOnly);
        for (int i = 0; i < files.Length; i++)
        {
            DeleteReplay(files[i]);
        }
    }

    private void DeleteReplay(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
