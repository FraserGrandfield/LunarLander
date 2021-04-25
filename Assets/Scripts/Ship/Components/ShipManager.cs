using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    private InputReader inputReader;
    public static event Action StartRecording;
    public static event Action<GameObject> CheckCameraPosition;

    private IState currentState;
    private ShipStats shipStats;

    private void Start()
    {
        shipStats = gameObject.GetComponent<ShipStats>();
        inputReader = GetComponent<InputReader>();
        StartRecording?.Invoke();
        if (shipStats.GetPlayTutorial())
        {
            currentState = new TutorialRotateRight();
        }
        else
        {
            currentState = new ShipIdle();
        }
    }

    public ShipStats getShipStats()
    {
        return shipStats;
    }

    private void Update()
    {
        CheckCameraPosition?.Invoke(gameObject);
        InputReader.InputKey? acceleration = inputReader.ReadAccelerateInput();
        InputReader.InputKey? accelerateKeyUp = inputReader.ReadAccelerateInputKeyUp();
        InputReader.InputKey? rotation = inputReader.ReadRotateInput();
        InputReader.InputKey? resume = inputReader.ReadContinueInput();
        InputReader.InputKey? pause = inputReader.ReadPauseInput();
        UpdateState(acceleration, accelerateKeyUp, rotation, resume, pause);
    }

    private void UpdateState(InputReader.InputKey? acceleration, InputReader.InputKey? accelerateKeyUp, 
        InputReader.InputKey? rotation, InputReader.InputKey? resume, InputReader.InputKey? pause)
    {
        IState newState = currentState.Tick(this, acceleration, accelerateKeyUp, rotation, resume, pause);

        if (newState != null)
        {
            currentState.Exit(this);
            currentState = newState;
            newState.Enter(this);
        }
    }
}
