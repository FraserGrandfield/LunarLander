using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteReplayButton : UIButton
{
    public static event Action DeleteReplayClicked;
    protected override void RaiseOnButtonClick()
    {
        DeleteReplayClicked?.Invoke();
    }
}
