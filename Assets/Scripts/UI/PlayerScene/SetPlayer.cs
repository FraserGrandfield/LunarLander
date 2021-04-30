using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayer : UIButton
{
    public static event Action SetPlayerButtonClicked;
    protected override void RaiseOnButtonClick()
    {
        SetPlayerButtonClicked?.Invoke();
    }
}
