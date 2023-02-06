using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using UnityEngine;

public class MenuState: GameState
{
    public GameObject Menu;

    public void Start()
    {
        StartFirstState(this);
    }

    protected override void Enter()
    {
        Menu.SetActive(true);
    }

    protected override void Exit()
    {
        Menu.SetActive(false);
    }
    
    public void StartHost(HostGameEventArgs args)
    {
        ChangeState(GameStates.Singleton.GettingReadyState);
        return;
        
        if (NetworkManager.Singleton && !NetworkManager.Singleton.IsListening)
        {
            var uNetTransport = NetworkManager.Singleton.GetComponent<UNetTransport>();
            uNetTransport.ServerListenPort = args.Port;
            uNetTransport.MaxConnections = args.MaxPlayers;
            NetworkManager.Singleton.StartHost();
            ChangeState(GameStates.Singleton.GettingReadyState);
        }
    }

    public void StartClient(JoinGameEventArgs args)
    {
        if (NetworkManager.Singleton && !NetworkManager.Singleton.IsListening)
        {
            var uNetTransport = NetworkManager.Singleton.GetComponent<UNetTransport>();
            uNetTransport.ConnectAddress = args.HostIP;
            uNetTransport.ConnectPort = args.Port;
            NetworkManager.Singleton.StartClient();
            ChangeState(GameStates.Singleton.GettingReadyState);
        }
    }
}
