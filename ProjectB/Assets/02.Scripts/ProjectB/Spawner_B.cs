using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_B : MonoBehaviour
{
    [System.Serializable]
    public class Wave_��
    {
        public int enemyCount;
        public float timeBetweenSpawns;
        public float enemyHealth;
        public float enemySpeed;
    }

    public Wave_��[] waves;
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
            Debug.Log("��� ���̺갡 �Ϸ�Ǿ����ϴ�!");
        }
    }

    private IEnumerator PrepareNextWave(Wave_�� wave)
    {
        Debug.Log($"Wave {currentWaveIndex + 1} �غ� ��...");
        yield return new WaitForSeconds(waveStartDelay);

        Debug.Log($"Wave {currentWaveIndex + 1} ����!");
        enemiesRemainingToSpawn = wave.enemyCount;
        enemiesRemainingAlive = wave.enemyCount;
        currentWaveIndex++; // ���̺� ���� �� �ε��� ����
    }

    private void SpawnEnemy()
    {
        if (currentWaveIndex - 1 >= 0 && currentWaveIndex - 1 < waves.Length)
        {
            
            Enemy_B spawnedEnemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);

            // �� �ʱ�ȭ
            Wave_�� currentWave = waves[currentWaveIndex - 1];
            spawnedEnemy.Initialize(player, currentWave.enemyHealth, currentWave.enemySpeed);
            spawnedEnemy.OnDeath += OnEnemyDeath;

            Debug.Log("�� ��ȯ �Ϸ�!");
        }
        else
        {
            Debug.LogError("Wave index is out of range!");
        }
    }

    private void OnEnemyDeath()
    {
        enemiesRemainingAlive--;
        Debug.Log("�� ���!");

        if (enemiesRemainingAlive == 0 && enemiesRemainingToSpawn == 0)
        {
            StartNextWave();
        }
    }
}
