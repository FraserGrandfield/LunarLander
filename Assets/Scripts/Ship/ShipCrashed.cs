using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCrashed : IState
{
    public static event Action shipCrashed;
    public static event Action RestartShip;


    public void Enter(ShipManager ship)
    {
        shipCrashed?.Invoke();
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
