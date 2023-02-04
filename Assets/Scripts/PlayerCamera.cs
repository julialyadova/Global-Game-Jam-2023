using Unity.Netcode;
using UnityEngine;

public class PlayerCamera : NetworkBehaviour
{
    void Start()
    {
        if(!IsOwner) GameObject.Find("Main Camera").SetActive(false);
    }
}
