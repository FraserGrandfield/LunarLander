using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeSceneComponent : MonoBehaviour
{
    private void OnEnable()
    {
        MainMenuButton.OnUIButtonClick += GetButtonType;
    }

    private void GetButtonType(String buttonName)
    {
        switch (buttonName)
        {
            case "PlayButton":
                Debug.Log("In play button");
                StartCoroutine(ChangeScene("PlayScene"));
                break;
            case "SettingsButton":
                StartCoroutine(ChangeScene("SettingsScene"));
                break;
            case "ChangePlayerButton":
                StartCoroutine(ChangeScene("ChangePlayerScene"));
                break;
            case "ReplaysButton":
                StartCoroutine(ChangeScene("ReplaysScene"));
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
