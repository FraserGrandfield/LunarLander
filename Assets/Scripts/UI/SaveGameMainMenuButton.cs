using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameMainMenuButton : UIButton
{
    public static event Action<bool> SaveGameMainMenu;

    protected override void RaiseOnButtonClick()
    {
        SaveGameMainMenu?.Invoke(false);
    }
}
