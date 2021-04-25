using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private Queue<string> achievementQueue = new Queue<string>();

    public static event Action showLandedAchievement;
    public static event Action showCrashedAchievement;
    public static event Action show500Achievement;
    public static event Action show1000Achievement;
    public static event Action showX4Achievement;

    void Start()
    {
        StartCoroutine(AchievementQueueCheck());
        ShipCrashed.shipCrashedAchievement += CheckToUnlockAchievement;
        ShipLanded.shipLandedAchievement += CheckToUnlockAchievement;
        ShipTouchGround.shipX4Achievement += CheckToUnlockAchievement;
    }

    private void OnDestroy()
    {
        ShipCrashed.shipCrashedAchievement -= CheckToUnlockAchievement;
        ShipLanded.shipLandedAchievement -= CheckToUnlockAchievement;
        ShipTouchGround.shipX4Achievement -= CheckToUnlockAchievement;
    }

    private void CheckToUnlockAchievement(string achievement)
    {
        string filePath = Application.persistentDataPath + "/" + PlayerPrefs.GetString("name") + "PlayerAchievmentData.dat";

        if (File.Exists(filePath))
        {
            FileStream file = File.OpenRead(filePath);
            BinaryFormatter bf = new BinaryFormatter();
            Dictionary<string, bool> achievementDic = (Dictionary<string, bool>)bf.Deserialize(file);
            if (!achievementDic[achievement])
            {
                achievementQueue.Enqueue(achievement);
            }
            file.Close();
        }
    }

    private void UnlockAchievement(string achievement)
    {
        SaveUnlock(achievement);
        switch (achievement)
        {
            case "landed":
                showLandedAchievement?.Invoke();
                break;
            case "crashed":
                showCrashedAchievement?.Invoke();
                break;
            case "500":
                show500Achievement?.Invoke();
                break;
            case "1000":
                show1000Achievement?.Invoke();
                break;
            case "x4":
                showX4Achievement?.Invoke();
                break;
        }
    }

    private void SaveUnlock(string achievement)
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
                UnlockAchievement(achievementQueue.Dequeue());
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
