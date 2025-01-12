using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_B : Character_B
{
    // 상태 정의
    public enum EnemyState
    {
        Idle,
        Chase,
        Attacking,
        MaxCount
    }

    private EnemyState currentState;
    private NavMeshAgent pathfinder;
    private bool playerIsAlive = true;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float attackDistance = 1.5f; // 공격 거리
    [SerializeField]
    private float attackDamage = 10f;    // 공격 데미지
    [SerializeField]
    private float attackCooldown = 2f;  // 공격 쿨타임
    [SerializeField]
    private float updateRate = 0.25f;   // 추적 갱신 간격

    private float lastAttackTime;
    private float lastUpdateTime;

    private Material skinMaterial;
    private Color originalColor;

    public void Initialize(Transform playerTransform, float initialHealth, float movementSpeed)
    {
        player = playerTransform;
        healthData.currentHealth = initialHealth;

        pathfinder = GetComponent<NavMeshAgent>();
        pathfinder.speed = movementSpeed;

        // 플레이어의 사망 이벤트 구독
        if (player != null)
        {
            Character_B playerCharacter = player.GetComponent<Character_B>();
            if (playerCharacter != null)
            {
                playerCharacter.OnDeath += OnPlayerDeath;
            }
        }

        SetState(EnemyState.Chase); // 기본 상태 설정
    }
    private void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        if (player != null)
        {
            SetState(EnemyState.Chase);
        }
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
            case EnemyState.Attacking:
                // 공격 중에는 아무 동작 없음
                break;
        }
    }

    private void SetState(EnemyState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case EnemyState.Idle:
                pathfinder.ResetPath(); // 이동 중지
                break;

            case EnemyState.Chase:
                pathfinder.isStopped = false; // 이동 재개
                break;

            case EnemyState.Attacking:
                pathfinder.isStopped = true; // 공격 중 이동 정지
                break;
        }
    }

    private void HandleIdle()
    {
        // 대기 상태 처리
    }

    private void HandleChase()
    {
        if (player == null) return;

        // 일정 간격으로 추적 갱신
        if (Time.time - lastUpdateTime >= updateRate)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // 공격 범위 안에 있을 경우 공격
            if (distanceToPlayer <= attackDistance && Time.time >= lastAttackTime + attackCooldown)
            {
                StartCoroutine(Attack());
            }
            else
            {
                pathfinder.SetDestination(player.position); // 플레이어 추적
            }

            lastUpdateTime = Time.time;
        }

        // 플레이어를 바라보게 설정
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0;
        transform.rotation = Quaternion.LookRotation(directionToPlayer);
    }

    private IEnumerator Attack()
    {
        SetState(EnemyState.Attacking);

        // 공격 시작 - 색상 변경
        skinMaterial.color = Color.red;

        Vector3 originalPosition = transform.position;
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        Vector3 attackPosition = player.position - dirToPlayer * 0.5f;

        float attackSpeed = 3f;
        float percent = 0f;
        bool hasAppliedDamage = false;

        // 공격 애니메이션 실행
        while (percent <= 1f)
        {
            if (percent >= 0.5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                player.GetComponent<Character_B>().TakeDamage(attackDamage);
            }

            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4; // 찌르기 곡선
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }

        // 공격 종료 - 원래 색상 복원
        skinMaterial.color = originalColor;

        // 공격 쿨타임 설정
        lastAttackTime = Time.time;

        // 추적 상태로 전환
        SetState(EnemyState.Chase);
    }

    private void OnPlayerDeath()
    {
        playerIsAlive = false; // 플레이어 사망 시 추적 중지
        SetState(EnemyState.Idle);
        Debug.Log("플레이어 사망. 적이 추적을 멈춥니다.");
    }

    protected override void Die()
    {
        base.Die();      
    }
}
