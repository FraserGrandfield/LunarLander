using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipReplayManager : MonoBehaviour
{
    public static event Action StartReplay;
    
    private void Start()
    {
        //TODO get the memory stream and parse it into start Replay!
        StartReplay?.Invoke();
    }
}
