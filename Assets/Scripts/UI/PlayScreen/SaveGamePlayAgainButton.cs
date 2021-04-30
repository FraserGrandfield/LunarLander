using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGamePlayAgainButton : UIButton
{
    public static event Action<bool> SaveGamePlayAgain;
    public static event Action<string> ChangeSceneAnimation;
    protected override void RaiseOnButtonClick()
    {
        SaveGamePlayAgain?.Invoke(true);
        ChangeSceneAnimation?.Invoke("Temp");
    }
}
