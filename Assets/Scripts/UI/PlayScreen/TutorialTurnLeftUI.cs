using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTurnLeftUI : MonoBehaviour
{
    void Start()
    {
        TutorialRotateLeft.ShowTutorialTurnLeftUI += ShowRotateLeftTutorial;
        TutorialRotateLeft.HideTutorialTurnLeftUI += HideRotateLeftTutorial;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        TutorialRotateLeft.ShowTutorialTurnLeftUI -= ShowRotateLeftTutorial;
        TutorialRotateLeft.HideTutorialTurnLeftUI -= HideRotateLeftTutorial;
    }

    private void ShowRotateLeftTutorial()
    {
        gameObject.SetActive(true);
    }
    
    private void HideRotateLeftTutorial()
    {
        gameObject.SetActive(false);
    }
}
