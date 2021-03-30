using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    private InputReader _inputReader;
    public static event Action StartRecording;
    private IState currentState = new ShipIdle();
    private bool gamePaused = false;
    private ShipStats _shipStats;

    private void Start()
    {
        _shipStats = gameObject.GetComponent<ShipStats>();
        StartRecording?.Invoke();
    }

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    public bool getGamePaused()
    {
        return gamePaused;
    }

    public ShipStats getShipStats()
    {
        return _shipStats;
    }

    private void Update()
    {
        InputReader.InputKey? acceleration = _inputReader.ReadAccelerateInput();
        InputReader.InputKey? accelerateKeyUp = _inputReader.ReadAccelerateInputKeyUp();
        InputReader.InputKey? rotation = _inputReader.ReadRotateInput();
        InputReader.InputKey? resume = _inputReader.ReadContinueInput();
        UpdateState(acceleration, accelerateKeyUp, rotation, resume);
    }

    private void UpdateState(InputReader.InputKey? acceleration, InputReader.InputKey? accelerateKeyUp, InputReader.InputKey? rotation, InputReader.InputKey? resume)
    {
        IState newState = currentState.Tick(this, acceleration, accelerateKeyUp, rotation, resume);

        if (newState != null)
        {
            currentState.Exit(this);
            currentState = newState;
            newState.Enter(this);
        }
    }
    
}
