using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    bool gameStarted;
    public bool GameStarted
    {
        get { return gameStarted; }
        set { gameStarted = value; }
    }

    private void Awake()
    {
        instance = this;

        gameStarted = false;
    }

    public void StartGame()
    {
        gameStarted = true;
    }
}
