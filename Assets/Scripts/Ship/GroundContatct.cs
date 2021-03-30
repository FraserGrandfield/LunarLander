using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundContatct : MonoBehaviour
{
    public static event Action<bool> ShipTouchedGround;

    private void OnTriggerEnter2D(Collider2D other)
    {
        float shipRotation = other.gameObject.transform.rotation.eulerAngles.z;
        float groundRotation = transform.rotation.eulerAngles.z;
        float rotationDifference = Math.Max(shipRotation, groundRotation) - Math.Min(shipRotation, groundRotation);
        
        float shipXSpeed = other.GetComponent<ShipStats>().getXSpeed();
        float shipYSpeed = other.GetComponent<ShipStats>().getYSpeed();
        
        if (Math.Abs(shipXSpeed) > 3 || Math.Abs(shipYSpeed) > 3 || rotationDifference > 5)
        {
            ShipTouchedGround?.Invoke(false);
        }
        else
        {
            ShipTouchedGround?.Invoke(true);
        }
    }
}
