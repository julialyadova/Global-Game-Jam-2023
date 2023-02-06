using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour  // Server-only
{
    public GameObject Prefab;

    public GameObject Spawn()
    {
        var go = Instantiate(Prefab, transform.position, Quaternion.identity);
        return go;
        //go.GetComponent<NetworkObject>().Spawn();
    }

    public void SpawnMany(int count, float spawnDelay, Action<GameObject> onSpawn)
    {
        StartCoroutine(SpawnManyCoroutine(count, spawnDelay, onSpawn));
    }
    
    private IEnumerator SpawnManyCoroutine(int count, float spawnDelay, Action<GameObject> onSpawn)
    {
        for (int i = 0; i < count; i++)
        {
            onSpawn?.Invoke(Spawn());
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
