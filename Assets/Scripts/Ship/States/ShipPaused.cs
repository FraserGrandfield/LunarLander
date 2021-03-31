using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPaused : IState
{
    public static event Action PauseShip;
    
    public static event Action UnPauseShip;


    public void Enter(ShipManager ship)
    { 
        PauseShip?.Invoke();
    }

    public IState Tick(ShipManager ship, InputReader.InputKey? acceleration, InputReader.InputKey? accelerateKeyUp, InputReader.InputKey? rotation, InputReader.InputKey? resume, InputReader.InputKey? pause)
    {
        if (pause == InputReader.InputKey.Escape)
        {
           return new ShipIdle();
        }
        return null;
    }

    public void Exit(ShipManager ship)
    {
        UnPauseShip?.Invoke();
    }
}
