using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetPlayerPref : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("name"))
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Player: " + PlayerPrefs.GetString("name");
        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "No Player Selected";
        }
    }
}
