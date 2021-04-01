using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "Score")
            {
                scoreText = gameObject.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            } 
        }
        ShipLanded.EndGame += showEndGameUI;
        ShipCrashed.EndGame += showEndGameUI;
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    { 
        ShipLanded.EndGame -= showEndGameUI;
        ShipCrashed.EndGame -= showEndGameUI;
    }

    private void showEndGameUI(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = "Score: " + score;
    }
}
