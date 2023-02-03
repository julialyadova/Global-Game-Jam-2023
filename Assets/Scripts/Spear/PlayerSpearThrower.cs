using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSpearThrower : NetworkBehaviour
{
    [FormerlySerializedAs("SpearPrefab")] 
    public GameObject spearPrefab;

    [FormerlySerializedAs("ThrowsPerSecond")] 
    public float throwsPerSecond = 1;
    public float Force = 20f;
    private bool _canThrow = false;

    private Transform _shootPoint;
    
    void Start()
    {
        StartCoroutine(ThrowASpearRoutine());
        _shootPoint = transform;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && IsOwner)
        {
            if (IsServer)
            {
                SpawnSpear();
            }
            else
            {
                SpawnSpearServerRpc();
            }
        }
    }
    
    private IEnumerator ThrowASpearRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / throwsPerSecond);
            _canThrow = true;
        }
    }

    private void SpawnSpear()
    {
        var spear = Instantiate(spearPrefab, transform.position, transform.rotation);
        var networkObject = spear.GetComponent<NetworkObject>();
        networkObject.Spawn();
        
        spear.GetComponent<Rigidbody>().AddForce(transform.forward * Force, ForceMode.Impulse);
    }

    [ServerRpc]
    private void SpawnSpearServerRpc()
    {
        SpawnSpear();
    }
}
