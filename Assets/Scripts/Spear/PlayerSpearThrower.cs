using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

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
    
    public float ForceGrowSpeed = 18f;
    
    public float MinForce = 3f;
     
    public float MaxForce = 20f;
    
    [SerializeField]
    private float _force = 0;
    
    void Start()
    {
        StartCoroutine(ThrowASpearRoutine());
        _shootPoint = transform;
    }
    
    void Update()
    {
        if (!IsOwner)
            return;
        
        
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (Force >= MinForce)
            {
                if (IsServer)
                {
                    SpawnSpear(Force);
                }
                else
                {
                    SpawnSpearServerRpc(Force);
                }
            }
            Force = 0;
        }

        if (Input.GetKey(KeyCode.Mouse0) && Force <= MaxForce)
        {
            Force += Time.deltaTime * ForceGrowSpeed;
            if (Force > MaxForce) Force = MaxForce;

            Debug.Log(Force);
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

    private void SpawnSpear(float throwForce)
    {
        var spear = Instantiate(spearPrefab, transform.position, transform.rotation);
        var networkObject = spear.GetComponent<NetworkObject>();
        networkObject.Spawn();
        
        spear.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
    }

    [ServerRpc]
    private void SpawnSpearServerRpc(float throwForce)
    {
        SpawnSpear(throwForce);
        Debug.Log("Sercer RPC - throw force = " + throwForce);
    }
}
//
// public class PlayerSpearThrower : NetworkBehaviour
// {
//     [FormerlySerializedAs("SpearPrefab")] 
//     public GameObject spearPrefab;
//     
//     [FormerlySerializedAs("ForceGrowSpeed")] 
//     public float forceGrowSpeed = 18f;
//     
//     [FormerlySerializedAs("MinForce")] 
//     public float minForce = 3f;
//     
//     [FormerlySerializedAs("MaxForce")] 
//     public float maxForce = 20f;
//     
//     private float Force = 0;
//
//     void Update()
//     {
//         if (!IsOwner)
//             return;
//
//         
//         Force = 20;
//         
//         if (Input.GetKeyUp(KeyCode.Mouse0))
//         {
//             if (Force >= minForce)
//             {
//                 if (IsServer)
//                 {
//                     SpawnSpear();
//                 }
//                 else
//                 {
//                     SpawnSpearServerRpc();
//                 }
//             }
//             Force = 0;
//         }
//
//         if (Input.GetKey(KeyCode.Mouse0) && Force <= maxForce)
//         {
//             Force += Time.deltaTime * forceGrowSpeed;
//             if (Force > maxForce) Force = maxForce;
//             
//             Debug.Log(Force);
//         }
//     }
//     
//     private void SpawnSpear()
//     {
//         var spear = Instantiate(spearPrefab, transform.position, transform.rotation);
//         var networkObject = spear.GetComponent<NetworkObject>();
//         networkObject.Spawn();
//         
//         spear.GetComponent<Rigidbody>().AddForce(transform.forward * Force, ForceMode.Impulse);
//     }
//
//     [ServerRpc]
//     private void SpawnSpearServerRpc()
//     {
//         SpawnSpear();
//     }
// }
