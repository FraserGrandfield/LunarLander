using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public IState Tick(ShipManager ship, InputReader.InputKey? acceleration, InputReader.InputKey? accelerateKeyUp, 
        InputReader.InputKey? rotation, InputReader.InputKey? resume, InputReader.InputKey? pause);
    public void Enter(ShipManager ship);
    public void Exit(ShipManager ship);
}
