using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAccelerateUI : MonoBehaviour
{
    void Start()
    {
        TutorialAccelerate.ShowTutorialAccelerateUI += ShowAccelerateTutorial;
        TutorialAccelerate.HideTutorialAccelerateUI += HideAccelerateTutorial;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        TutorialAccelerate.ShowTutorialAccelerateUI -= ShowAccelerateTutorial;
        TutorialAccelerate.HideTutorialAccelerateUI -= HideAccelerateTutorial;
    }

    private void ShowAccelerateTutorial()
    {
        gameObject.SetActive(true);
    }
    
    private void HideAccelerateTutorial()
    {
        gameObject.SetActive(false);
    }
}
