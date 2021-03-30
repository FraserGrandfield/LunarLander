using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    private InputReader _inputReader;
    
    public static event Action<float> AccelerateShip;
    public static event Action<int> RotateShip;
    public static event Action StartRecording;
    public static event Action PauseGame;
    public static event Action AcceleratorKeyUp;

    [SerializeField] private bool gamePaused;

    private void Start()
    {
        StartRecording?.Invoke();
        gamePaused = false;
    }

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    private void Update()
    {
        if (!gamePaused)
        {
            int acceleration = _inputReader.ReadAccelerateInput();
            if (acceleration != 0)
            {
                AccelerateShip?.Invoke(acceleration);
            }

            bool accelerateKeyUp = _inputReader.ReadAccelerateInputKeyUp();
            if (accelerateKeyUp)
            {
                AcceleratorKeyUp?.Invoke();
            }

            int rotation = _inputReader.ReadRotateInput();
            if (rotation != 0)
            {
                RotateShip?.Invoke(rotation);
            }
        }
        bool escPressed = _inputReader.ReadPauseGameInput();
        if (escPressed)
        {
            gamePaused = !gamePaused;
            PauseGame?.Invoke();
        }
    }
}
