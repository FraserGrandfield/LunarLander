using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneThousandAchievementUI : MonoBehaviour
{
    void Start()
    {
        AchievementManager.show1000Achievement += ShowNotification;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        AchievementManager.show1000Achievement -= ShowNotification;
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
