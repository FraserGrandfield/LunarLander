using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAudio : MonoBehaviour
{
    [SerializeField] private AudioClip AccelerateShip;
    [SerializeField] private AudioClip ShipExplosion;

    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = 1 * (PlayerPrefs.GetInt("volume") / 100);
    }

    private void OnEnable()
    {
        ShipAccelerating.AccelerateShip += playAccelerateSound;
        ShipIdle.AcceleratorKeyUp += stopSound;
        ShipLanded.shipLanded += stopSound;
        ShipCrashed.shipCrashed += stopSound;
        ShipCrashed.shipCrashed += shipExpload;
        ShipCrashed.RestartShip += stopSound;
    }

    private void OnDisable()
    {
        ShipAccelerating.AccelerateShip -= playAccelerateSound;
        ShipIdle.AcceleratorKeyUp -= stopSound;
        ShipLanded.shipLanded -= stopSound;
        ShipCrashed.shipCrashed -= stopSound;
        ShipCrashed.shipCrashed += shipExpload;
        ShipCrashed.RestartShip += stopSound;
    }

    private void playAccelerateSound()
    {
        if (!source.isPlaying)
        {
            source.clip = AccelerateShip;
            source.Play();
        }
    }

    private void stopSound()
    {
        source.Stop();
    }

    private void shipExpload()
    {
        source.clip = ShipExplosion;
        source.Play();
    }
}
