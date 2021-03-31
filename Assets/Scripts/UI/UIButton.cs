using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public static event Action<string> OnUIButtonClick;

    [SerializeField] protected Button button;
    
    private void Start()
    {
        button.onClick.AddListener(RaiseOnButtonClick);
    }

    protected virtual void RaiseOnButtonClick()
    {
        if (OnUIButtonClick != null)
        {
            OnUIButtonClick?.Invoke(button.name);
        }
    }
}
