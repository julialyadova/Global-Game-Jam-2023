
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class WaveController : MonoBehaviour //Server-Only
{
    public Spawner Spawner;
    public float SpawnDelay;
    public float WavesDelay;
    public int[] Waves;

    public int WaveNumber; //Network Variable
    public int EnemiesInWave; //Network Variable
    public int EnemiesRemained; //Network Variable
    public List<Entity> Enemies = new();

    private Action _onWavesEnded;

    public void StartWaves(Action onEnd)
    {
        _onWavesEnded = onEnd;
        StartWave(1);
    }

    private void StartWave(int waveNumber)
    {
        WaveNumber = waveNumber;
        EnemiesInWave = Waves[waveNumber - 1];
        EnemiesRemained = EnemiesInWave;
        Spawner.SpawnMany(EnemiesInWave, SpawnDelay, OnEnemySpawned);
        
        Debug.Log($"Wave {waveNumber} started: {EnemiesInWave} enemies in wave");
    }

    private void OnEnemySpawned(GameObject enemy)
    {
        var entity = enemy.GetComponent<Entity>();
        Enemies.Add(entity);
        entity.OnDie = OnCreatureKilled;
    }

    private void OnCreatureKilled(Entity entity)
    {
        if (Enemies.Contains(entity))
        {
            EnemiesRemained -= 1;
            Enemies.Remove(entity);
        }

        if (EnemiesRemained == 0)
        {
            EndWave();
        }
    }

    private void EndWave()
    {
        if (WaveNumber == Waves.Length)
        {
            End();
        }
        else
        {
            Debug.Log($"Wave {WaveNumber} ended.");
            StartCoroutine(DelayBeforeNextWave());
        }
    }

    private IEnumerator DelayBeforeNextWave()
    {
        yield return new WaitForSeconds(WavesDelay);
        WaveNumber++;
        StartWave(WaveNumber);
    }

    private void End()
    {
        _onWavesEnded?.Invoke();
        _onWavesEnded = null;
        
        Debug.Log($"All waves ended.");
    }
}
