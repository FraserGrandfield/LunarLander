using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiveHundredAchievementUI : MonoBehaviour
{
    void Start()
    {
        AchievementManager.show500Achievement += ShowNotification;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        AchievementManager.show500Achievement -= ShowNotification;
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
