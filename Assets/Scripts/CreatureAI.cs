using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class CreatureAI : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent _agent;
    
    [FormerlySerializedAs("PathUpdateFrequency")] 
    public float pathUpdateFrequency = 1;
    
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(FindTargetRoutine());
    }

    void Update()
    {

    }
    
    private IEnumerator FindTargetRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(pathUpdateFrequency);

            target = FindTarget();
        
            if(target != null)
                _agent.SetDestination(target.position);
        }
    }

    [CanBeNull]
    private Transform FindTarget()
    {
        var playersAndTrees = GameObject.FindGameObjectsWithTag("Tree");
            playersAndTrees.ToList().AddRange(GameObject.FindGameObjectsWithTag("Player"));
        
        var targets = playersAndTrees.Select(x =>
            {
                // check shortest distance
                _agent.SetDestination(x.transform.position);
                return (_agent.remainingDistance, x.transform);
            }).ToList();

        if (!targets.Any()) return null;

        return targets
            .OrderByDescending(x => x.remainingDistance)
            .First()
            .transform;
    }
}
