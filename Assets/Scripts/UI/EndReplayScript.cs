using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndReplayScript : MonoBehaviour
{

    private void Start()
    {
        ShipPlayReplay.EndOfReplay += ShowEndGameUI;
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    { 
        ShipPlayReplay.EndOfReplay -= ShowEndGameUI;
    }

    private void ShowEndGameUI(int score)
    {
        gameObject.SetActive(true);
    }
}