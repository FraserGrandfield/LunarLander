using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTouchGround : MonoBehaviour
{
    public static event Action<bool, int> updateLanded;

    private void Awake()
    {
        GroundContatct.ShipTouchedGround += calculateTouchGround;
    }

    private void calculateTouchGround(int multiplier, float groundRotation)
    {
        float shipRotation = transform.rotation.eulerAngles.z;
        float rotationDifference = Math.Max(shipRotation, groundRotation) - Math.Min(shipRotation, groundRotation);
        
        float shipXSpeed = GetComponent<ShipStats>().getXSpeed();
        float shipYSpeed = GetComponent<ShipStats>().getYSpeed();

        if (shipXSpeed > 5 || shipYSpeed > 5 || rotationDifference > 5)
        {
            updateLanded?.Invoke(false, multiplier);
        }
        else
        {
            updateLanded?.Invoke(true, multiplier);
        }
    }
}