using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCommand : Command
{
    private int _rotation;
    public static event Action<int> RotateShip;

    public RotateCommand(IEntity entity, int rotation) : base(entity)
    {
        _rotation = rotation;
    }

    public override void Execute()
    {
        if (RotateShip != null)
        {
            RotateShip?.Invoke(_rotation);
        }
    }

    public override void Undo()
    {
        if (RotateShip != null)
        {
            RotateShip?.Invoke(_rotation * -1);
        }
    }
}
