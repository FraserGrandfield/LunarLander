using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLanded : IState
{
    public static event Action ShipLandedEvent;
    public static event Action<string> ShipLandedAchievement;
    public static event Action RestartShip;
    public static event Action<int> EndGame;
    public static event Action<bool> EndRound;

    public static event Action<bool> SaveShipLanded;

    public void Enter(ShipManager ship)
    {
        ShipLandedEvent?.Invoke();
        ShipLandedAchievement?.Invoke("landed");
        SaveShipLanded?.Invoke(false);
        ShipStats ss = ship.gameObject.GetComponent<ShipStats>();
        if (ss.GetFuel() < 1)
        {
            EndGame?.Invoke(ss.GetScore());
            if (ss.GetScore() == 500)
            {
                ShipLandedAchievement?.Invoke("500");
            }

            if (ss.GetScore() == 1000)
            {
                ShipLandedAchievement?.Invoke("1000");
            }
        }
        else
        {
            EndRound?.Invoke(true);
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
