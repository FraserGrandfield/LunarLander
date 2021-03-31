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
        MainMenuButton.OnUIButtonClick += GetButtonType;
        ShipLanded.EndGame += showEndGameUI;
        ShipCrashed.EndGame += showEndGameUI;
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    { 
        MainMenuButton.OnUIButtonClick -= GetButtonType;
        ShipLanded.EndGame -= showEndGameUI;
        ShipCrashed.EndGame -= showEndGameUI;
    }

    private void showEndGameUI(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = "Score: " + score;
    }

    private void GetButtonType(String buttonName)
    {
        switch (buttonName)
        {
            case "PlayAgainButton":
                StartCoroutine(ChangeScene("PlayScene"));
                break;
            case "MainMenuButton":
                StartCoroutine(ChangeScene("HomeScreen"));
                break;
            default:
                Debug.Log("Error no button " + buttonName);
                break;
        }
    }

    private static IEnumerator ChangeScene(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
