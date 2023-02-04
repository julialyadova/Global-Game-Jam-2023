using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSpearThrower : NetworkBehaviour
{
    [FormerlySerializedAs("SpearPrefab")] 
    public GameObject spearPrefab;
    
    [FormerlySerializedAs("ForceGrowSpeed")] 
    public float forceGrowSpeed = 18f;
    
    [FormerlySerializedAs("MinForce")] 
    public float minForce = 3f;
    
    [FormerlySerializedAs("MaxForce")] 
    public float maxForce = 20f;
    
    private float Force = 0;

    void Update()
    {
        if (!IsOwner)
            return;

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (Force >= minForce)
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
            Force = 0;
        }

        if (Input.GetKey(KeyCode.Mouse0) && Force <= maxForce)
        {
            Force += Time.deltaTime * forceGrowSpeed;
            if (Force > maxForce) Force = maxForce;
            
            Debug.Log(Force);
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
