using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject button;
    private void OnEnable()
    {
        HighScoreReader.DisplayHighScores += DisplayHighScores;
    }

    private void OnDisable()
    {
        HighScoreReader.DisplayHighScores -= DisplayHighScores;
    }

    private void DisplayHighScores(Dictionary<string, int> highscores)
    {
        foreach (KeyValuePair<string, int> highscore in highscores.OrderByDescending(key => key.Value))
        {
            GameObject newButton = Instantiate(button);
            newButton.SetActive(true);
            newButton.GetComponent<TextMeshProUGUI>().text = highscore.Key + " " + highscore.Value;
            newButton.transform.SetParent(transform);
            newButton.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
