using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCommand : Command
{
    private float _gravity;
    public static event Action<float> AddGravity;

    public GravityCommand(IEntity entity, float gravity) : base(entity)
    {
        _gravity = gravity;
    }

    public override void Execute()
    {
        if (AddGravity != null)
        {
            AddGravity?.Invoke(_gravity);
        }
    }

    public override void Undo()
    {
        if (AddGravity != null)
        {
            AddGravity?.Invoke(_gravity * -1f);
        }
    }
}
