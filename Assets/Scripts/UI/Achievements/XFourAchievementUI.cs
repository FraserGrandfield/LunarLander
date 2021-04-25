using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XFourAchievementUI : MonoBehaviour
{
    void Start()
    {
        AchievementManager.showX4Achievement += ShowNotification;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        AchievementManager.showX4Achievement -= ShowNotification;
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
