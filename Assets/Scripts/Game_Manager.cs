using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public SaveObject so;
    static Game_Manager instance;

    public static Game_Manager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public SaveObject getSaveObject()
    {
        return so;
    }

    public void setplayerName(string name)
    {
        so.playerName = name;
    }

    public string getplayerName()
    {
        return so.playerName;
    }

    public void setplayerColor(string color)
    {
        so.playerColor = color;
    }

    public string getplayerColor()
    {
        return so.playerColor;
    }

    public void setplayerDifficulty(string difficulty)
    {
        so.playerDifficulty = difficulty;
    }

    public string getplayerDifficulty()
    {
        return so.playerDifficulty;
    }

}
