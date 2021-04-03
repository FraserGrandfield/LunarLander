using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRotateRight : IState
{
    public static event Action<int> RotateShip;
    public static event Action PauseShip;
    public static event Action UnPauseShip;
    public static event Action ShowTutorialTurnRightUI;                                                                                              
    public static event Action HideTutorialTurnRightUI;                                                                                            

    public void Enter(ShipManager ship)
    {
        return;
    }

    public IState Tick(ShipManager ship, InputReader.InputKey? acceleration, InputReader.InputKey? accelerateKeyUp, InputReader.InputKey? rotation, InputReader.InputKey? resume, InputReader.InputKey? pause)
    {
        if (ship.gameObject.GetComponent<ShipStats>().getShipCrashed()) return new ShipCrashed();
        if (ship.gameObject.GetComponent<ShipStats>().getShipLanded()) return new ShipLanded();
        if (rotation == InputReader.InputKey.RotateClockWise)
        {
            UnPauseShip?.Invoke();
            RotateShip?.Invoke(-1);
            return new TutorialRotateLeft();
        }
        PauseShip?.Invoke();
        ShowTutorialTurnRightUI?.Invoke();
        return null;
    }

    public void Exit(ShipManager ship)
    {
        HideTutorialTurnRightUI?.Invoke();
    }
}
