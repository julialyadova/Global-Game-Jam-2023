using Unity.Netcode;
using UnityEngine;

public class ObjectDespawner : MonoBehaviour
{
    public void Despawn()
    {
        GetComponent<NetworkObject>().Despawn();
        Destroy(this);
    }
}
