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
    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;

    private void Start()
    {
        fuel = 5000;
        StartRecording?.Invoke();
    }

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        ShipMovment.fuelUsed += updateFuel;
        ShipMovment.updateVelocity += updateVelocity;
        GroundContatct.ShipTouchedGround += shipTouchedGround;
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
    
    public float getXSpeed()
    {
        return xSpeed;
    }
    
    public float getYSpeed()
    {
        return ySpeed;
    }

    private void updateFuel(int fuelUsed)
    {
        fuel -= fuelUsed;
        if (fuel < 0)
        {
            fuel = 0;
        }
    }

    private void updateVelocity(float x, float y)
    {
        xSpeed = x;
        ySpeed = y;
    }

    private void shipTouchedGround(bool landed)
    {
        if (landed)
        {
            shipLanded = true;
        }
        else
        {
            shipCrashed = true;
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
