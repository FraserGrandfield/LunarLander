using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ShipRecordReplay : MonoBehaviour
{
    private MemoryStream memoryStream;
    private BinaryWriter binaryWriter;
    private ShipStats shipStats;
    private Camera camera;

    public static event Action<string> LoadNewScene; 
    private void Start()
    {
        shipStats = gameObject.GetComponent<ShipStats>();
        camera = Camera.main;
    }
    
    private void OnEnable()
    {
        ShipMovment.SaveFrame += SaveShip;
        ShipManager.StartRecording += StartRecording;
        SaveGameMainMenuButton.SaveGameMainMenu += SaveData;
        SaveGamePlayAgainButton.SaveGamePlayAgain += SaveData;
        ShipLanded.SaveShipLanded += SaveShip;
        ShipCrashed.SaveShipCrashed += SaveShip;
    }

    private void OnDisable()
    {
        ShipMovment.SaveFrame -= SaveShip;
        ShipManager.StartRecording -= StartRecording;
        SaveGameMainMenuButton.SaveGameMainMenu -= SaveData;
        SaveGamePlayAgainButton.SaveGamePlayAgain -= SaveData;
        ShipLanded.SaveShipLanded -= SaveShip;
        ShipCrashed.SaveShipCrashed -= SaveShip;
    }
    
    private void StartRecording()
    {
        memoryStream = new MemoryStream();
        binaryWriter = new BinaryWriter(memoryStream);
        memoryStream.SetLength(0);
        memoryStream.Seek(0, SeekOrigin.Begin);
        binaryWriter.Seek(0, SeekOrigin.Begin);
    }
    
    private void SaveShip(bool isAccelerating)
    {
        binaryWriter.Write(transform.position.x);
        binaryWriter.Write(transform.position.y);
        binaryWriter.Write(transform.rotation.x);
        binaryWriter.Write(transform.rotation.y);
        binaryWriter.Write(transform.rotation.z);
        binaryWriter.Write(transform.rotation.w);
        binaryWriter.Write(isAccelerating);
        binaryWriter.Write(shipStats.GetFuel());
        binaryWriter.Write(shipStats.GetXSpeed());
        binaryWriter.Write(shipStats.GetYSpeed());
        binaryWriter.Write(shipStats.GetScore());
        binaryWriter.Write(shipStats.GetShipCrashed());
        binaryWriter.Write(shipStats.GetShipLanded());
        binaryWriter.Write(camera.gameObject.transform.position.x);
        binaryWriter.Write(camera.gameObject.transform.position.y);
    }

    private void SaveData(bool playAgain)
    {
        if (PlayerPrefs.GetInt("playTutorial") != 1)
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
            bf.Serialize(file, memoryStream);
            file.Close();
        }
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
