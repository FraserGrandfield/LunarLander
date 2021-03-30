using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLanded : IState
{
    public static event Action PauseShip;
    public static event Action shipLanded;
    public static event Action RestartShip;

    public void Enter(ShipManager ship)
    { 
        Animator animator = ship.GetComponent<Animator>();
        animator.SetBool("ForceApplied", false);
        PauseShip?.Invoke();
        shipLanded?.Invoke();
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
