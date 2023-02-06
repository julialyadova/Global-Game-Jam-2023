using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class OldSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public float Frequency;

    public int maxCount = 10;

    private int _currentCount = 0;

    // private void Awake()
    // {
    //     if (!IsServer) gameObject.SetActive(false); 
    // }

    private void OnServerStarted()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Frequency);
            
            if(_currentCount >= maxCount)
                continue;
            
            var go = Instantiate(Prefab, transform.position, Quaternion.identity);
            var networkObject = go.GetComponent<NetworkObject>();
            networkObject.Spawn();

            _currentCount++;
        }
    }

    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnServerStarted -= OnServerStarted;
        }
    }
}
