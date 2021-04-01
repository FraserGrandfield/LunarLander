using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShipReplayManager : MonoBehaviour
{
    private InputReader _inputReader;
    private bool gamePaused;
    public static event Action PauseReplay;
    public static event Action UnPauseReplay;

    public static event Action PauseSound;

    private void Start()
    {
        _inputReader = GetComponent<InputReader>();
        gamePaused = false;
    }

    private void Update()
    {
        InputReader.InputKey? pause = _inputReader.ReadPauseInput();
        if (pause != null)
        {
            if (gamePaused)
            {
                UnPauseReplay?.Invoke();
                gamePaused = false;
            }
            else
            {
                PauseReplay?.Invoke();
                gamePaused = true;
            }
            
        }
    }
}
