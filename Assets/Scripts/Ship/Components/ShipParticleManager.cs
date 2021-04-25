using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipParticleManager : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private void OnEnable()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        ShipLanded.ShipLandedEvent += StartLandingParticles;
        ShipPlayReplay.HasLanded += StartLandingParticles;
    }

    private void OnDisable()
    {
        ShipLanded.ShipLandedEvent -= StartLandingParticles;
        ShipPlayReplay.HasLanded -= StartLandingParticles;
    }

    private void StartLandingParticles()
    {
        particleSystem.Play();
    }
}
