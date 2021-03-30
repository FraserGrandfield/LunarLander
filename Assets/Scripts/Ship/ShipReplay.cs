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

    private void Start()
    {
        startReplay = false;
    }

    private void Awake()
    {
        ShipMovment.SaveFrame += SaveShip;
        ShipManager.StartRecording += startRecording;
        ShipReplayManager.StartReplay += StartReplay;
        
    }

    private void startRecording()
    {
        _memoryStream = new MemoryStream();
        _binaryWriter = new BinaryWriter(_memoryStream);
        _binaryReader = new BinaryReader(_memoryStream);
        _memoryStream.SetLength(0);
        _memoryStream.Seek(0, SeekOrigin.Begin);
        _binaryWriter.Seek(0, SeekOrigin.Begin);
    }

    private void SaveShip()
    {
        _binaryWriter.Write(transform.position.x);
        _binaryWriter.Write(transform.position.y);
        _binaryWriter.Write(transform.rotation.x);
        _binaryWriter.Write(transform.rotation.y);
        _binaryWriter.Write(transform.rotation.z);
        _binaryWriter.Write(transform.rotation.w);
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
