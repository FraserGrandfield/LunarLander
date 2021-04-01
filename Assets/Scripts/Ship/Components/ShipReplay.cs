using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShipReplay : MonoBehaviour
{
    private MemoryStream _memoryStream;
    private BinaryWriter _binaryWriter;
    private BinaryReader _binaryReader;
    private bool startReplay;
    private ShipStats _shipStats;

    private void Start()
    {
        startReplay = false;
        _shipStats = gameObject.GetComponent<ShipStats>();
    }

    private void OnEnable()
    {
        ShipMovment.SaveFrame += SaveShip;
        ShipManager.StartRecording += startRecording;
        ShipReplayManager.StartReplay += StartReplay;
    }

    private void OnDisable()
    {
        ShipMovment.SaveFrame -= SaveShip;
        ShipManager.StartRecording -= startRecording;
        ShipReplayManager.StartReplay -= StartReplay;    }

    private void startRecording()
    {
        _memoryStream = new MemoryStream();
        _binaryWriter = new BinaryWriter(_memoryStream);
        _binaryReader = new BinaryReader(_memoryStream);
        _memoryStream.SetLength(0);
        _memoryStream.Seek(0, SeekOrigin.Begin);
        _binaryWriter.Seek(0, SeekOrigin.Begin);
    }

    private void SaveShip(bool isAccelerating)
    {
        _binaryWriter.Write(transform.position.x);
        _binaryWriter.Write(transform.position.y);
        _binaryWriter.Write(transform.rotation.x);
        _binaryWriter.Write(transform.rotation.y);
        _binaryWriter.Write(transform.rotation.z);
        _binaryWriter.Write(transform.rotation.w);
        _binaryWriter.Write(isAccelerating);
        _binaryWriter.Write(_shipStats.getFuel());
        _binaryWriter.Write(_shipStats.getXSpeed());
        _binaryWriter.Write(_shipStats.getYSpeed());
        _binaryWriter.Write(_shipStats.getScore());
        _binaryWriter.Write(_shipStats.getShipCrashed());
        _binaryWriter.Write(_shipStats.getShipLanded());
    }

    private void StartReplay()
    {
        _memoryStream.Seek(0, SeekOrigin.Begin);
        _binaryWriter.Seek(0, SeekOrigin.Begin);
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
        if (_memoryStream.Position >= _memoryStream.Length)
        {
            startReplay = false;
        }
    }

    private void FixedUpdate()
    {
        if (startReplay)
        {
            ReplayShip();
        }
    }
}
