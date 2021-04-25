using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCrashed : IState
{
    public static event Action ShipCrashedEvent;
    public static event Action<string> ShipCrashedAchievement;
    public static event Action RestartShip;
    public static event Action<int> EndGame;
    public static event Action<bool> EndRound;
    public static event Action<bool> SaveShipCrashed;

    public void Enter(ShipManager ship)
    {
        ShipCrashedEvent?.Invoke();
        ShipCrashedAchievement?.Invoke("crashed");
        SaveShipCrashed?.Invoke(false);
        ShipStats ss = ship.gameObject.GetComponent<ShipStats>();
        if (ss.GetFuel() < 1)
        {
            EndGame?.Invoke(ss.GetScore());
            if (ss.GetScore() == 500)
            {
                ShipCrashedAchievement?.Invoke("500");
            }

            if (ss.GetScore() == 1000)
            {
                ShipCrashedAchievement?.Invoke("1000");
            }
        }
        else
        {
            EndRound?.Invoke(false);
        }
    }

    public IState Tick(ShipManager ship, InputReader.InputKey? acceleration, InputReader.InputKey? accelerateKeyUp, InputReader.InputKey? rotation, InputReader.InputKey? resume, InputReader.InputKey? pause)
    {
        if (resume == InputReader.InputKey.Resume && ship.gameObject.GetComponent<ShipStats>().GetFuel() > 0)
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
