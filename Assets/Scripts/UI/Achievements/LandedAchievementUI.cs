using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandedAchievementUI : MonoBehaviour
{
    void Start()
    {
        AchievementManager.showLandedAchievement += ShowNotification;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        AchievementManager.showLandedAchievement -= ShowNotification;
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
