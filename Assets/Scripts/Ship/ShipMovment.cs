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

    private void OnEnable()
    {
        AccelerateCommand.AccelerateShip += AddForce;
        RotateCommand.RotateShip += RotateShip;
        GravityCommand.AddGravity += AddGravity;
    }

      private void AddForce(float force)
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
          yForce += (float) Math.Cos(eularAngle) * force;
          xForce += (float) Math.Sin(eularAngle) * -force;
      }

      private void RotateShip(int rotate)
      {
          rotateDireciton += rotate;
      }

      private void AddGravity(float gravity)
      {
          yForce += gravity;
      }

      private void FixedUpdate()
      {
          transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime * rotateDireciton));
          rotateDireciton = 0;

          //Calculate new velocity V = U + (F / m) t
          velocity.x = velocity.x + (xForce / shipMass) * Time.deltaTime;
          velocity.y = velocity.y + (yForce / shipMass) * Time.deltaTime;

          //Calculate displacement S = 0.5 * (u + v) t
          float newX = 0.5f * velocity.x * Time.deltaTime;
          float newY = 0.5f * velocity.y * Time.deltaTime; 
          transform.position = new Vector3(transform.position.x + newX,transform.position.y + newY, 0);
          xForce = 0;
          yForce = 0;
      }
}