using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    private InputReader _inputReader;
    public static event Action StartRecording;
    private IState currentState = new ShipIdle();
    private bool shipLanded = false;
    private bool shipCrashed = false;
    private bool gamePaused = false;
    [SerializeField] private int fuel;
    private void Start()
    {
        fuel = 5000;
        StartRecording?.Invoke();
    }

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        ShipMovment.fuelUsed += updateFuel;
    }

    public bool getShipLanded()
    {
        return shipLanded;
    }
    
    public bool getShipCrashed()
    {
        return shipCrashed;
    }
    
    public bool getGamePaused()
    {
        return gamePaused;
    }

    public int getFuel()
    {
        return fuel;
    }

    private void updateFuel(int fuelUsed)
    {
        fuel -= fuelUsed;
        if (fuel < 0)
        {
            fuel = 0;
        }
    }

    private void Update()
    {
        InputReader.InputKey? acceleration = _inputReader.ReadAccelerateInput();
        InputReader.InputKey? accelerateKeyUp = _inputReader.ReadAccelerateInputKeyUp();
        InputReader.InputKey? rotation = _inputReader.ReadRotateInput();
        UpdateState(acceleration, accelerateKeyUp, rotation);
    }

    private void UpdateState(InputReader.InputKey? acceleration, InputReader.InputKey? accelerateKeyUp, InputReader.InputKey? rotation)
    {
        IState newState = currentState.Tick(this, acceleration, accelerateKeyUp, rotation);

        if (newState != null)
        {
            currentState.Exit(this);
            currentState = newState;
            newState.Enter(this);
        }
    }
    
}
