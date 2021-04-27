using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public AudioSource audioSource;
    public static event Action<string> OnUIButtonClick;

    [SerializeField] protected Button button;
    
    private void Start()
    {
        button.onClick.AddListener(ButtonClicked);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void ButtonClicked()
    {
        if (audioSource != null)
        {
            if (PlayerPrefs.HasKey("gameVolume"))
            {
                audioSource.volume = (float)PlayerPrefs.GetInt("gameVolume") / 100;
            }
            else
            {
                audioSource.volume = 0.5f;
            }
            audioSource.Play();
        }
        RaiseOnButtonClick();
    }

    protected virtual void RaiseOnButtonClick()
    {
        if (OnUIButtonClick != null)
        {
            OnUIButtonClick?.Invoke(button.name);
        }
    }
}
