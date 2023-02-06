using Unity.Netcode;
using UnityEngine;

public class GettingReadyState: GameState
{
    public GameObject GettingReadyUI;

    public void StartRound() //Server-only
    {
        ChangeState(GameStates.Singleton.RoundState);
    }

    protected override void Enter()
    {
        GettingReadyUI.SetActive(true);
    }

    protected override void Exit()
    {
        GettingReadyUI.SetActive(false);
    }
}
