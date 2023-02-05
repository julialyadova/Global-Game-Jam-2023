using System;
using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CreatureAI : MonoBehaviour
{
    [FormerlySerializedAs("MaxPlayerChaiseInSeconds")] 
    public float maxPlayerChaiseInSeconds = 5;
 
    [FormerlySerializedAs("MaxTreeChaiseInSeconds")] 
    public float maxTreeChaiseInSeconds = 60;
    
    private NavMeshAgent _agent;
    
    private TargetWithDistance _target;
    
    [FormerlySerializedAs("PathUpdateFrequency")] 
    public float pathUpdateFrequency = 0.3f;
    
    [FormerlySerializedAs("FindTargetFrequency")] 
    public float findTargetFrequency = 1;

    [FormerlySerializedAs("ChanceToChoosePlayerAsTarget")] 
    public float chanceToChoosePlayerAsTarget = 0.70f;

    public UnityAction FoundTarget;
    public UnityAction NoTarget;
    
    void Awake()    
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(FindTargetRoutine());
        StartCoroutine(UpdatePathRoutine());
    }

    private IEnumerator FindTargetRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(findTargetFrequency);

            if (_target != null && _target.Transform == null)
                _target = null;
            
            if (_target != null && _target.Transform.CompareTag("Player") &&
                DateTime.UtcNow.Subtract(_target.StartChaiseAt).TotalSeconds > maxPlayerChaiseInSeconds)
                _target = null;
            
            if (_target != null && _target.Transform.CompareTag("Tree") &&
                DateTime.UtcNow.Subtract(_target.StartChaiseAt).TotalSeconds > maxTreeChaiseInSeconds)
                _target = null;
            
            if(_target != null)
                continue;

            var nearlyPlayer = FindTarget("Player");
            var nearlyTree = FindTarget("Tree");

            if (nearlyPlayer != null && nearlyTree != null)
                 Debug.Log($"player: {nearlyPlayer.Distance} tree:{nearlyTree.Distance}");
            
            if (nearlyPlayer == null) _target = nearlyTree;
            else if (nearlyTree == null) _target = nearlyPlayer;
            else if (nearlyPlayer.Distance < nearlyTree.Distance / 4)
                _target = nearlyPlayer;
            else if (nearlyPlayer.Distance > nearlyTree.Distance * 3 
                     && Random.Range(0f, 1f) < chanceToChoosePlayerAsTarget)
                 _target = nearlyPlayer;
            else _target = nearlyTree;
            
            if(_target != null)
                _agent.SetDestination(_target.Transform.position);
        }
    }

    [CanBeNull]
    private TargetWithDistance FindTarget(string tag)
    {
        var playersAndTrees = GameObject.FindGameObjectsWithTag(tag).ToList();
        var targets = playersAndTrees.Select(x =>
            {
                // check shortest distance
                //_agent.SetDestination(x.transform.position);
                return new TargetWithDistance()
                {
                    Transform = x.transform,
                    //Distance = _agent.remainingDistance,
                    Distance = Mathf.Abs(Vector3.Distance(transform.position, x.transform.position)),
                    StartChaiseAt = DateTime.UtcNow
                };
            }).ToList();

        if (!targets.Any()) return null;

        targets = targets
            .OrderByDescending(x => x.Distance)
            .ToList();

        var max = targets.Count <= 3 ? targets.Count : 3;
        return targets[Random.Range(0, max)];
    }
    
    private IEnumerator UpdatePathRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(pathUpdateFrequency);

            if (_target != null && (_target.Transform == null || !_target.Transform.gameObject.activeInHierarchy))
                _target = null;
            
            if(_target != null)
                _agent.SetDestination(_target.Transform.position);
        }
    }
}

public class TargetWithDistance
{
    public float Distance { get; set; }
    public Transform Transform { get; set; }
    public DateTime StartChaiseAt { get; set; }
}
