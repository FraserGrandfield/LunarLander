using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TouchedGroundUI : MonoBehaviour
{
    private TextMeshProUGUI touchGroundTypeText;
    private void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "TouchedGroundType")
            {
                touchGroundTypeText = gameObject.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            } 
        }
        ShipLanded.EndRound += showEndGameUI;
        ShipCrashed.EndRound += showEndGameUI;
        ShipLanded.RestartShip += hideEndgameUI;
        ShipCrashed.RestartShip += hideEndgameUI;
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    { 
        ShipLanded.EndRound -= showEndGameUI;
        ShipCrashed.EndRound -= showEndGameUI;
        ShipLanded.RestartShip -= hideEndgameUI;
        ShipCrashed.RestartShip -= hideEndgameUI;
    }

    private void showEndGameUI(bool landed)
    {
   
        gameObject.SetActive(true);
        if (landed)
        {
            touchGroundTypeText.text = "Good Landing!";
        }
        else
        {
            touchGroundTypeText.text = "You Crashed!";
        }
    }

    private void hideEndgameUI()
    {
        gameObject.SetActive(false);
    }
}
