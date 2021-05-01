using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTouchGround : MonoBehaviour
{
    public static event Action<bool, int> UpdateLanded;
    public static event Action<string> ShipX4Achievement;

        private void OnEnable()
    {
        GroundContatct.ShipTouchedGround += CalculateTouchGround;
    }

    private void OnDisable()
    {
        GroundContatct.ShipTouchedGround -= CalculateTouchGround;
    }

    private void CalculateTouchGround(int multiplier, float groundRotation)
    {
        float shipRotation = transform.rotation.eulerAngles.z;
        float shipXSpeed = GetComponent<ShipStats>().GetXSpeed();
        float shipYSpeed = GetComponent<ShipStats>().GetYSpeed();
        if (Math.Abs(shipXSpeed) > 7 || Math.Abs(shipYSpeed) > 7 || (shipRotation >= 6  && shipRotation <= 354))
        {
            UpdateLanded?.Invoke(false, multiplier);
        }
        else
        {
            UpdateLanded?.Invoke(true, multiplier);
            if (multiplier == 4)
            {
                ShipX4Achievement?.Invoke("x4");
            }
        }
    }
}
