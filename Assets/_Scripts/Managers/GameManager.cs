using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public ScoreManager ScoreManager;

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

        Application.targetFrameRate = 60;
    }
}
