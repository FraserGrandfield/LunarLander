using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XFourAchievementUI : MonoBehaviour
{
    void Start()
    {
        AchievementManager.ShowX4Achievement += ShowNotification;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        AchievementManager.ShowX4Achievement -= ShowNotification;
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
