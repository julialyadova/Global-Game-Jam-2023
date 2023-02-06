using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    private static GameState _currentState;

    protected abstract void Enter();
    
    protected abstract void Exit();
    
    protected void ChangeState(GameState nextState)
    {
        _currentState.Exit();
        _currentState.enabled = false;
        _currentState = nextState;
        nextState.enabled = true;
        nextState.Enter();
    }

    protected void StartFirstState(GameState firstState)
    {
        _currentState = firstState;
        _currentState.enabled = true;
        _currentState.Enter();
    }
}
