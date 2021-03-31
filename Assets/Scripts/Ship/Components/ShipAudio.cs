using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAudio : MonoBehaviour
{
    [SerializeField] private AudioClip AccelerateShipAudio;
    [SerializeField] private AudioClip ShipExplosionAudio;
    [SerializeField] private AudioClip ShipLandedAudio;

    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = (float)PlayerPrefs.GetInt("volume") / 100;
    }

    private void OnEnable()
    {
        ShipAccelerating.AccelerateShip += playAccelerateSound;
        ShipIdle.AcceleratorKeyUp += stopSound;
        ShipLanded.shipLanded += stopSound;
        ShipCrashed.shipCrashed += stopSound;
        ShipCrashed.shipCrashed += playShipExploadSound;
        ShipCrashed.RestartShip += stopSound;
        ShipLanded.shipLanded += playShipLandedSound;
        ShipLanded.RestartShip += stopSound;
    }

    private void OnDisable()
    {
        ShipAccelerating.AccelerateShip -= playAccelerateSound;
        ShipIdle.AcceleratorKeyUp -= stopSound;
        ShipLanded.shipLanded -= stopSound;
        ShipCrashed.shipCrashed -= stopSound;
        ShipCrashed.shipCrashed -= playShipExploadSound;
        ShipCrashed.RestartShip -= stopSound;
        ShipLanded.shipLanded -= playShipLandedSound;
        ShipLanded.RestartShip -= stopSound;
    }

    private void playAccelerateSound()
    {
        if (!source.isPlaying)
        {
            source.clip = AccelerateShipAudio;
            source.Play();
        }
    }

    private void stopSound()
    {
        source.Stop();
    }

    private void playShipExploadSound()
    {
        source.clip = ShipExplosionAudio;
        source.Play();
    }

    private void playShipLandedSound()
    {
        source.clip = ShipLandedAudio;
        source.Play();
    }
}
