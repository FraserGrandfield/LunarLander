using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLanded : IState
{
    public static event Action shipLanded;
    public static event Action RestartShip;
    public static event Action<int> EndGame;
    public static event Action<bool> EndRound;

    
    public void Enter(ShipManager ship)
    {
        shipLanded?.Invoke();
        if (ship.gameObject.GetComponent<ShipStats>().getFuel() < 1)
        {
            EndGame?.Invoke(ship.gameObject.GetComponent<ShipStats>().getScore());
        }
        else
        {
            EndRound?.Invoke(true);
        }
    }

    public IState Tick(ShipManager ship, InputReader.InputKey? acceleration, InputReader.InputKey? accelerateKeyUp, InputReader.InputKey? rotation, InputReader.InputKey? resume)
    {
        if (resume == InputReader.InputKey.Resume && ship.gameObject.GetComponent<ShipStats>().getFuel() > 0)
        {
            RestartShip?.Invoke(); 
            return new ShipIdle();
        }
        return null;
    }

    public void Exit(ShipManager ship)
    {
        return;
    }
}