using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShipPlayReplay : MonoBehaviour
{
    private MemoryStream _memoryStream;
    private BinaryReader _binaryReader;
    private bool startReplay;
    private bool shipTouchedGround;

    public static event Action<int> UpdateFuel;
    public static event Action<Vector2> UpdateVelocity;
    public static event Action<int> UpdateScore;
    public static event Action<bool> UpdateHasCrashed;
    public static event Action<bool> UpdateHasLanded;
    public static event Action IsAccelerating;
    public static event Action StopedAccelerating;
    public static event Action HasCrashed;
    public static event Action HasLanded;
    public static event Action ResetAnimations;

    private void Start()
    {
        startReplay = false;
        shipTouchedGround = false;
    }

    private void OnEnable()
    {
        ShipReplayReader.ReplayMemoryStream += StartReplay;
    }

    private void OnDisable()
    {
        ShipReplayReader.ReplayMemoryStream -= StartReplay;
    }

    private void StartReplay(MemoryStream memoryStream)
    {
        _memoryStream = memoryStream;
        _memoryStream.Seek(0, SeekOrigin.Begin);
        _binaryReader = new BinaryReader(_memoryStream);
        startReplay = true;
    }
    
    private void ReplayShip()
    {
        float posX = _binaryReader.ReadSingle();
        float posY = _binaryReader.ReadSingle();
        float rotx = _binaryReader.ReadSingle();
        float roty = _binaryReader.ReadSingle();
        float rotz = _binaryReader.ReadSingle();
        float rotw = _binaryReader.ReadSingle();
        bool isAccelerating = _binaryReader.ReadBoolean();
        int fuel = _binaryReader.ReadInt32();
        float xSpeed = _binaryReader.ReadSingle();
        float ySpeed = _binaryReader.ReadSingle();
        int score = _binaryReader.ReadInt32();
        bool hasShipCrashed = _binaryReader.ReadBoolean();
        bool hasShipLanded = _binaryReader.ReadBoolean();
        
        transform.position = new Vector3(posX, posY, 0);
        transform.rotation = new Quaternion(rotx, roty, rotz, rotw);

        if (isAccelerating)
        {
            IsAccelerating?.Invoke();
        }
        else
        {
            StopedAccelerating?.Invoke();
        }
        if (hasShipCrashed)
        {
            HasCrashed?.Invoke();
            shipTouchedGround = true;
        }

        if (hasShipLanded)
        {
            HasLanded?.Invoke();
            shipTouchedGround = true;
        }

        UpdateFuel?.Invoke(fuel);
        UpdateVelocity?.Invoke(new Vector2(xSpeed, ySpeed));
        UpdateScore?.Invoke(score);
        UpdateHasCrashed?.Invoke(hasShipCrashed);
        UpdateHasLanded?.Invoke(hasShipLanded);
        
        if (_memoryStream.Position >= _memoryStream.Length)
        {
            startReplay = false;
        }
    }

    private IEnumerator WaitforShipTouchGround()
    {
        yield return new WaitForSeconds(2);
        ResetAnimations?.Invoke();
        shipTouchedGround = false;
    }
    
    private void FixedUpdate()
    {
        if (startReplay && !shipTouchedGround)
        { 
            ReplayShip();
        } else if (shipTouchedGround)
        {
            StartCoroutine(WaitforShipTouchGround());
        }
    }
}
