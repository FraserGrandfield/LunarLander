using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListManager : MonoBehaviour
{
    [SerializeField] private GameObject button;
    private ArrayList players = new ArrayList();
    private PlayerData selectedPlayer;
    public static event Action<PlayerData> AddNewPlayer;
    public static event Action<Dictionary<string, bool>, string> AddNewPlayerAchievments;
    public static event Action UpdateMusicVolume;
    public static event Action<string> ShowNotificaiton;
    public static event Action<PlayerData> DeletePlayer;

    private void OnEnable()
    {
        AddPlayerButton.AddPlayerName += AddPlayer;
        ReadPlayersData.AllPlayerData += AddAllPlayersToList;
        SetPlayer.SetPlayerButtonClicked += SetActivePlayer;
        SelectPlayerButton.PlayerSelected += SetSelectedPlayer;
        DeletePlayerButton.DeletePlayer += DeleteSelectedPlayer;
    }
    
    private void OnDisable()
    {
        AddPlayerButton.AddPlayerName -= AddPlayer;
        ReadPlayersData.AllPlayerData -= AddAllPlayersToList;
        SetPlayer.SetPlayerButtonClicked -= SetActivePlayer;
        SelectPlayerButton.PlayerSelected -= SetSelectedPlayer;
        DeletePlayerButton.DeletePlayer -= DeleteSelectedPlayer;
    }

    private void AddPlayer(string pName)
    {
        Debug.Log("addName: " + pName);
        if (CheckName(pName))
        {
            PlayerData pd = new PlayerData(pName, 100, 100, 1, 0);
            Dictionary<string, bool> achievments = new Dictionary<string, bool>();
            achievments.Add("landed", false);
            achievments.Add("crashed", false);
            achievments.Add("500", false);
            achievments.Add("1000", false);
            achievments.Add("x4", false);
            AddPlayerToList(pd);
            AddNewPlayer?.Invoke(pd);
            AddNewPlayerAchievments?.Invoke(achievments, pName);
        }
    }

    private void AddAllPlayersToList(ArrayList playerDataList)
    {
        for (int i = 0; i < playerDataList.Count; i++)
        {
            AddPlayerToList(((PlayerData)playerDataList[i]));
        }
    }

    private void AddPlayerToList(PlayerData player)
    {
        GameObject newButton = Instantiate(button);
        players.Add(newButton);
        newButton.SetActive(true);
        newButton.GetComponent<SelectPlayerButton>().SetPlayer(player);
        newButton.transform.SetParent(transform);
        newButton.transform.localScale = new Vector3(1, 1, 1);
    }

    private bool CheckName(string pName)
    {
        if ( pName.Length > 10 || pName == "")
        {
            ShowNotificaiton?.Invoke("Player  Name  cannot  be  empty  or  longer  than  10  characters");
            return false;
        }
        for (int i = 0; i < players.Count; i++)
        {
            if (pName == ((GameObject)players[i]).gameObject.GetComponent<SelectPlayerButton>().GetPlayerData().GetName())
            {
                ShowNotificaiton?.Invoke(pName + "  already  exists");
                return false;
            }
        }
        return true;
    }

    private void SetSelectedPlayer(PlayerData player)
    {
        selectedPlayer = player;
    }

    private void SetActivePlayer()
    {
        if (selectedPlayer != null)
        {
            PlayerPrefs.SetString("name", selectedPlayer.GetName());
            PlayerPrefs.SetInt("gameVolume", selectedPlayer.GetGameVolume());
            PlayerPrefs.SetInt("musicVolume", selectedPlayer.GetMusicVolume());
            PlayerPrefs.SetInt("playTutorial", selectedPlayer.GetPlayTutorial());
            PlayerPrefs.SetInt("highscore", selectedPlayer.GetHighScore());
            Debug.Log("Change player tutorial toggle " + selectedPlayer.GetPlayTutorial());
            ShowNotificaiton?.Invoke("Player  set  Too:  " + selectedPlayer.GetName());
            UpdateMusicVolume?.Invoke();
        }
        else
        {
            ShowNotificaiton?.Invoke("Please  select  a  player");
        }
    }

    private void DeleteSelectedPlayer()
    {
        if (selectedPlayer != null)
        {
            Debug.Log("Before " + players.Count);
            DeletePlayer?.Invoke(selectedPlayer);
            if (selectedPlayer.GetName() == PlayerPrefs.GetString("name"))
            {
                PlayerPrefs.DeleteAll();
            }

            for (int i = 0; i < players.Count; i++)
            {
                if (selectedPlayer.GetName() == ((GameObject)players[i]).gameObject.GetComponent<SelectPlayerButton>().GetPlayerData().GetName())
                {
                    Destroy((GameObject) players[i]);
                    players.RemoveAt(i);
                }
            }
            selectedPlayer = null;
            Debug.Log("After " + players.Count);
        }
        else
        {
            ShowNotificaiton?.Invoke("Please  select  a  player");
        }
    }
}
