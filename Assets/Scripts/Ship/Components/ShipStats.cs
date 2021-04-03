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
    private bool hasCrashedOnce;
    private bool hasLandedOnce; 
    
    public static event Action<int> FuelUpdated;
    public static event Action<int, int> speedUpdated;
    public static event Action<int> ScoreUpdated;

    void Start()
    {
        AddToScore(0);
        fuel = 1500;
        FuelUpdated?.Invoke(fuel);
        xSpeed = 0;
        ySpeed = 0;
        shipLanded = false;
        shipCrashed = false;
        hasCrashedOnce = false;
        hasLandedOnce = false;
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
        ShipMovment.updateVelocity += updateVelocity;
        ShipMovment.fuelUsed += updateUsedFuel;
        ShipTouchGround.updateLanded += updateTouchedGround;
        ShipCrashed.RestartShip += restartShip;
        ShipLanded.RestartShip += restartShip;
        
        ShipPlayReplay.UpdateFuel += updateFuel;
        ShipPlayReplay.UpdateVelocity += updateVelocity;
        ShipPlayReplay.UpdateScore += updateScore;
        ShipPlayReplay.UpdateHasCrashed += updateHasCrashed;
        ShipPlayReplay.UpdateHasLanded += updateHasLanded;
    }

    private void OnDisable()
    {
        ShipMovment.updateVelocity -= updateVelocity;
        ShipMovment.fuelUsed -= updateUsedFuel;
        ShipTouchGround.updateLanded -= updateTouchedGround;
        ShipCrashed.RestartShip -= restartShip;
        ShipLanded.RestartShip -= restartShip;
        
        ShipPlayReplay.UpdateFuel -= updateFuel;
        ShipPlayReplay.UpdateVelocity -= updateVelocity;
        ShipPlayReplay.UpdateScore -= updateScore;
        ShipPlayReplay.UpdateHasCrashed -= updateHasCrashed;
        ShipPlayReplay.UpdateHasLanded -= updateHasLanded;
    }

    public int getFuel()
    {
        return fuel;
    }

    public float getXSpeed()
    {
        return xSpeed;
    }
    
    public float getYSpeed()
    {
        return ySpeed;
    }

    public Vector2 getRealVelocity()
    {
        return realVelocity;
    }
    
    public int getScore()
    {
        return score;
    }
    
    public bool getShipLanded()
    {
        return shipLanded;
    }
    
    public bool getShipCrashed()
    {
        return shipCrashed;
    }
    
    public bool getPlayTutorial()
    {
        return playTutorial;
    }
    
    public bool getHashCrashedOnce()
    {
        return hasCrashedOnce;
    }
    
    public bool getHasLandedOnce()
    {
        return hasLandedOnce;
    }
    
    private void updateUsedFuel(int fuelUsed)
    {
        fuel -= fuelUsed;
        if (fuel < 0)
        {
            fuel = 0;
        }
        FuelUpdated?.Invoke(fuel);
    }

    private void updateFuel(int currentFuel)
    {
        fuel = currentFuel;
        FuelUpdated?.Invoke(fuel);
    }

    private void updateVelocity(Vector2 velocity)
    {
        xSpeed = (int)(velocity.x * 10);
        ySpeed = (int)(velocity.y * 10);
        realVelocity = velocity;
        speedUpdated?.Invoke(xSpeed, ySpeed);
    }

    private void AddToScore(int scoreToAdd)
    {
        score += scoreToAdd;
        ScoreUpdated?.Invoke(score);
    }

    private void updateScore(int newScore)
    {
        score = newScore;
        ScoreUpdated?.Invoke(score);
    }
    
    private void updateHasCrashed(bool hasCrashed)
    {
        shipCrashed = hasCrashed;
    }
    
    private void updateHasLanded(bool hasLanded)
    {
        shipLanded = hasLanded;
    }

    private void updateTouchedGround(bool landed, int multiplier)
    {
        if (landed)
        {
            shipLanded = true;
            AddToScore(100 * multiplier);
        }
        else
        {
            shipCrashed = true;
            updateUsedFuel(200);
        }
    }
    
    private void restartShip()
    {
        shipCrashed = false;
        shipLanded = false;
    }
}
