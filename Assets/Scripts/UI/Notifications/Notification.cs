using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    private void Start()
    {
        PlayerListManager.ShowNotificaiton += ShowNotification;
        SaveSettingsButton.ShowNotification += ShowNotification;
        HomeScreenPlayButton.ShowNotification += ShowNotification;
        ReplaysListManager.ShowNotificaiton += ShowNotification;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        PlayerListManager.ShowNotificaiton -= ShowNotification;
        SaveSettingsButton.ShowNotification -= ShowNotification;
        HomeScreenPlayButton.ShowNotification -= ShowNotification;
        ReplaysListManager.ShowNotificaiton -= ShowNotification;
    }

    private void ShowNotification(string notificationText)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = notificationText;
        gameObject.SetActive(true);
        StartCoroutine(HideNotification());
    }

    private IEnumerator HideNotification()
    {
        yield return new WaitForSeconds(3); 
        gameObject.SetActive(false);
    }
}
