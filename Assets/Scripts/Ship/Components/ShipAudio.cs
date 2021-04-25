using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAudio : MonoBehaviour
{
    [SerializeField] private AudioClip accelerateShipAudio;
    [SerializeField] private AudioClip shipExplosionAudio;
    [SerializeField] private AudioClip shipLandedAudio;

    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = (float)PlayerPrefs.GetInt("gameVolume") / 100;
    }

    private void OnEnable()
    {
        ShipAccelerating.AccelerateShip += PlayAccelerateSound;
        ShipIdle.AcceleratorKeyUp += StopSound;
        ShipLanded.ShipLandedEvent += StopSound;
        ShipCrashed.ShipCrashedEvent += StopSound;
        ShipCrashed.ShipCrashedEvent += PlayShipExploadSound;
        ShipCrashed.RestartShip += StopSound;
        ShipLanded.ShipLandedEvent += PlayShipLandedSound;
        ShipLanded.RestartShip += StopSound;
        ShipPaused.PauseShip += StopSound;
        ShipPlayReplay.IsAccelerating += PlayAccelerateSound;
        ShipPlayReplay.StopedAccelerating += StopSound;
        ShipPlayReplay.HasCrashed += PlayShipExploadSound;
        ShipPlayReplay.HasLanded += PlayShipLandedSound;
        ShipReplayManager.PauseReplay += StopSound;
    }

    private void OnDisable()
    {
        ShipAccelerating.AccelerateShip -= PlayAccelerateSound;
        ShipIdle.AcceleratorKeyUp -= StopSound;
        ShipLanded.ShipLandedEvent -= StopSound;
        ShipCrashed.ShipCrashedEvent -= StopSound;
        ShipCrashed.ShipCrashedEvent -= PlayShipExploadSound;
        ShipCrashed.RestartShip -= StopSound;
        ShipLanded.ShipLandedEvent -= PlayShipLandedSound;
        ShipLanded.RestartShip -= StopSound;
        ShipPaused.PauseShip -= StopSound;
        ShipPlayReplay.IsAccelerating -= PlayAccelerateSound;
        ShipPlayReplay.StopedAccelerating -= StopSound;
        ShipPlayReplay.HasCrashed -= PlayShipExploadSound;
        ShipPlayReplay.HasLanded -= PlayShipLandedSound;
        ShipReplayManager.PauseReplay -= StopSound;
    }

    private void PlayAccelerateSound()
    {
        if (!source.isPlaying)
        {
            source.clip = accelerateShipAudio;
            source.loop = true;
            source.Play();
        }
    }

    private void StopSound()
    {
        source.Stop();
    }

    private void PlayShipExploadSound()
    {
        source.clip = shipExplosionAudio;
        source.loop = false;
        source.Play();
    }

    private void PlayShipLandedSound()
    {
        source.clip = shipLandedAudio;
        source.loop = false;
        source.Play();
    }
}
