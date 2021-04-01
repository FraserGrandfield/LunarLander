using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ShipRecordReplay : MonoBehaviour
{
    private MemoryStream _memoryStream;
    private BinaryWriter _binaryWriter;
    private ShipStats _shipStats;

    public static event Action<string> LoadNewScene; 
    private void Start()
    {
        _shipStats = gameObject.GetComponent<ShipStats>();
    }
    
    private void OnEnable()
    {
        ShipMovment.SaveFrame += SaveShip;
        ShipManager.StartRecording += startRecording;
        SaveGameMainMenuButton.SaveGameMainMenu += SaveData;
        SaveGamePlayAgainButton.SaveGamePlayAgain += SaveData;
        ShipLanded.SaveShipLanded += SaveShip;
        ShipCrashed.SaveShipCrashed += SaveShip;
    }

    private void OnDisable()
    {
        ShipMovment.SaveFrame -= SaveShip;
        ShipManager.StartRecording -= startRecording;
        SaveGameMainMenuButton.SaveGameMainMenu -= SaveData;
        SaveGamePlayAgainButton.SaveGamePlayAgain -= SaveData;
        ShipLanded.SaveShipLanded -= SaveShip;
        ShipCrashed.SaveShipCrashed -= SaveShip;
    }
    
    private void startRecording()
    {
        _memoryStream = new MemoryStream();
        _binaryWriter = new BinaryWriter(_memoryStream);
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

    private void SaveData(bool playAgain)
    {
        string dateTime = DateTime.Now.ToString();
        dateTime = dateTime.Replace('/', '-');
        dateTime = dateTime.Replace(' ', '-');
        dateTime = dateTime.Replace(':', '-');
        string filePathStr = "/" + dateTime + "Replay" + PlayerPrefs.GetString("name") + ".dat";
        string filePath = Application.persistentDataPath + filePathStr;
        Debug.Log(filePath);
        FileStream file;

        if (File.Exists(filePath))
        {
            file = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        }
        else
        {
            file = File.Create(filePath);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, _memoryStream);
        file.Close();
        if (playAgain)
        {
            LoadNewScene?.Invoke("PlayAgainButton");
        }
        else
        {
            LoadNewScene?.Invoke("MainMenuButton");
        }
    }
}
