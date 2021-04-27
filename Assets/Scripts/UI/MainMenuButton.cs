using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuButton : MonoBehaviour
{
    private AudioSource audioSource;
    
    public static event Action<string> OnUIButtonClick;

    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(RaiseOnButtonClick);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void RaiseOnButtonClick()
    {
        if (OnUIButtonClick != null)
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
            OnUIButtonClick?.Invoke(button.name);
        }
    }
}
