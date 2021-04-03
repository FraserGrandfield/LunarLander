using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTurnRightUI : MonoBehaviour
{
    void Start()
    {
        TutorialRotateRight.ShowTutorialTurnRightUI += ShowRotateRightTutorial;
        TutorialRotateRight.HideTutorialTurnRightUI += HideRotateRightTutorial;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        TutorialRotateRight.ShowTutorialTurnRightUI -= ShowRotateRightTutorial;
        TutorialRotateRight.HideTutorialTurnRightUI -= HideRotateRightTutorial;
    }

    private void ShowRotateRightTutorial()
    {
        Debug.Log("display tutorial right");
        gameObject.SetActive(true);
    }
    
    private void HideRotateRightTutorial()
    {
        gameObject.SetActive(false);
    }
}
