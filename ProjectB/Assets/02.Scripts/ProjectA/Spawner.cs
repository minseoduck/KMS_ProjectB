using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Wave[] waves;
    public Enemy enemy;

    //현재 웨이브의 레퍼런스 가져오기 위함 ( 편의 위해  )
    Wave currentWave;
    //현재 웨이브 횟수 
    int currentWaveNumber;

    //남아있는 적 몇마리인지 알아야함 
    int enemiseRemainToSpawn;

    int enemiseRemainingAlive;
    //다음번 스폰 시간 
    float nextSpawnTime;




    //웨이브 정보 저장할 클래스 필요 
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
        //타이머에 따라 적들을 얼마나 스폰할지
        //-> 스폰해야할적이 얼마나 남았는지 알고있어야함
        if (enemiseRemainToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiseRemainToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
            Debug.Log("적 스폰");
            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
    }
    void OnEnemyDeath()
    {
        Debug.Log("적 죽음;");
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
