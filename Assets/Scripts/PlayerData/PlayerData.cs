using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    private string name;
    private int gameVolume;
    private int musicVolume;
    private int playTutorial;
    private int highScore;

    public PlayerData(string name, int gameVolume, int musicVolume, int playTutorial, int highScore)
    {
        this.name = name;
        this.gameVolume = gameVolume;
        this.musicVolume = musicVolume;
        this.playTutorial = playTutorial;
        this.highScore = highScore;
    }

    public string GetName()
    {
        return name;
    }

    public int GetGameVolume()
    {
        return gameVolume;
    }

    public void SetGameVolume(int gameVolume)
    {
        this.gameVolume = gameVolume;
    }
    
    public int GetMusicVolume()
    {
        return musicVolume;
    }

    public void SetMusicVolume(int musicVolume)
    {
        this.musicVolume = musicVolume;
    }
    
        public int GetPlayTutorial()
    {
        return playTutorial;
    }

    public void SetPlayTutorial(int playTutorial)
    {
        this.playTutorial = playTutorial;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void SetHighScore(int score)
    {
        highScore = score;
    }
}
