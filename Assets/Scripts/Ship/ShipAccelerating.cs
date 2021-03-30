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
        Animator animator = ship.GetComponent<Animator>();
        animator.SetBool("ForceApplied", true);
    }

    public IState Tick(ShipManager ship, InputReader.InputKey? acceleration, InputReader.InputKey? accelerateKeyUp, InputReader.InputKey? rotation)
    {
        if (!ship.getGamePaused())
        {
            if (accelerateKeyUp == InputReader.InputKey.SpaceUp || ship.getFuel() < 1) return new ShipIdle();
            if (ship.getShipCrashed()) return new ShipCrashed();
            if (ship.getShipLanded()) return new ShipLanded();
            AccelerateShip?.Invoke();
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
