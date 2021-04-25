using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraFollowShip : MonoBehaviour
{
    private Vector2 shipVelocity;
    private int cameraBoundry = 420;
    private float smoothTime = 20f;
    private Vector2 shipPos;
    private bool gameStopped;
    private float yClampValue = -12f;
    [SerializeField] private GameObject ship;
    [SerializeField] private UnityEngine.Camera main;

    private void Start()
    {
        gameStopped = false;
        shipPos = main.WorldToScreenPoint(ship.transform.position);
        shipVelocity = ship.gameObject.GetComponent<ShipStats>().GetRealVelocity();
    }

    private void OnEnable()
    {
        ShipCrashed.EndGame += GameEnd;
        ShipCrashed.EndRound += RoundEnd;
        ShipLanded.EndGame += GameEnd;
        ShipLanded.EndRound += RoundEnd;
        ShipMovment.MoveCameraToSpawnPoint += ResetCamera;
    }
    
    private void OnDisable()
    {
        ShipCrashed.EndGame -= GameEnd;
        ShipCrashed.EndRound -= RoundEnd;
        ShipLanded.EndGame -= GameEnd;
        ShipLanded.EndRound -= RoundEnd;
        ShipMovment.MoveCameraToSpawnPoint -= ResetCamera;
    }

    private void GameEnd(int val)
    {
        gameStopped = true;
    }
    
    private void RoundEnd(bool val)
    {
        gameStopped = true;
    }

    private void ResetCamera(Vector3 pos)
    {
        transform.position = pos;
        gameStopped = false;
    }
    
    private void FixedUpdate()
    {
        if (!gameStopped)
        {
            shipVelocity = ship.gameObject.GetComponent<ShipStats>().GetRealVelocity();
            shipPos = main.WorldToScreenPoint(ship.transform.position);
            float newX = 0.5f * shipVelocity.x * Time.deltaTime;
            float newY = 0.5f * shipVelocity.y * Time.deltaTime;
            Vector3 cameraPos = transform.position;
            bool changed = false;
            if (shipPos.x < cameraBoundry)
            {
                cameraPos.x = cameraPos.x + newX;
                changed = true;
            }
            if (shipPos.y < cameraBoundry && cameraPos.y > yClampValue + 0.5f)
            {
                cameraPos.y = cameraPos.y + newY;
                changed = true;
            }
            if (shipPos.x > Screen.width - cameraBoundry)
            { 
                cameraPos.x = cameraPos.x + newX;
                changed = true;
            }
            if (shipPos.y > Screen.height - cameraBoundry)
            { 
                cameraPos.y = cameraPos.y + newY;
                changed = true;
            }

            if (changed)
            {
                cameraPos.y = Mathf.Clamp(cameraPos.y, yClampValue, 99999f);
                Vector3 tempShipVelocity = new Vector3(shipVelocity.x, shipVelocity.y, 0);
                if (cameraPos.y == yClampValue)
                {
                    tempShipVelocity.y = 0;
                }
                transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref tempShipVelocity, smoothTime);
            }
        }
    }
}
