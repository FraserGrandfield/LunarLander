using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetReplayButton : UIButton
{
    public static event Action SetReplayButtonClicked;
    protected override void RaiseOnButtonClick()
    {
        SetReplayButtonClicked?.Invoke();
    }
}
