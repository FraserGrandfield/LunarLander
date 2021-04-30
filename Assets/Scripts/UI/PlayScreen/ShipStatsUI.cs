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

    private void OnEnable()
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
        ShipStats.FuelUpdated += UpdateFuel;
        ShipStats.ScoreUpdated += UpdateScore;
        ShipStats.SpeedUpdated += SpeedUpdated;
    }
    
     private void OnDisable()
    {
        ShipStats.FuelUpdated -= UpdateFuel;
        ShipStats.ScoreUpdated -= UpdateScore;
        ShipStats.SpeedUpdated -= SpeedUpdated;
    }

    private void UpdateFuel(int fuel)
    {
        fuelText.text = "Fuel:  " + fuel;
    }
    
    private void UpdateScore(int score)
    {
        scoreText.text = "Score:  " + score;
    }
    
    private void SpeedUpdated(int xSpeed, int ySpeed)
    {
        horizontalSpeedText.text = "Horizontal  Speed:  " + xSpeed;
        verticalSpeedText.text = "Vertical  Speed:  " + ySpeed;
    }
}
