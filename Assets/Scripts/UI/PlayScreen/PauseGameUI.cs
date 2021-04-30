using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameUI : MonoBehaviour
{
    void Start()
    {
        ShipPaused.PauseShip += ShowPauseGame;
        ShipPaused.UnPauseShip += HidePauseGame;
        ShipReplayManager.PauseReplay += ShowPauseGame;
        ShipReplayManager.UnPauseReplay += HidePauseGame;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        ShipPaused.PauseShip -= ShowPauseGame;
        ShipPaused.UnPauseShip -= HidePauseGame;
        ShipReplayManager.PauseReplay -= ShowPauseGame;
        ShipReplayManager.UnPauseReplay -= HidePauseGame;
    }

    private void ShowPauseGame()
    {
        Cursor.visible = true;
        gameObject.SetActive(true);
    }

    private void HidePauseGame()
    {
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
}
