using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    private string name;
    private int gameVolume;
    private int musicVolume;
    
    public PlayerData(string name, int gameVolume, int musicVolume)
    {
        this.name = name;
        this.gameVolume = gameVolume;
        this.musicVolume = musicVolume;
    }

    public string getName()
    {
        return name;
    }

    public int getGameVolume()
    {
        return gameVolume;
    }

    public void setGameVolume(int gameVolume)
    {
        this.gameVolume = gameVolume;
    }
    
    public int getMusicVolume()
    {
        return musicVolume;
    }

    public void setMusicVolume(int musicVolume)
    {
        this.musicVolume = musicVolume;
    }
}
