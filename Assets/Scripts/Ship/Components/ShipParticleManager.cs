using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipParticleManager : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private void OnEnable()
    {
        _particleSystem = gameObject.GetComponent<ParticleSystem>();
        ShipLanded.shipLanded += StartLandingParticles;
        ShipPlayReplay.HasLanded += StartLandingParticles;
    }

    private void OnDisable()
    {
        ShipLanded.shipLanded -= StartLandingParticles;
        ShipPlayReplay.HasLanded -= StartLandingParticles;
    }

    private void StartLandingParticles()
    {
        _particleSystem.Play();
    }
}
