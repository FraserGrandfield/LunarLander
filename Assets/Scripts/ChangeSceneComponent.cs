using System;
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
                ChangeScene("PlayScene");
                break;
            case "SettingsButton":
                ChangeScene("SettingsScene");
                break;
            case "ChangePlayerButton":
                ChangeScene("ChangePlayerScene");
                break;
            case "ReplaysButton":
                ChangeScene("ReplaysScene");
                break;
            case "HighscoreButton":
                ChangeScene("HighscoreScene");
                break;
            case "AchievementsButton":
                ChangeScene("AchievementsScene");
                break;
            case "ExitButton":
                Application.Quit();
                break;
            default:
                Debug.Log("Error no button " + buttonName);
                break;
        }
    }

    private static void ChangeScene(string sceneName)
    { 
        SceneManager.LoadScene(sceneName);
    }
}
