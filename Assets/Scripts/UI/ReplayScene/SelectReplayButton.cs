using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectReplayButton : UIButton
{
    private string replayFilePath;

    public static event Action<string> ReplaySelected;

    private void OnEnable()
    {
        ReplaySelected += DifferentButtonBeenSelected;
    }
    
    private void OnDisable()
    {
        ReplaySelected -= DifferentButtonBeenSelected;
    }

    public void SetReplay(string replayPath, int replayNumber)
    {
        replayFilePath = replayPath;
        
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Replay:  " + replayNumber;
    }

    public string GetReplayPath()
    {
        return replayFilePath;
    }

    protected override void RaiseOnButtonClick()
    {
        gameObject.GetComponent<Image>().color = Color.gray;
        ReplaySelected?.Invoke(replayFilePath);
    }

    private void DifferentButtonBeenSelected(string replay)
    {
        if (replayFilePath != replay)
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
    
}
