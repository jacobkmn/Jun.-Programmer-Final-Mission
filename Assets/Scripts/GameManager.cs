using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    bool gameStarted;
    public bool GameStarted
    {
        get { return GameStarted; }
        set { GameStarted = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        gameStarted = false;
    }

    public void StartGame()
    {
        gameStarted = true;
    }
}
