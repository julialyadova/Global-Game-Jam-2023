using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CoopModeUI : MonoBehaviour
{
    //TODO: validate coop mode inputs: ip address, port, etc
    
    public UnityEvent<HostGameEventArgs> OnHostGame;
    public UnityEvent<JoinGameEventArgs> OnJoinGame;

    public GameObject HostDialog;
    public GameObject JoinDialog;
    public TMP_InputField IPInput;
    public TMP_InputField HostPortInput;
    public TMP_Text LocalIPText;
    public TMP_InputField JoinPortInput;
    public TMP_InputField MaxPlayersInput;

    void Start()
    {
        LocalIPText.text = GetLocalIPAddress();
    }

    private string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }

        return "???.???.???.???";
    }

    public void OnSubmitHostButtonClick()
    {
        if (int.TryParse(HostPortInput.text, out var port)
            && int.TryParse(MaxPlayersInput.text, out var maxPlayers))
        {
            OnHostGame.Invoke(new HostGameEventArgs(port, maxPlayers));
        }
        else
        {
            Debug.LogWarning("Invalid input");
        }
    }

    public void OnSubmitJoinButtonClick()
    {
        if (int.TryParse(JoinPortInput.text, out var port))
        {
            OnJoinGame.Invoke(new JoinGameEventArgs(IPInput.text, port));
        }
        else
        {
            Debug.LogWarning("Invalid input");
        }
    }
    
    public void OnHostButtonClick()
    {
        HostDialog.SetActive(true);
    }

    public void OnJoinButtonClick()
    {
        JoinDialog.SetActive(true);
    }

    public void OnCloseHostDialogClick()
    {
        HostDialog.SetActive(false);
    }
    
    public void OnCloseJoinDialogClick()
    {
        JoinDialog.SetActive(false);
    }
}

public struct HostGameEventArgs
{
    public readonly int Port;
    public readonly int MaxPlayers;

    public HostGameEventArgs(int port, int maxPlayers)
    {
        Port = port;
        MaxPlayers = maxPlayers;
    }
}

public struct JoinGameEventArgs
{
    public readonly string HostIP;
    public readonly int Port;

    public JoinGameEventArgs(string hostIP, int port)
    {
        HostIP = hostIP;
        Port = port;
    }
}
