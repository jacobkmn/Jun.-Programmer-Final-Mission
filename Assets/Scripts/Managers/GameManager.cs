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

    bool gameOver;
    public bool GameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }

    private void Awake()
    {
        instance = this;

        gameStarted = false;
    }

    public void MarkStartGame()
    {
        gameStarted = true;
    }
    public void MarkGameOver()
    {
        gameOver = gameOver == true ? false : true;
    }
}
