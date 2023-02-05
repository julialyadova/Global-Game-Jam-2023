using Unity.Netcode;
using UnityEngine;

public class PlayerSpawnpoint : NetworkBehaviour
{
    void Start()
    {
        if(!IsOwner) return;

        var respawns = GameObject.FindGameObjectsWithTag("Respawn");
        var respawn = respawns[Random.Range(0, respawns.Length)];

        GetComponent<PlayerMovement>().Telporting(respawn.transform.position);
    }
}
