using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneComponent : MonoBehaviour
{
    private void Start()
    {
        MainMenuButton.OnUIButtonClick += GetButtonType;
    }

    private void OnDestroy()
    {
        MainMenuButton.OnUIButtonClick -= GetButtonType;
    }

    private void GetButtonType(String buttonName)
    {
        switch (buttonName)
        {
            case "PlayButton":
                StartCoroutine(ChangeScene("PlayScene"));
                break;
            case "SettingsButton":
                StartCoroutine(ChangeScene("SettingsScene"));
                break;
            case "ChangePlayerButton":
                StartCoroutine(ChangeScene("ChangePlayerScene"));
                break;
            case "ReplaysButton":
                StartCoroutine(ChangeScene("ChooseReplayScene"));
                break;
            case "HighscoreButton":
                StartCoroutine(ChangeScene("HighscoreScene"));
                break;
            case "AchievementsButton":
                StartCoroutine(ChangeScene("AchievementsScene"));
                break;
            case "ExitButton":
                Application.Quit();
                break;
            case "MainMenuButton":
                StartCoroutine(ChangeScene("HomeScreen"));
                break;
            case "PlayAgainButton":
                StartCoroutine(ChangeScene("PlayScene"));
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
