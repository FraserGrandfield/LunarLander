using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIdle : IState
{
    public static event Action AcceleratorKeyUp;
    public static event Action<int> RotateShip;
    
    public void Enter(ShipManager ship)
    {
        AcceleratorKeyUp?.Invoke();
        Animator animator = ship.GetComponent<Animator>();
        animator.SetBool("ForceApplied", false);
    }

    public IState Tick(ShipManager ship, InputReader.InputKey? acceleration, InputReader.InputKey? accelerateKeyUp, InputReader.InputKey? rotation)
    {
        if (!ship.getGamePaused())
        {
            if (ship.getShipCrashed()) return new ShipCrashed();
            if (ship.getShipLanded()) return new ShipLanded();
            if (acceleration == InputReader.InputKey.SpaceDown && ship.getFuel() > 0) return new ShipAccelerating();
            if (rotation == InputReader.InputKey.RotateClockWise)
            {
                RotateShip?.Invoke(-1);
            }

            if (rotation == InputReader.InputKey.RotateAntiClockWise)
            { 
                RotateShip?.Invoke(1);
            }
        }
        return null;
    }

    public void Exit(ShipManager ship)
    {
        return;
    }
}
