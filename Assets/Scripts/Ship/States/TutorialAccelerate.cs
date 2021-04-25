using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAccelerate : IState
{
    public static event Action PauseShip;
    public static event Action UnPauseShip;
    public static event Action ShowTutorialAccelerateUI;                                                                                              
    public static event Action HideTutorialAccelerateUI;                                                                                              
    public static event Action AccelerateShip;

    public void Enter(ShipManager ship)
    {
        PauseShip?.Invoke();
        ShowTutorialAccelerateUI?.Invoke();
    }

    public IState Tick(ShipManager ship, InputReader.InputKey? acceleration, InputReader.InputKey? accelerateKeyUp, InputReader.InputKey? rotation, InputReader.InputKey? resume, InputReader.InputKey? pause)
    {
        if (ship.gameObject.GetComponent<ShipStats>().GetShipCrashed()) return new ShipCrashed();
        if (ship.gameObject.GetComponent<ShipStats>().GetShipLanded()) return new ShipLanded();
        if (acceleration == InputReader.InputKey.SpaceDown)
        {
            UnPauseShip?.Invoke();
            AccelerateShip?.Invoke();
            return new ShipAccelerating();
        }
        
        return null;
    }

    public void Exit(ShipManager ship)
    {
        HideTutorialAccelerateUI?.Invoke();
    }
}
