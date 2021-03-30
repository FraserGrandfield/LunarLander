using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovment : MonoBehaviour
{
    private float shipMass = 10;
    private Vector2 velocity;
    private float xForce;
    private float yForce;
    private int rotateDireciton = 0;
    private bool gamePaused;
    private float thrust = 10;
    private bool isAccelerating;

    public static event Action<bool> SaveFrame;
    public static event Action<int> fuelUsed;
    public static event Action<float, float> updateVelocity;

    private void Start()
    {
        gamePaused = false;
    }

    private void OnEnable()
    {
        ShipAccelerating.AccelerateShip += AddForce;
        ShipAccelerating.RotateShip += RotateShip;
        ShipIdle.RotateShip += RotateShip;
        ShipIdle.AcceleratorKeyUp += AcceleratorKeyUp;
        ShipCrashed.PauseShip += PauseGame;
        ShipLanded.PauseShip += PauseGame;
    }

    private void AddForce()
    {
        isAccelerating = true;
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

    private void RotateShip(int rotate)
    {
        rotateDireciton += rotate;
    }

    private void PauseGame()
    {
        gamePaused = !gamePaused;
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
            rotateShip();
            moveShip();
            fuelUpdate();
        }
    }

    private void rotateShip()
    {
        transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime * rotateDireciton));
        rotateDireciton = 0;
    }

    private void moveShip()
    {
        yForce += -2.5f;
        //Calculate new velocity V = U + (F / m) t
        velocity.x = velocity.x + (xForce / shipMass) * Time.deltaTime;
        velocity.y = velocity.y + (yForce / shipMass) * Time.deltaTime;
        updateVelocity?.Invoke(velocity.x, velocity.y);
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
}