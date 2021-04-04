using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovment : MonoBehaviour
{
    private float shipMass = 20;
    private Vector2 velocity;
    private float xForce;
    private float yForce;
    private int rotateDireciton;
    private bool gamePaused;
    private float thrust = 10;
    private float rotateMultiplier = 2;
    private bool isAccelerating;
    private Vector3 spawnPosition = new Vector3(-5, 0, 0);
    private Vector2 spawnVelocity = new Vector2(2, 0);

    public static event Action<bool> SaveFrame;
    public static event Action<int> fuelUsed;
    public static event Action<Vector2> updateVelocity;

    private void Start()
    {
        gamePaused = false;
        xForce = 0;
        yForce = 0;
        rotateDireciton = 0;
        transform.position = spawnPosition;
        velocity = spawnVelocity;
    }

    private void OnEnable()
    {
        ShipAccelerating.AccelerateShip += AddForce;
        ShipAccelerating.RotateShip += RotateShip;
        ShipIdle.RotateShip += RotateShip;
        ShipIdle.AcceleratorKeyUp += AcceleratorKeyUp;
        ShipCrashed.shipCrashed += PauseGame;
        ShipCrashed.RestartShip += restartShip;
        ShipLanded.shipLanded += PauseGame;
        ShipLanded.RestartShip += restartShip;
        ShipPaused.PauseShip += PauseGame;
        ShipPaused.UnPauseShip += UnPauseGame;
        TutorialRotateLeft.RotateShip += RotateShip;
        TutorialRotateLeft.PauseShip += PauseGame;
        TutorialRotateLeft.UnPauseShip += UnPauseGame;
        TutorialRotateRight.RotateShip += RotateShip;
        TutorialRotateRight.PauseShip += PauseGame;
        TutorialRotateRight.UnPauseShip += UnPauseGame;
        TutorialAccelerate.AccelerateShip += AddForce;
        TutorialAccelerate.PauseShip += PauseGame;
        TutorialAccelerate.UnPauseShip += UnPauseGame;
    }

    private void OnDisable()
    {
        ShipAccelerating.AccelerateShip -= AddForce;
        ShipAccelerating.RotateShip -= RotateShip;
        ShipIdle.RotateShip -= RotateShip;
        ShipIdle.AcceleratorKeyUp -= AcceleratorKeyUp;
        ShipCrashed.shipCrashed -= PauseGame;
        ShipCrashed.RestartShip -= restartShip;
        ShipLanded.shipLanded -= PauseGame;
        ShipLanded.RestartShip -= restartShip;
        ShipPaused.PauseShip -= PauseGame;
        ShipPaused.UnPauseShip -= UnPauseGame;
        TutorialRotateLeft.RotateShip -= RotateShip;
        TutorialRotateLeft.PauseShip -= PauseGame;
        TutorialRotateLeft.UnPauseShip -= UnPauseGame;
        TutorialRotateRight.RotateShip -= RotateShip;
        TutorialRotateRight.PauseShip -= PauseGame;
        TutorialRotateRight.UnPauseShip -= UnPauseGame;
        TutorialAccelerate.AccelerateShip -= AddForce;
        TutorialAccelerate.PauseShip -= PauseGame;
        TutorialAccelerate.UnPauseShip -= UnPauseGame;
    }

    private void AddForce()
    {
        isAccelerating = true;
    }

    private void RotateShip(int rotate)
    {
        rotateDireciton = rotate;
    }

    private void PauseGame()
    {
        gamePaused = true;
    }

    private void UnPauseGame()
    {
        gamePaused = false;
    }

    private void AcceleratorKeyUp()
    {
        isAccelerating = false;
    }

    private void FixedUpdate()
    {
        if (!gamePaused) 
        {
            SaveFrame?.Invoke(isAccelerating);
            moveShip();
            rotateShip();
            fuelUpdate();
        }
    }

    private void rotateShip()
    {
        transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime * rotateDireciton * rotateMultiplier));
        rotateDireciton = 0;
    }

    private void moveShip()
    {
        if (isAccelerating)
        {
            double eularAngle = transform.localEulerAngles.z;
            //Convert angle to +/- 180
            if (eularAngle > 180f) 
            { 
                eularAngle = eularAngle - 360;
            }
            //Convert to raidians
            eularAngle = eularAngle * (Math.PI / 180);
            
            //Break force into x and y
            yForce += (float) Math.Cos(eularAngle) * thrust;
            xForce += (float) Math.Sin(eularAngle) * -thrust;
        }
        //Add gravity
        yForce += -2.5f;
        //Calculate new velocity V = U + (F / m) t
        velocity.x = velocity.x + (xForce / shipMass) * Time.deltaTime;
        velocity.y = velocity.y + (yForce / shipMass) * Time.deltaTime;
        updateVelocity?.Invoke(velocity);
        //Calculate displacement S = 0.5 * (u + v) t
        float newX = 0.5f * velocity.x * Time.deltaTime;
        float newY = 0.5f * velocity.y * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + newX, transform.position.y + newY, 0);
        xForce = 0;
        yForce = 0;
    }

    private void fuelUpdate()
    {
        if (isAccelerating)
        {
            fuelUsed?.Invoke(1);
        }
    }

    private void restartShip()
    {
        transform.position = spawnPosition;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        velocity = spawnVelocity;
        xForce = 0;
        yForce = 0;
        isAccelerating = false;
        rotateDireciton = 0;
        gamePaused = false;
    }
}