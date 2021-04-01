using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenPlayButton : UIButton
{
    public static event Action<string> ShowNotification;
    public static event Action<string> OnUIButtonClick;
    protected override void RaiseOnButtonClick()
    {
        if (PlayerPrefs.HasKey("name"))
        {
            OnUIButtonClick?.Invoke(button.name);
        }
        else
        {
            ShowNotification?.Invoke("Please choose a player!");
        }
    }
}
