using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLanded : IState
{

    public void Enter(ShipManager ship)
    {
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
