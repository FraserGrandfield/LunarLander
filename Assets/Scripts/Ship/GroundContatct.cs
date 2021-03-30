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
        
        float shipXSpeed = other.GetComponent<ShipManager>().getXSpeed();
        float shipYSpeed = other.GetComponent<ShipManager>().getYSpeed();
        
        if (Math.Abs(shipXSpeed) > 1.5 || Math.Abs(shipYSpeed) > 1.5 || rotationDifference > 5)
        {
            Debug.Log("Ship Crashed");
            ShipTouchedGround?.Invoke(false);
        }
        else
        {
            Debug.Log("Ship Landed");
            ShipTouchedGround?.Invoke(true);
        }
    }
}
