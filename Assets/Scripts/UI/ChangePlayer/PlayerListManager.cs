using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListManager : MonoBehaviour
{
    [SerializeField] private GameObject button;
    private ArrayList players = new ArrayList();
    private PlayerData selectedPlayer = null;
    public static event Action<PlayerData> AddNewPlayer;
    public static event Action<string> ShowNotificaiton;

    private void OnEnable()
    {
        AddPlayerButton.AddPlayerName += addPlayer;
        ReadPlayerData.AllPlayerData += addAllPlayersToList;
        SetPlayer.SetPlayerButtonClicked += setActivePlayer;
        SelectPlayerButton.PlayerSelected += setSelectedPlayer;
    }
    
    private void OnDisable()
    {
        AddPlayerButton.AddPlayerName -= addPlayer;
        ReadPlayerData.AllPlayerData -= addAllPlayersToList;
        SetPlayer.SetPlayerButtonClicked -= setActivePlayer;
        SelectPlayerButton.PlayerSelected -= setSelectedPlayer;
    }

    private void addPlayer(string pName)
    {
        Debug.Log("addName: " + pName);
        if (checkName(pName))
        {
            PlayerData pd = new PlayerData(pName, 100, 100, 1);
            addPlayerToList(pd);
            AddNewPlayer?.Invoke(pd);
        }
    }

    private void addAllPlayersToList(ArrayList playerDataList)
    {
        for (int i = 0; i < playerDataList.Count; i++)
        {
            //TODO check if line below is need as it called in other function!
            //players.Add(playerDataList[i]);
            addPlayerToList(((PlayerData)playerDataList[i]));
        }
    }

    private void addPlayerToList(PlayerData player)
    {
        players.Add(player);
        GameObject newButton = Instantiate(button);
        newButton.SetActive(true);
        newButton.GetComponent<SelectPlayerButton>().SetPlayer(player);
        newButton.transform.SetParent(transform);
        newButton.transform.localScale = new Vector3(1, 1, 1);
    }

    private bool checkName(string pName)
    {
        if ( pName.Length > 10 || pName == "")
        {
            ShowNotificaiton?.Invoke("Player Name cannot be empty or longer than 10 characters");
            return false;
        }
        for (int i = 0; i < players.Count; i++)
        {
            if (pName == ((PlayerData) players[i]).getName())
            {
                ShowNotificaiton?.Invoke(pName + " already exists");
                return false;
            }
        }
        return true;
    }

    private void setSelectedPlayer(PlayerData player)
    {
        selectedPlayer = player;
    }

    private void setActivePlayer()
    {
        if (selectedPlayer != null)
        {
            PlayerPrefs.SetString("name", selectedPlayer.getName());
            PlayerPrefs.SetInt("gameVolume", selectedPlayer.getGameVolume());
            PlayerPrefs.SetInt("musicVolume", selectedPlayer.getMusicVolume());
            PlayerPrefs.SetInt("playTutorial", selectedPlayer.getPlayTutorial());
            Debug.Log("Change player tutorial toggle " + selectedPlayer.getPlayTutorial());
            ShowNotificaiton?.Invoke("Player set Too: " + selectedPlayer.getName());
        }
        else
        {
            ShowNotificaiton?.Invoke("Please select a player");
        }
    }
}
