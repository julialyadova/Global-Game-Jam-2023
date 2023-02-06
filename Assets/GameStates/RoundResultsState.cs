using UnityEngine;

public class RoundResultsState : GameState
{
    public GameObject RoundResultsUI;
    
    protected override void Enter()
    {
        RoundResultsUI.SetActive(true);
    }

    protected override void Exit()
    {
        RoundResultsUI.SetActive(false);
    }

    public void StartNewRound()//Server-only action
    {
        ChangeState(GameStates.Singleton.RoundState); //Client Rpc
    }
}
