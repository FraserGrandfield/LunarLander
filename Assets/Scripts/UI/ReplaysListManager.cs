using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReplaysListManager : MonoBehaviour
{
    [SerializeField] private GameObject button;
    private string selectedReplayPath = null;
    private ArrayList replays = new ArrayList();
    public static event Action<string> ShowNotificaiton;
    public static event Action<string> ChangeScene;

    private void OnEnable()
    {
        ReadAllReplayData.AllReplayData += addAllReplaysToList;
        SelectReplayButton.ReplaySelected += setSelectedReplay;
        SetReplayButton.SetReplayButtonClicked += setActiveReplay;
    }
    
    private void OnDisable()
    {
        ReadAllReplayData.AllReplayData -= addAllReplaysToList;
        SelectReplayButton.ReplaySelected -= setSelectedReplay;
        SetReplayButton.SetReplayButtonClicked -= setActiveReplay;
    }

    private void addAllReplaysToList(ArrayList replaysList)
    {
        Debug.Log("Here1");
        foreach(string replay in replaysList)
        {
            Debug.Log("Here2");
            addReplayToList(replay);
        }
    }
    
    private void addReplayToList(string replayPath)
    {
        Debug.Log("Here3");
        replays.Add(replayPath);
        GameObject newButton = Instantiate(button);
        newButton.SetActive(true);
        newButton.GetComponent<SelectReplayButton>().SetReplay(replayPath, replays.Count);
        newButton.transform.SetParent(transform);
        newButton.transform.localScale = new Vector3(1, 1, 1);
    }
    
    private void setSelectedReplay(string replayPath)
    {
        selectedReplayPath = replayPath;
    }

    private void setActiveReplay()
    {
        if (selectedReplayPath != null)
        {
            PlayerPrefs.SetString("replayPath", selectedReplayPath);
            ChangeScene?.Invoke("ReplayScene");
        }
        else
        {
            ShowNotificaiton?.Invoke("Please select a replay");
        }
    } 
}
