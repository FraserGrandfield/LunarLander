using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    [SerializeField] public int fuel;
    [SerializeField] public float xSpeed;
    [SerializeField] public float ySpeed;
    [SerializeField] public float score;
    
    void Start()
    {
        fuel = 1500;
        xSpeed = 0f;
        ySpeed = 0f;
        score = 0f;
    }

    private void Awake()
    {
        ShipMovment.updateVelocity += updateVelocity;
        ShipMovment.fuelUsed += updateFuel;
        ShipCrashed.shipCrashed += calculateShipCrashed;
        ShipLanded.shipLanded += calculateShipLanded;
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

    private void updateFuel(int fuelUsed)
    {
        fuel -= fuelUsed;
        if (fuel < 0)
        {
            fuel = 0;
        }
    }

    private void updateVelocity(float x, float y)
    {
        xSpeed = x;
        ySpeed = y;
    }

    private void calculateShipCrashed()
    {
        fuel -= 200;
        if (fuel < 0)
        {
            fuel = 0;
        }
    }
    
    private void calculateShipLanded()
    {
        score += 100;
    }
}
