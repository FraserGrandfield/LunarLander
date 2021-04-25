using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLanded : IState
{
    public static event Action shipLanded;
    public static event Action<string> shipLandedAchievement;
    public static event Action RestartShip;
    public static event Action<int> EndGame;
    public static event Action<bool> EndRound;

    public static event Action<bool> SaveShipLanded;

    public void Enter(ShipManager ship)
    {
        shipLanded?.Invoke();
        shipLandedAchievement?.Invoke("landed");
        SaveShipLanded?.Invoke(false);
        ShipStats ss = ship.gameObject.GetComponent<ShipStats>();
        if (ss.getFuel() < 1)
        {
            EndGame?.Invoke(ss.getScore());
            if (ss.getScore() == 500)
            {
                shipLandedAchievement?.Invoke("500");
            }

            if (ss.getScore() == 1000)
            {
                shipLandedAchievement?.Invoke("1000");
            }
        }
        else
        {
            EndRound?.Invoke(true);
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
