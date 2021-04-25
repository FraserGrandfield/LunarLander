using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    [SerializeField] private int fuel;
    [SerializeField] private int xSpeed;
    [SerializeField] private int ySpeed;
    [SerializeField] private Vector2 realVelocity;
    [SerializeField] private int score;
    private bool shipLanded;
    private bool shipCrashed;
    private bool playTutorial;

    public static event Action<int> FuelUpdated;
    public static event Action<int, int> SpeedUpdated;
    public static event Action<int> ScoreUpdated;
    public static event Action<bool> LowFuel;

    void Start()
    {
        AddToScore(0);
        fuel = 1500;
        FuelUpdated?.Invoke(fuel);
        xSpeed = 0;
        ySpeed = 0;
        shipLanded = false;
        shipCrashed = false;
        if (PlayerPrefs.GetInt("playTutorial") == 1)
        {
            playTutorial = true;
        }
        else
        {
            playTutorial = false;
        }
    }

    private void OnEnable()
    {
        ShipMovment.UpdateVelocity += UpdateVelocity;
        ShipMovment.FuelUsed += UpdateUsedFuel;
        ShipTouchGround.UpdateLanded += UpdateTouchedGround;
        ShipCrashed.RestartShip += RestartShip;
        ShipLanded.RestartShip += RestartShip;
        
        ShipPlayReplay.UpdateFuel += UpdateFuel;
        ShipPlayReplay.UpdateVelocity += UpdateVelocity;
        ShipPlayReplay.UpdateScore += UpdateScore;
        ShipPlayReplay.UpdateHasCrashed += UpdateHasCrashed;
        ShipPlayReplay.UpdateHasLanded += UpdateHasLanded;
    }

    private void OnDisable()
    {
        ShipMovment.UpdateVelocity -= UpdateVelocity;
        ShipMovment.FuelUsed -= UpdateUsedFuel;
        ShipTouchGround.UpdateLanded -= UpdateTouchedGround;
        ShipCrashed.RestartShip -= RestartShip;
        ShipLanded.RestartShip -= RestartShip;
        
        ShipPlayReplay.UpdateFuel -= UpdateFuel;
        ShipPlayReplay.UpdateVelocity -= UpdateVelocity;
        ShipPlayReplay.UpdateScore -= UpdateScore;
        ShipPlayReplay.UpdateHasCrashed -= UpdateHasCrashed;
        ShipPlayReplay.UpdateHasLanded -= UpdateHasLanded;
    }

    public int GetFuel()
    {
        return fuel;
    }

    public float GetXSpeed()
    {
        return xSpeed;
    }
    
    public float GetYSpeed()
    {
        return ySpeed;
    }

    public Vector2 GetRealVelocity()
    {
        return realVelocity;
    }
    
    public int GetScore()
    {
        return score;
    }
    
    public bool GetShipLanded()
    {
        return shipLanded;
    }
    
    public bool GetShipCrashed()
    {
        return shipCrashed;
    }
    
    public bool GetPlayTutorial()
    {
        return playTutorial;
    }

    private void UpdateUsedFuel(int fuelUsed)
    {
        fuel -= fuelUsed;
        if (fuel < 0)
        {
            fuel = 0;
        }

        if (fuel < 200)
        {
            LowFuel?.Invoke(true);
        }
        else
        {
            LowFuel?.Invoke(false);
        }
        FuelUpdated?.Invoke(fuel);
    }

    private void UpdateFuel(int currentFuel)
    {
        fuel = currentFuel;
        if (fuel < 200)
        {
            LowFuel?.Invoke(true);
        }
        else
        {
            LowFuel?.Invoke(false);
        }
        FuelUpdated?.Invoke(fuel);
    }

    private void UpdateVelocity(Vector2 velocity)
    {
        xSpeed = (int)(velocity.x * 10);
        ySpeed = (int)(velocity.y * 10);
        realVelocity = velocity;
        SpeedUpdated?.Invoke(xSpeed, ySpeed);
    }

    private void AddToScore(int scoreToAdd)
    {
        score += scoreToAdd;
        ScoreUpdated?.Invoke(score);
    }

    private void UpdateScore(int newScore)
    {
        score = newScore;
        ScoreUpdated?.Invoke(score);
    }
    
    private void UpdateHasCrashed(bool hasCrashed)
    {
        shipCrashed = hasCrashed;
    }
    
    private void UpdateHasLanded(bool hasLanded)
    {
        shipLanded = hasLanded;
    }

    private void UpdateTouchedGround(bool landed, int multiplier)
    {
        if (landed)
        {
            shipLanded = true;
            AddToScore(100 * multiplier);
        }
        else
        {
            shipCrashed = true;
            UpdateUsedFuel(200);
        }
    }
    
    private void RestartShip()
    {
        shipCrashed = false;
        shipLanded = false;
    }
}
