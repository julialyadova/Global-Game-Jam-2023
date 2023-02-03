using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSpearThrower : MonoBehaviour
{
    [FormerlySerializedAs("SpearPrefab")] 
    public GameObject spearPrefab;

    [FormerlySerializedAs("ThrowsPerSecond")] 
    public float throwsPerSecond = 1;
    public float m_Thrust = 20f;
    private bool _canThrow = false;

    public Transform shootPoint;
    
    void Start()
    {
        StartCoroutine(ThrowASpearRoutine());
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ThrowASpear();
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

    private void ThrowASpear()
    {
        var go = Instantiate(spearPrefab, shootPoint.position, shootPoint.rotation);
//        var networkObject = go.GetComponent<NetworkObject>();
//        networkObject.Spawn();
    }
}
