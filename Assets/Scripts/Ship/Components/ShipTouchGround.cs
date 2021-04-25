using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTouchGround : MonoBehaviour
{
    public static event Action<bool, int> updateLanded;
    public static event Action<string> shipX4Achievement;

        private void OnEnable()
    {
        GroundContatct.ShipTouchedGround += calculateTouchGround;
    }

    private void OnDisable()
    {
        GroundContatct.ShipTouchedGround -= calculateTouchGround;
    }

    private void calculateTouchGround(int multiplier, float groundRotation)
    {
        float shipRotation = transform.rotation.eulerAngles.z;
        float rotationDifference = Math.Max(shipRotation, groundRotation) - Math.Min(shipRotation, groundRotation);
        
        float shipXSpeed = GetComponent<ShipStats>().getXSpeed();
        float shipYSpeed = GetComponent<ShipStats>().getYSpeed();
        if (Math.Abs(shipXSpeed) > 7 || Math.Abs(shipYSpeed) > 7 || rotationDifference > 7)
        {
            updateLanded?.Invoke(false, multiplier);
        }
        else
        {
            updateLanded?.Invoke(true, multiplier);
            if (multiplier == 4)
            {
                shipX4Achievement?.Invoke("x4");
            }
        }
    }
}
