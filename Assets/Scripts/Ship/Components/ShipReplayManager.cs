using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShipReplayManager : MonoBehaviour
{
    private InputReader _inputReader;
    private bool gamePaused;
    private bool endOfReplay;
    public static event Action PauseReplay;
    public static event Action UnPauseReplay;
    public static event Action LeftKeyPressed;
    public static event Action RightKeyPressed;

    private void Start()
    {
        _inputReader = GetComponent<InputReader>();
        gamePaused = false;
        endOfReplay = false;
    }

    private void OnEnable()
    {
        ShipPlayReplay.EndOfReplay += EndReplay;
    }

    private void OnDisable()
    {
        ShipPlayReplay.EndOfReplay += EndReplay;
    }

    private void EndReplay(int val)
    {
        endOfReplay = true;
    }

    private void Update()
    {
        InputReader.InputKey? pause = _inputReader.ReadPauseInput();
        if (pause != null)
        {
            if (gamePaused && !endOfReplay)
            {
                UnPauseReplay?.Invoke();
                gamePaused = false;
            }
            else if (!endOfReplay)
            {
                PauseReplay?.Invoke();
                gamePaused = true;
            }
        }
        else
        {
            InputReader.InputKey? leftKey = _inputReader.ReadLeftArrowKey();
            if (leftKey != null)
            {
                LeftKeyPressed?.Invoke();
            }
            InputReader.InputKey? rightKey = _inputReader.ReadRightArrowKey();
            if (rightKey != null)
            {
                RightKeyPressed?.Invoke();
            }
        }
    }
}
