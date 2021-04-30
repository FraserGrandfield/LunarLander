using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLandedUI : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("playTutorial") == 1)
        { 
            ShipLanded.EndRound += ShowLandedTutorial;
            ShipLanded.RestartShip += HideLandedTutorial;
        }
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (PlayerPrefs.GetInt("playTutorial") == 1)
        {
            ShipLanded.EndRound -= ShowLandedTutorial;
            ShipLanded.RestartShip -= HideLandedTutorial;
        }
    }

    private void ShowLandedTutorial(bool val)
    {
        gameObject.SetActive(true);
    }
    
    private void HideLandedTutorial()
    {
        gameObject.SetActive(false);
    }
}
