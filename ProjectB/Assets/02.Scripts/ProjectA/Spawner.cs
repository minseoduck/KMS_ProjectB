using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Wave[] waves;
    public Enemy enemy;

    //���� ���̺��� ���۷��� �������� ���� ( ���� ����  )
    Wave currentWave;
    //���� ���̺� Ƚ�� 
    int currentWaveNumber;

    //�����ִ� �� ������� �˾ƾ��� 
    int enemiseRemainToSpawn;

    int enemiseRemainingAlive;
    //������ ���� �ð� 
    float nextSpawnTime;




    //���̺� ���� ������ Ŭ���� �ʿ� 
    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
    }
    private void Start()
    {
        NextWave();
    }


    void Update()
    {
        //Ÿ�̸ӿ� ���� ������ �󸶳� ��������
        //-> �����ؾ������� �󸶳� ���Ҵ��� �˰��־����
        if (enemiseRemainToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiseRemainToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
            Debug.Log("�� ����");
            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
    }
    void OnEnemyDeath()
    {
        Debug.Log("�� ����;");
        enemiseRemainingAlive--;
        if (enemiseRemainingAlive==0)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        Debug.Log("Wave: " + currentWaveNumber);
        if (currentWaveNumber -1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];

            enemiseRemainToSpawn = currentWave.enemyCount;
            enemiseRemainingAlive = enemiseRemainToSpawn;
        }
       
    }
}
