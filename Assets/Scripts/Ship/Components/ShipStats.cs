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
    
    public static event Action<int> FuelUpdated;
    public static event Action<int, int> speedUpdated;
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

    private void OnEnable()
    {
        ShipMovment.updateVelocity += updateVelocity;
        ShipMovment.fuelUsed += updateFuel;
        ShipTouchGround.updateLanded += updateTouchedGround;
        ShipCrashed.RestartShip += restartShip;
        ShipLanded.RestartShip += restartShip;
    }

    private void OnDisable()
    {
        ShipMovment.updateVelocity -= updateVelocity;
        ShipMovment.fuelUsed -= updateFuel;
        ShipTouchGround.updateLanded -= updateTouchedGround;
        ShipCrashed.RestartShip -= restartShip;
        ShipLanded.RestartShip -= restartShip;
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
    
    private void updateFuel(int fuelUsed)
    {
        fuel -= fuelUsed;
        if (fuel < 0)
        {
            fuel = 0;
        }
        FuelUpdated?.Invoke(fuel);
    }

    private void updateVelocity(Vector2 velocity)
    {
        xSpeed = (int)(velocity.x * 10);
        ySpeed = (int)(velocity.y * 10);
        realVelocity = velocity;
        speedUpdated?.Invoke(xSpeed, ySpeed);
    }

    private void updateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        ScoreUpdated?.Invoke(score);
    }

    private void updateTouchedGround(bool landed, int multiplier)
    {
        if (landed)
        {
            shipLanded = true;
            updateScore(100 * multiplier);
        }
        else
        {
            shipCrashed = true;
            updateFuel(200);
        }
    }
    
    private void restartShip()
    {
        shipCrashed = false;
        shipLanded = false;
    }
}
