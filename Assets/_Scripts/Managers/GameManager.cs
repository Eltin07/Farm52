using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public ScoreManager ScoreManager;
    public CropManager CropManager;
    public Board Board;


    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;        
    }
    void Start()
    {
        ScoreManager = gameObject.GetComponentInChildren<ScoreManager>();
        CropManager = gameObject.GetComponentInChildren<CropManager>();
        Application.targetFrameRate = 60;
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Play:
                HandlePlay();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
            case GameState.Paused:
                HandlePause();
                break; 
            default:
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }


    public void HandlePlay()
    {

    }

    public void HandleGameOver()
    {

    }

    public void HandlePause()
    {

    }
}

public enum GameState
{
    Play,
    GameOver,
    Paused
}