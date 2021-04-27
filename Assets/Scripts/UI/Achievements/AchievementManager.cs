using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private Queue<string> achievementQueue = new Queue<string>();

    public static event Action ShowLandedAchievement;
    public static event Action ShowCrashedAchievement;
    public static event Action Show500Achievement;
    public static event Action Show1000Achievement;
    public static event Action ShowX4Achievement;

    void Start()
    {
        StartCoroutine(AchievementQueueCheck());
        ShipCrashed.ShipCrashedAchievement += CheckToUnlockAchievement;
        ShipLanded.ShipLandedAchievement += CheckToUnlockAchievement;
        ShipTouchGround.ShipX4Achievement += CheckToUnlockAchievement;
    }

    private void OnDestroy()
    {
        ShipCrashed.ShipCrashedAchievement -= CheckToUnlockAchievement;
        ShipLanded.ShipLandedAchievement -= CheckToUnlockAchievement;
        ShipTouchGround.ShipX4Achievement -= CheckToUnlockAchievement;
    }

    private void CheckToUnlockAchievement(string achievement)
    {
        string filePath = Application.persistentDataPath + "/" + PlayerPrefs.GetString("name") + "PlayerAchievmentData.dat";

        if (File.Exists(filePath))
        {
            FileStream file = File.OpenRead(filePath);
            BinaryFormatter bf = new BinaryFormatter();
            Dictionary<string, bool> achievementDic = (Dictionary<string, bool>)bf.Deserialize(file);
            file.Close();
            if (!achievementDic[achievement])
            {
                SaveUnlockedAchievement(achievement);
                achievementQueue.Enqueue(achievement);
            }
        }
    }

    private void DisplayAchievement(string achievement)
    {
        switch (achievement)
        {
            case "landed":
                ShowLandedAchievement?.Invoke();
                break;
            case "crashed":
                ShowCrashedAchievement?.Invoke();
                break;
            case "500":
                Show500Achievement?.Invoke();
                break;
            case "1000":
                Show1000Achievement?.Invoke();
                break;
            case "x4":
                ShowX4Achievement?.Invoke();
                break;
        }
    }

    private void SaveUnlockedAchievement(string achievement)
    {
        string filePath = Application.persistentDataPath + "/" + PlayerPrefs.GetString("name") + "PlayerAchievmentData.dat";
        FileStream file;
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(filePath))
        {
            file = File.OpenRead(filePath);
            Dictionary<string, bool> achievementDic = (Dictionary<string, bool>)bf.Deserialize(file);
            file.Close();
            achievementDic[achievement] = transform;
            file = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            bf.Serialize(file, achievementDic);
            file.Close();
        }
    }

    private IEnumerator AchievementQueueCheck()
    {
        for (; ;)
        {
            if (achievementQueue.Count > 0)
            {
                DisplayAchievement(achievementQueue.Dequeue());
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
