using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipStatsUI : MonoBehaviour
{
    private TextMeshProUGUI fuelText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI horizontalSpeedText;
    private TextMeshProUGUI verticalSpeedText;

    private void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "Fuel")
            {
                fuelText = gameObject.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            } else if (gameObject.transform.GetChild(i).name == "Score")
            {
                scoreText = gameObject.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            } else if (gameObject.transform.GetChild(i).name == "HorizontalSpeed")
            {
                horizontalSpeedText = gameObject.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            }else if (gameObject.transform.GetChild(i).name == "VerticalSpeed")
            {
                verticalSpeedText = gameObject.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            }
        }
    }

    private void Awake()
    {
        ShipStats.FuelUpdated += updateFuel;
        ShipStats.ScoreUpdated += updateScore;
        ShipStats.XSpeedUpdated += horizontalSpeedUpdate;
        ShipStats.YSpeedUpdated += verticalSpeedUpdate;
    }

    private void updateFuel(int fuel)
    {
        fuelText.text = "Fuel: " + fuel;
    }
    
    private void updateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
    
    private void horizontalSpeedUpdate(float speed)
    {
        horizontalSpeedText.text = "Horizontal Speed: " + (int)speed;
    }
    
    private void verticalSpeedUpdate(float speed)
    {
        verticalSpeedText.text = "Vertical Speed: " + (int)speed;
    }
}
