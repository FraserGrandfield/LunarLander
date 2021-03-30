using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    [SerializeField] private int fuel;
    [SerializeField] private int xSpeed;
    [SerializeField] private int ySpeed;
    [SerializeField] private int score;
    private bool shipLanded;
    private bool shipCrashed;
    
    public static event Action<int> FuelUpdated;
    public static event Action<float> XSpeedUpdated;
    public static event Action<float> YSpeedUpdated;
    public static event Action<int> ScoreUpdated;

    void Start()
    {
        updateScore(0);
        fuel = 1500;
        FuelUpdated?.Invoke(fuel);
        xSpeed = 0;
        ySpeed = 0;
        shipLanded = false;
        shipCrashed = false;
    }

    private void Awake()
    {
        ShipMovment.updateVelocity += updateVelocity;
        ShipMovment.fuelUsed += updateFuel;
        ShipCrashed.shipCrashed += calculateShipCrashed;
        ShipLanded.shipLanded += calculateShipLanded;
        GroundContatct.ShipTouchedGround += shipTouchedGround;
        ShipCrashed.RestartShip += restartShip;
        ShipLanded.RestartShip += restartShip;
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
    
    public float getScore()
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
    
    private void updateFuel(int fuelUsed)
    {
        fuel -= fuelUsed;
        if (fuel < 0)
        {
            fuel = 0;
        }
        FuelUpdated?.Invoke(fuel);
    }

    private void updateVelocity(float x, float y)
    {
        xSpeed = (int)(x * 10);
        ySpeed = (int)(y * 10);
        XSpeedUpdated?.Invoke(xSpeed);
        YSpeedUpdated?.Invoke(ySpeed);
    }

    private void updateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        ScoreUpdated?.Invoke(score);
    }

    private void calculateShipCrashed()
    {
        updateFuel(200);
    }
    
    private void calculateShipLanded()
    {
        updateScore(100);
    }
    
    private void shipTouchedGround(bool landed)
    {
        if (landed)
        {
            shipLanded = true;
        }
        else
        {
            shipCrashed = true;
        }
    }
    
    private void restartShip()
    {
        shipCrashed = false;
        shipLanded = false;
    }
}
