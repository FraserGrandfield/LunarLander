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
        ShipManager.AccelerateShip += AccelerateShipAnimation;
        ShipManager.AcceleratorKeyUp += AccelerateShipAnimationKeyUp;
    }

    private void AccelerateShipAnimation(float force)
    {
        Debug.Log("HEREREE1111");
        _animator.SetBool("ForceApplied", true);
    }
    
    private void AccelerateShipAnimationKeyUp()
    {   
        Debug.Log("HERER222222");
        _animator.SetBool("ForceApplied", false);
    }
}
