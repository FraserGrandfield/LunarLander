using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCrashed : IState
{
    public static event Action PauseShip;

    public void Enter(ShipManager ship)
    {
        Animator animator = ship.GetComponent<Animator>();
        animator.SetBool("ForceApplied", false);
        //TODO crash animation
        PauseShip?.Invoke();
    }

    public IState Tick(ShipManager ship, InputReader.InputKey? acceleration, InputReader.InputKey? accelerateKeyUp, InputReader.InputKey? rotation)
    {
        return null;
    }

    public void Exit(ShipManager ship)
    {
        return;
    }
}
