using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayerButton : UIButton
{
    private PlayerData player;
    

    public static event Action<PlayerData> PlayerSelected;

    private void OnEnable()
    {
        PlayerSelected += DifferentButtonBeenSelected;
    }
    
    private void OnDisable()
    {
        PlayerSelected -= DifferentButtonBeenSelected;
    }

    public void SetPlayer(PlayerData player)
    {
        this.player = player;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = player.GetName() + "  |  HighScore:  " + player.GetHighScore();
    }

    public PlayerData GetPlayerData()
    {
        return player;
    }

    protected override void RaiseOnButtonClick()
    {
        gameObject.GetComponent<Image>().color = Color.gray;
        PlayerSelected?.Invoke(player);
    }

    private void DifferentButtonBeenSelected(PlayerData player)
    {
        if (this.player.GetName() != player.GetName())
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}
