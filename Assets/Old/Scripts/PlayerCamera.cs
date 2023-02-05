using Unity.Netcode;
using UnityEngine;

public class PlayerCamera : NetworkBehaviour
{
    [SerializeField]
    private GameObject _camera;
    
    void Start()
    {
        if (!IsOwner)
        {
            _camera.SetActive(false);
        }
    }
}
