using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundContatct : MonoBehaviour
{
    [SerializeField]private int scoreMultiplier;
    public static event Action<int, float> ShipTouchedGround;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        float groundRotation = transform.rotation.eulerAngles.z;
        ShipTouchedGround?.Invoke(scoreMultiplier, groundRotation);
    }
}
