using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShipPlayReplay : MonoBehaviour
{
    private MemoryStream memoryStream;
    private BinaryReader binaryReader;
    private bool startReplay;
    private bool endOfReplay;
    private bool shipTouchedGround;
    private bool gamePaused;
    private ArrayList replayData = new ArrayList();
    private int pointer;
    private Camera camera;
    
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
        camera = Camera.main;
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
        this.memoryStream = memoryStream;
        
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
        memoryStream.Seek(0, SeekOrigin.Begin);
        binaryReader = new BinaryReader(memoryStream);
        while (memoryStream.Position < memoryStream.Length)
        {
            ReplayData rp = new ReplayData();
            rp.posx = binaryReader.ReadSingle();
            rp.posy = binaryReader.ReadSingle();
            rp.rotx = binaryReader.ReadSingle();
            rp.roty = binaryReader.ReadSingle();
            rp.rotz = binaryReader.ReadSingle();
            rp.rotw = binaryReader.ReadSingle();
            rp.isAccelerating = binaryReader.ReadBoolean();
            rp.fuel = binaryReader.ReadInt32();
            rp.xSpeed = binaryReader.ReadSingle();
            rp.ySpeed = binaryReader.ReadSingle();
            rp.score = binaryReader.ReadInt32();
            rp.hasShipCrashed = binaryReader.ReadBoolean();
            rp.hasShipLanded = binaryReader.ReadBoolean();
            rp.cameraX = binaryReader.ReadSingle();
            rp.cameraY = binaryReader.ReadSingle();
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

        camera.gameObject.transform.position = new Vector3(rd.cameraX, rd.cameraY, -10);

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
        if (!shipTouchedGround)
        {
            if (pointer - 50 < 0)
            {
                pointer = 0;
            }
            else
            {
                pointer -= 50;
            }
        }
    }

    private void FastForwardReplay()
    {
        if (!shipTouchedGround)
        {
          if (pointer + 50 > replayData.Count - 1)
          {
              pointer = replayData.Count - 1;
          }
          else
          {
              pointer += 50;
          }  
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
