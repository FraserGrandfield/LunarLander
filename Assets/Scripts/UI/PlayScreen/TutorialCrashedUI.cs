using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCrashedUI : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("playTutorial") == 1)
        {
            ShipCrashed.EndRound += ShowCrashedTutorial;
            ShipCrashed.RestartShip += HideCrashedTutorial;
        }
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (PlayerPrefs.GetInt("playTutorial") == 1)
        {
            ShipCrashed.EndRound -= ShowCrashedTutorial;
            ShipCrashed.RestartShip -= HideCrashedTutorial;
        }
    }

    private void ShowCrashedTutorial(bool val)
    {
        gameObject.SetActive(true);
    }
    
    private void HideCrashedTutorial()
    {
        gameObject.SetActive(false);
    }
}
