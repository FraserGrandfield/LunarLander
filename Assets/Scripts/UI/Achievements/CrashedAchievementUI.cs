using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashedAchievementUI : MonoBehaviour
{
    void Start()
    {
        AchievementManager.ShowCrashedAchievement += ShowNotification;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        AchievementManager.ShowCrashedAchievement -= ShowNotification;
    }

    private void ShowNotification()
    {
        gameObject.SetActive(true);
        StartCoroutine(HideNotification());
    }

    private IEnumerator HideNotification()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
