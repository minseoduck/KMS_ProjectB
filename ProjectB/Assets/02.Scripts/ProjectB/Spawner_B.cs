using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_B : MonoBehaviour
{
    [System.Serializable]
    public class Wave_Ｂ
    {
        public int enemyCount;
        public float timeBetweenSpawns;
        public float enemyHealth;
        public float enemySpeed;
    }

    public Wave_Ｂ[] waves;
    public Enemy_B enemyPrefab;
    public Transform player;
   

    private int currentWaveIndex = 0;
    private int enemiesRemainingToSpawn = 0;
    private int enemiesRemainingAlive = 0;
    private float nextSpawnTime;

    public float waveStartDelay = 3f;

    private void Start()
    {
        StartNextWave();
    }

    private void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + waves[currentWaveIndex].timeBetweenSpawns;
        }
    }

    private void StartNextWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            StartCoroutine(PrepareNextWave(waves[currentWaveIndex]));
        }
        else
        {
            Debug.Log("모든 웨이브가 완료되었습니다!");
        }
    }

    private IEnumerator PrepareNextWave(Wave_Ｂ wave)
    {
        Debug.Log($"Wave {currentWaveIndex + 1} 준비 중...");
        yield return new WaitForSeconds(waveStartDelay);

        Debug.Log($"Wave {currentWaveIndex + 1} 시작!");
        enemiesRemainingToSpawn = wave.enemyCount;
        enemiesRemainingAlive = wave.enemyCount;
        currentWaveIndex++; // 웨이브 시작 후 인덱스 증가
    }

    private void SpawnEnemy()
    {
        if (currentWaveIndex - 1 >= 0 && currentWaveIndex - 1 < waves.Length)
        {
            
            Enemy_B spawnedEnemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);

            // 적 초기화
            Wave_Ｂ currentWave = waves[currentWaveIndex - 1];
            spawnedEnemy.Initialize(player, currentWave.enemyHealth, currentWave.enemySpeed);
            spawnedEnemy.OnDeath += OnEnemyDeath;

            Debug.Log("적 소환 완료!");
        }
        else
        {
            Debug.LogError("Wave index is out of range!");
        }
    }

    private void OnEnemyDeath()
    {
        enemiesRemainingAlive--;
        Debug.Log("적 사망!");

        if (enemiesRemainingAlive == 0 && enemiesRemainingToSpawn == 0)
        {
            StartNextWave();
        }
    }
}
