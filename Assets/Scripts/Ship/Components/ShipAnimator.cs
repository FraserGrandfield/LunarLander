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

    private void OnEnable()
    {
        ShipAccelerating.AccelerateShip += AccelerateShipAnimation;
        ShipIdle.AcceleratorKeyUp += AccelerateShipAnimationKeyUp;
        ShipCrashed.ShipCrashedEvent += ShipExpload;
        ShipLanded.ShipLandedEvent += AccelerateShipAnimationKeyUp;
        ShipCrashed.RestartShip += ResetAnimations; 
        ShipLanded.RestartShip += ResetAnimations;
        ShipPlayReplay.IsAccelerating += AccelerateShipAnimation;
        ShipPlayReplay.StopedAccelerating += AccelerateShipAnimationKeyUp;
        ShipPlayReplay.HasCrashed += ShipExpload;
        ShipPlayReplay.ResetReplay += ResetAnimations;
    }

    private void OnDisable()
    {
        ShipAccelerating.AccelerateShip -= AccelerateShipAnimation;
        ShipIdle.AcceleratorKeyUp -= AccelerateShipAnimationKeyUp;
        ShipCrashed.ShipCrashedEvent -= ShipExpload;
        ShipLanded.ShipLandedEvent -= AccelerateShipAnimationKeyUp;
        ShipCrashed.RestartShip -= ResetAnimations;
        ShipLanded.RestartShip -= ResetAnimations;
        ShipPlayReplay.IsAccelerating -= AccelerateShipAnimation;
        ShipPlayReplay.StopedAccelerating -= AccelerateShipAnimationKeyUp;
        ShipPlayReplay.HasCrashed -= ShipExpload;
        ShipPlayReplay.ResetReplay -= ResetAnimations;
    }

    private void AccelerateShipAnimation()
    {
        _animator.SetBool("ForceApplied", true);
    }
    
    private void AccelerateShipAnimationKeyUp()
    {   
        _animator.SetBool("ForceApplied", false);
    }

    private void ShipExpload()
    {
        _animator.SetBool("Expload", true);
        _animator.SetBool("ForceApplied", false);
    }

    private void ResetAnimations()
    {
        _animator.SetBool("Expload", false);
        _animator.SetBool("ForceApplied", false);
    }
}
