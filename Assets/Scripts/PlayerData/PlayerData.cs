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
    
        public int getPlayTutorial()
    {
        return playTutorial;
    }

    public void setPlayTutorial(int playTutorial)
    {
        this.playTutorial = playTutorial;
    }

    public int getHighScore()
    {
        return highScore;
    }

    public void setHighScore(int score)
    {
        highScore = score;
    }
}
