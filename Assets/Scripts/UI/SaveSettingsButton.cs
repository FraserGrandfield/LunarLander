using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSettingsButton : UIButton
{
    public static event Action SaveSettings;
    public static event Action<string> ShowNotification;
    protected override void RaiseOnButtonClick()
    {
        SaveSettings?.Invoke();
        ShowNotification?.Invoke("Settings Saved");
    }
}
