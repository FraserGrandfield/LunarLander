using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAccelerating : IState
{
    public static event Action AccelerateShip;
    public static event Action<int> RotateShip;

    public void Enter(ShipManager ship)
    { 
        AccelerateShip?.Invoke();
    }

    public IState Tick(ShipManager ship, InputReader.InputKey? acceleration, InputReader.InputKey? accelerateKeyUp, InputReader.InputKey? rotation, InputReader.InputKey? resume, InputReader.InputKey? pause)
    {
        if (ship.gameObject.GetComponent<ShipStats>().GetShipCrashed()) return new ShipCrashed();
        if (ship.gameObject.GetComponent<ShipStats>().GetShipLanded()) return new ShipLanded();
        if (pause == InputReader.InputKey.Escape) return new ShipPaused();
        if (accelerateKeyUp == InputReader.InputKey.SpaceUp || ship.getShipStats().GetFuel() < 1) return new ShipIdle();
        AccelerateShip?.Invoke();
        if (rotation == InputReader.InputKey.RotateClockWise)
        {
            RotateShip?.Invoke(-1);
        }

        if (rotation == InputReader.InputKey.RotateAntiClockWise)
        {
            RotateShip?.Invoke(1);
        }
        
        return null;
    }

    public void Exit(ShipManager ship)
    {
        return;
    }
}
