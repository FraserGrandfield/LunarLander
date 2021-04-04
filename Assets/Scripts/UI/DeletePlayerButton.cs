using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlayerButton : UIButton
{
    public static event Action DeletePlayer;
    protected override void RaiseOnButtonClick()
    {
        DeletePlayer?.Invoke();
    }
}
