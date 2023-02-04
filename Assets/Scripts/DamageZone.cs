using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public bool ignoreTree = false;
    public bool ignoreCreatures = true;
    public bool ignorePlayers = false;
    public float damageIntervalInSeconds = 1;
    public int damageAmountPerCall = 10;

    private Dictionary<Collider, Health> _objectsInZone 
        = new Dictionary<Collider, Health>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DamageRoutine());
    }

    private IEnumerator DamageRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(damageIntervalInSeconds);

            var buffer = _objectsInZone.Values.ToArray();
            if (buffer.Length == 0) continue;
            
            // Debug.Log($"Buffer has {buffer.Length} elements");
            
            foreach (var item in buffer)
                item.TakeDamage(damageAmountPerCall);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_objectsInZone.ContainsKey(other))
            return;
  
        if(ignoreCreatures && other.CompareTag("Creature"))
            return;
        if(ignoreTree && other.CompareTag("Tree"))
            return;
        if(ignorePlayers && other.CompareTag("Player"))
            return;
        
        var health = other.GetComponent<Health>();
        
        if (health == null)
            return;

        _objectsInZone.Add(other, health);

    }

    private void OnTriggerExit(Collider other)
    {
        if (!_objectsInZone.ContainsKey(other))
            return;

        _objectsInZone.Remove(other);
    }
}
