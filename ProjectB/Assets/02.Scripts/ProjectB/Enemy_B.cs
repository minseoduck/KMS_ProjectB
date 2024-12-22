using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_B : MonoBehaviour
{
    // ���¸� ����
    public enum EnemyState 
    { 
        Idle, 
        Chase,
        MaxCount
    }

    private EnemyState currentState;
    private NavMeshAgent pathfinder; // ���� NavMeshAgent
  
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

        // �ʱ� ���¸� Chase�� ����
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
                pathfinder.ResetPath(); // �̵��B������
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
           
            // �÷��̾ �ٶ󺸰�
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            directionToPlayer.y = 0; 
            transform.rotation = Quaternion.LookRotation(directionToPlayer);
        }
    }
}

