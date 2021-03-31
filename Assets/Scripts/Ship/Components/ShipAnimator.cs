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
        ShipCrashed.shipCrashed += shipExpload;
        ShipLanded.shipLanded += AccelerateShipAnimationKeyUp;
        ShipCrashed.RestartShip += resetAnimations;
        ShipLanded.RestartShip += resetAnimations;
    }

    private void OnDisable()
    {
        ShipAccelerating.AccelerateShip -= AccelerateShipAnimation;
        ShipIdle.AcceleratorKeyUp -= AccelerateShipAnimationKeyUp;
        ShipCrashed.shipCrashed -= shipExpload;
        ShipLanded.shipLanded -= AccelerateShipAnimationKeyUp;
        ShipCrashed.RestartShip -= resetAnimations;
        ShipLanded.RestartShip -= resetAnimations;
    }

    private void AccelerateShipAnimation()
    {
        _animator.SetBool("ForceApplied", true);
    }
    
    private void AccelerateShipAnimationKeyUp()
    {   
        _animator.SetBool("ForceApplied", false);
    }

    private void shipExpload()
    {
        _animator.SetBool("Expload", true);
        _animator.SetBool("ForceApplied", false);
        Debug.Log("Crashed animation");
    }

    private void resetAnimations()
    {
        _animator.SetBool("Expload", false);
        _animator.SetBool("ForceApplied", false);
    }
}
