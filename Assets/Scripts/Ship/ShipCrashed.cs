using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCrashed : IState
{

    public void Enter(ShipManager ship)
    {
        Animator animator = ship.GetComponent<Animator>();
        //TODO crash animation
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
