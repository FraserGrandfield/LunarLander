using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievmentListManager : MonoBehaviour
{
    private Dictionary<string, bool> achievments;

    private void Start()
    {
        GetPlayerData();
    }

    private void GetPlayerData()
    {
        string filePath = Application.persistentDataPath + "/" + PlayerPrefs.GetString("name") + "PlayerAchievmentData.dat";
        if (File.Exists(filePath))
        {
            FileStream file = File.OpenRead(filePath);
            BinaryFormatter bf = new BinaryFormatter();
            achievments = (Dictionary<string, bool>) bf.Deserialize(file);
            file.Close();
            ShowAchievments();
        }
    }

    private void ShowAchievments()
    {
        if (achievments["landed"])
        {
            gameObject.transform.Find("AchivmentLand").GetComponent<Image>().color = Color.white;
            gameObject.transform.Find("AchivmentLand").GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            gameObject.transform.Find("AchivmentLand").GetComponent<Image>().color = new Color(0.3686275f, 0.3686275f, 0.3686275f);
            gameObject.transform.Find("AchivmentLand").GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.3686275f, 0.3686275f, 0.3686275f);
        }

        if (achievments["crashed"])
        {
            gameObject.transform.Find("AchivmentCrash").GetComponent<Image>().color = Color.white;
            gameObject.transform.Find("AchivmentCrash").GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            gameObject.transform.Find("AchivmentCrash").GetComponent<Image>().color = new Color(0.3686275f, 0.3686275f, 0.3686275f);
            gameObject.transform.Find("AchivmentCrash").GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.3686275f, 0.3686275f, 0.3686275f);

        }
        
        if (achievments["500"])
        {
            gameObject.transform.Find("Achivment500").GetComponent<Image>().color = Color.white;
            gameObject.transform.Find("Achivment500").GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            gameObject.transform.Find("Achivment500").GetComponent<Image>().color = new Color(0.3686275f, 0.3686275f, 0.3686275f);
            gameObject.transform.Find("Achivment500").GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.3686275f, 0.3686275f, 0.3686275f);
        }
        
        if (achievments["1000"])
        {
            gameObject.transform.Find("Achivment1000").GetComponent<Image>().color = Color.white;
            gameObject.transform.Find("Achivment1000").GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            gameObject.transform.Find("Achivment1000").GetComponent<Image>().color = new Color(0.3686275f, 0.3686275f, 0.3686275f);
            gameObject.transform.Find("Achivment1000").GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.3686275f, 0.3686275f, 0.3686275f);
        }
        
        if (achievments["x4"])
        {
            gameObject.transform.Find("AchivmentX4").GetComponent<Image>().color = Color.white;
            gameObject.transform.Find("AchivmentX4").GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            gameObject.transform.Find("AchivmentX4").GetComponent<Image>().color = new Color(0.3686275f, 0.3686275f, 0.3686275f);
            gameObject.transform.Find("AchivmentX4").GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.3686275f, 0.3686275f, 0.3686275f);
        }
    }
}
