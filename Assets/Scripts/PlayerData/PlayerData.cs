using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    private string name;
    private int volume;

    public PlayerData(string name, int volume)
    {
        this.name = name;
        this.volume = volume;
    }

    public string getName()
    {
        return name;
    }

    public int getVolume()
    {
        return volume;
    }

    public void setVolume(int volume)
    {
        this.volume = volume;
    }
}
