using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameMainMenuButton : UIButton
{
    public static event Action<bool> SaveGameMainMenu;
    public static event Action<string> ChangeSceneAnimation;


    protected override void RaiseOnButtonClick()
    {
        SaveGameMainMenu?.Invoke(false);
        ChangeSceneAnimation?.Invoke("Temp");
    }
}
