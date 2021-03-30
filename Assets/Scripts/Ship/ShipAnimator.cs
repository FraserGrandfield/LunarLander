using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Awake()
    {
        ShipAccelerating.AccelerateShip += AccelerateShipAnimation;
        ShipIdle.AcceleratorKeyUp += AccelerateShipAnimationKeyUp;
    }

    private void AccelerateShipAnimation()
    {
        _animator.SetBool("ForceApplied", true);
    }
    
    private void AccelerateShipAnimationKeyUp()
    {   
        _animator.SetBool("ForceApplied", false);
    }
}
