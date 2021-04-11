using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayData
{
    public float posx { get; set; }
    public float posy { get; set; }
    public float rotx { get; set; }
    public float roty { get; set; }
    public float rotz { get; set; }
    public float rotw { get; set; }
    public bool isAccelerating { get; set; }
    public int fuel { get; set; }
    public float xSpeed { get; set; }
    public float ySpeed { get; set; }
    public int score { get; set; }
    public bool hasShipCrashed { get; set; }
    public bool hasShipLanded { get; set; }
    public float cameraX { get; set; }
    public float cameraY { get; set; }
}
