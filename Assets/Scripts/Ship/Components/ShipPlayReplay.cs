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
    private bool endOfReplay;
    private bool shipTouchedGround;
    private bool gamePaused;
    private ArrayList replayData = new ArrayList();
    private int pointer;
    
    public static event Action<int> UpdateFuel;
    public static event Action<Vector2> UpdateVelocity;
    public static event Action<int> UpdateScore;
    public static event Action<bool> UpdateHasCrashed;
    public static event Action<bool> UpdateHasLanded;
    public static event Action IsAccelerating;
    public static event Action StopedAccelerating;
    public static event Action HasCrashed;
    public static event Action HasLanded;
    public static event Action ResetReplay;
    public static event Action<int> EndOfReplay;

    private void Start()
    {
        startReplay = false;
        shipTouchedGround = false;
        gamePaused = false;
        pointer = 0;
    }

    private void OnEnable()
    {
        ShipReplayReader.ReplayMemoryStream += StartReplay;
        ShipReplayManager.PauseReplay += PauseGame;
        ShipReplayManager.UnPauseReplay += UnPauseGame;
        ShipReplayManager.LeftKeyPressed += RewindReplay;
        ShipReplayManager.RightKeyPressed += FastForwardReplay;
    }

    private void OnDisable()
    {
        ShipReplayReader.ReplayMemoryStream -= StartReplay;
        ShipReplayManager.PauseReplay -= PauseGame;
        ShipReplayManager.UnPauseReplay -= UnPauseGame;
        ShipReplayManager.LeftKeyPressed -= RewindReplay;
        ShipReplayManager.RightKeyPressed -= FastForwardReplay;
    }

    private void StartReplay(MemoryStream memoryStream)
    {
        _memoryStream = memoryStream;
        
        PopulateReplayDataList();
        startReplay = true;
    }

    private void PauseGame()
    {
        gamePaused = true;
    }

    private void UnPauseGame()
    {
        gamePaused = false;
    }

    private void PopulateReplayDataList()
    {
        _memoryStream.Seek(0, SeekOrigin.Begin);
        _binaryReader = new BinaryReader(_memoryStream);
        while (_memoryStream.Position < _memoryStream.Length)
        {
            ReplayData rp = new ReplayData();
            rp.posx = _binaryReader.ReadSingle();
            rp.posy = _binaryReader.ReadSingle();
            rp.rotx = _binaryReader.ReadSingle();
            rp.roty = _binaryReader.ReadSingle();
            rp.rotz = _binaryReader.ReadSingle();
            rp.rotw = _binaryReader.ReadSingle();
            rp.isAccelerating = _binaryReader.ReadBoolean();
            rp.fuel = _binaryReader.ReadInt32();
            rp.xSpeed = _binaryReader.ReadSingle();
            rp.ySpeed = _binaryReader.ReadSingle();
            rp.score = _binaryReader.ReadInt32();
            rp.hasShipCrashed = _binaryReader.ReadBoolean();
            rp.hasShipLanded = _binaryReader.ReadBoolean();
            replayData.Add(rp);
        }
    }
    
    private void ReplayShip()
    {
        ReplayData rd = (ReplayData)replayData[pointer];
        transform.position = new Vector3(rd.posx, rd.posy, 0);
        transform.rotation = new Quaternion(rd.rotx, rd.roty, rd.rotz, rd.rotw);

        if (rd.isAccelerating)
        {
            IsAccelerating?.Invoke();
        }
        else
        {
            StopedAccelerating?.Invoke();
        }
        if (rd.hasShipCrashed)
        {
            HasCrashed?.Invoke();
            shipTouchedGround = true;
        }

        if (rd.hasShipLanded)
        {
            HasLanded?.Invoke();
            shipTouchedGround = true;
        }

        UpdateFuel?.Invoke(rd.fuel);
        UpdateVelocity?.Invoke(new Vector2(rd.xSpeed / 10, rd.ySpeed / 10));
        UpdateScore?.Invoke(rd.score);
        UpdateHasCrashed?.Invoke(rd.hasShipCrashed);
        UpdateHasLanded?.Invoke(rd.hasShipLanded);
        if (pointer >= replayData.Count - 1)
        {
            Debug.Log("pointer end");
            startReplay = false;
            endOfReplay = true;
            EndOfReplay?.Invoke(0);
        }
        pointer++;
    }

    private void RewindReplay()
    {
        if (pointer - 1 < 0)
        {
            pointer = 0;
        }
        else
        {
            pointer -= 1;
        }
    }

    private void FastForwardReplay()
    {
        if (pointer + 1 > replayData.Count - 1)
        {
            pointer = replayData.Count - 1;
        }
        else
        {
            pointer += 1;
        }
    }

    private IEnumerator WaitforShipTouchGround()
    {
        yield return new WaitForSeconds(2);
        ResetReplay?.Invoke();
        shipTouchedGround = false;
    }
    
    private void FixedUpdate()
    {
        if (startReplay && !shipTouchedGround && !endOfReplay && !gamePaused)
        { 
            ReplayShip();
        } else if (shipTouchedGround && !endOfReplay)
        {
            StartCoroutine(WaitforShipTouchGround());
        } 
    }
}
