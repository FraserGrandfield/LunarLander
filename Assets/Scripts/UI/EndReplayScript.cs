using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndReplayScript : MonoBehaviour
{

    private void Start()
    {
        ShipPlayReplay.EndOfReplay += showEndGameUI;
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    { 
        ShipPlayReplay.EndOfReplay -= showEndGameUI;
    }

    private void showEndGameUI(int score)
    {
        gameObject.SetActive(true);
    }
}
