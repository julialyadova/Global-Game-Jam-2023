
using Unity.Netcode;
using UnityEngine;

public class RoundState : GameState
{
    public GameObject GameUI;
    public WaveController WaveController;
    
    protected override void Enter()
    {
        GameUI.SetActive(true);
        WaveController.StartWaves(OnWavesEnded); //Server-only
    }

    protected override void Exit()
    {
        GameUI.SetActive(false);
    }

    public void OnWavesEnded()
    {
        ChangeState(GameStates.Singleton.RoundResultsState); //Client Rpc
    }
    

}
