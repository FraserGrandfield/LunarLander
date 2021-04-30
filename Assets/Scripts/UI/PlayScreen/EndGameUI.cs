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
        ShipLanded.EndGame += ShowEndGameUI;
        ShipCrashed.EndGame += ShowEndGameUI;
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    { 
        ShipLanded.EndGame -= ShowEndGameUI;
        ShipCrashed.EndGame -= ShowEndGameUI;
    }

    private void ShowEndGameUI(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = "Score:  " + score;
    }
}
