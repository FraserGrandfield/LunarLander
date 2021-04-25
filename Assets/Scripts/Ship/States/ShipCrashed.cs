using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCrashed : IState
{
    public static event Action shipCrashed;
    public static event Action<string> shipCrashedAchievement;
    public static event Action RestartShip;
    public static event Action<int> EndGame;
    public static event Action<bool> EndRound;
    public static event Action<bool> SaveShipCrashed;

    public void Enter(ShipManager ship)
    {
        shipCrashed?.Invoke();
        shipCrashedAchievement?.Invoke("crashed");
        SaveShipCrashed?.Invoke(false);
        ShipStats ss = ship.gameObject.GetComponent<ShipStats>();
        if (ss.getFuel() < 1)
        {
            EndGame?.Invoke(ss.getScore());
            if (ss.getScore() == 500)
            {
                shipCrashedAchievement?.Invoke("500");
            }

            if (ss.getScore() == 1000)
            {
                shipCrashedAchievement?.Invoke("1000");
            }
        }
        else
        {
            EndRound?.Invoke(false);
        }
    }

    public IState Tick(ShipManager ship, InputReader.InputKey? acceleration, InputReader.InputKey? accelerateKeyUp, InputReader.InputKey? rotation, InputReader.InputKey? resume, InputReader.InputKey? pause)
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
