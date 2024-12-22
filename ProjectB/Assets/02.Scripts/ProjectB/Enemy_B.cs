using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_B : MonoBehaviour
{
    // 상태를 정의
    public enum EnemyState 
    { 
        Idle, 
        Chase,
        MaxCount
    }

    private EnemyState currentState;
    private NavMeshAgent pathfinder; // 적의 NavMeshAgent
  
    [SerializeField] 
    private Transform player;

    [SerializeField]
    private float updateRate = 0.25f;

    private float lastUpdateTime;

    private void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            return;
        }

        // 초기 상태를 Chase로 설정
        SetState(EnemyState.Chase);
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdle();
                break;
            case EnemyState.Chase:
                HandleChase();
                break;
        }
    }

    private void SetState(EnemyState newState)
    {
        currentState = newState;

       
        switch (newState)
        {
            case EnemyState.Idle:
                pathfinder.ResetPath(); // 이동줒ㅇ중지
                break;

            case EnemyState.Chase:
                break;
        }
    }

    private void HandleIdle()
    {
       
    }

    private void HandleChase()
    {
        if (Time.time - lastUpdateTime >= updateRate)
        {
            if (player != null)
            {
                pathfinder.SetDestination(player.position);
            }
            lastUpdateTime = Time.time;
        }
        if (player != null)
        {
           
            // 플레이어를 바라보게
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            directionToPlayer.y = 0; 
            transform.rotation = Quaternion.LookRotation(directionToPlayer);
        }
    }
}

