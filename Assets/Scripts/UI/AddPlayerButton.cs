using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddPlayerButton : UIButton
{
    public static event Action<String> AddPlayerName;
    protected override void RaiseOnButtonClick()
    {
        string playerName = getPlayerNameToAdd();
        AddPlayerName?.Invoke(playerName);
    }

    private string getPlayerNameToAdd()
    {
        string playerName = "";
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "CreateUserTextField")
            {
               playerName  = gameObject.transform.GetChild(i).GetComponent<TMP_InputField>().text;;
            } 
        }
        Debug.Log("AddPlayerButton: " + playerName);
        return playerName;
    }
}
