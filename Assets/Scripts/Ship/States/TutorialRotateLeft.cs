using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRotateLeft : IState
{
    public static event Action<int> RotateShip;
    public static event Action PauseShip;
    public static event Action UnPauseShip;
    public static event Action ShowTutorialTurnLeftUI;                                                                                              
    public static event Action HideTutorialTurnLeftUI;                                                                                              

    public void Enter(ShipManager ship)
    {
        PauseShip?.Invoke();
        ShowTutorialTurnLeftUI?.Invoke();
    }

    public IState Tick(ShipManager ship, InputReader.InputKey? acceleration, InputReader.InputKey? accelerateKeyUp, InputReader.InputKey? rotation, InputReader.InputKey? resume, InputReader.InputKey? pause)
    {
        if (ship.gameObject.GetComponent<ShipStats>().getShipCrashed()) return new ShipCrashed();
        if (ship.gameObject.GetComponent<ShipStats>().getShipLanded()) return new ShipLanded();
        if (rotation == InputReader.InputKey.RotateAntiClockWise)
        {
            UnPauseShip?.Invoke();
            RotateShip?.Invoke(1);
            return new TutorialAccelerate();
        }
        return null;
    }

    public void Exit(ShipManager ship)
    {
        HideTutorialTurnLeftUI?.Invoke();
    }
}
