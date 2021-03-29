using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateCommand : Command
{
    private float _force;
    public static event Action<float> AccelerateShip;

    public AccelerateCommand(IEntity entity, float force) : base(entity)
    {
        _force = force;
    }

    public override void Execute()
    {
        if (AccelerateShip != null)
        {
            AccelerateShip?.Invoke(_force);
        }
    }

    public override void Undo()
    {
        if (AccelerateShip != null)
        {
            AccelerateShip?.Invoke(_force * -1f);
        }
    }
}
