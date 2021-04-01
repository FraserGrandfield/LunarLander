using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGamePlayAgainButton : UIButton
{
    public static event Action<bool> SaveGamePlayAgain;

    protected override void RaiseOnButtonClick()
    {
        SaveGamePlayAgain?.Invoke(true);
    }
}
