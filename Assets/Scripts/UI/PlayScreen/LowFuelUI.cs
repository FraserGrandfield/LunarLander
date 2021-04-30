using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowFuelUI : MonoBehaviour
{
    void Start()
    {
        ShipStats.LowFuel += ShowLowFuelUI;
        ShipLanded.EndGame += HideLowFuelUI;
        ShipCrashed.EndGame += HideLowFuelUI;
        ShipPlayReplay.EndOfReplay += HideLowFuelUI;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        ShipStats.LowFuel -= ShowLowFuelUI;
        ShipLanded.EndGame -= HideLowFuelUI;
        ShipCrashed.EndGame -= HideLowFuelUI;
        ShipPlayReplay.EndOfReplay -= HideLowFuelUI;
    }

    private void ShowLowFuelUI(bool isFuelLow)
    {
        gameObject.SetActive(isFuelLow);
    }

    private void HideLowFuelUI(int val)
    {
        gameObject.SetActive(false);
    }
}
