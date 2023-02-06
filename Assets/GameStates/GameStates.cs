using System;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public static GameStates Singleton;
    
    public GameState MenuState;
    public GameState GettingReadyState;
    public GameState RoundState;
    public GameState RoundResultsState;

    public GameStates()
    {
        Singleton = this;
    }

    public void Start()
    {
        MenuState.enabled = true;
        GettingReadyState.enabled = false;
        RoundState.enabled = false;
        RoundResultsState.enabled = false;
    }
}
