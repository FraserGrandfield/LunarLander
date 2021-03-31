using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuButton : MonoBehaviour
{
    public static event Action<string> OnUIButtonClick;

    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(RaiseOnButtonClick);
    }

    private void RaiseOnButtonClick()
    {
        Debug.Log("Button clicked");
        if (OnUIButtonClick != null)
        {
            OnUIButtonClick?.Invoke(button.name);
        }
    }
}
