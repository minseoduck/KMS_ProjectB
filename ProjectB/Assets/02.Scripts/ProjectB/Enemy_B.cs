using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_B : Character_B
{
    // ���� ����
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
    private float attackDistance = 1.5f; // ���� �Ÿ�
    [SerializeField]
    private float attackDamage = 10f;    // ���� ������
    [SerializeField]
    private float attackCooldown = 2f;  // ���� ��Ÿ��
    [SerializeField]
    private float updateRate = 0.25f;   // ���� ���� ����

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

        // �÷��̾��� ��� �̺�Ʈ ����
        if (player != null)
        {
            Character_B playerCharacter = player.GetComponent<Character_B>();
            if (playerCharacter != null)
            {
                playerCharacter.OnDeath += OnPlayerDeath;
            }
        }

        SetState(EnemyState.Chase); // �⺻ ���� ����
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
                // ���� �߿��� �ƹ� ���� ����
                break;
        }
    }

    private void SetState(EnemyState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case EnemyState.Idle:
                pathfinder.ResetPath(); // �̵� ����
                break;

            case EnemyState.Chase:
                pathfinder.isStopped = false; // �̵� �簳
                break;

            case EnemyState.Attacking:
                pathfinder.isStopped = true; // ���� �� �̵� ����
                break;
        }
    }

    private void HandleIdle()
    {
        // ��� ���� ó��
    }

    private void HandleChase()
    {
        if (player == null) return;

        // ���� �������� ���� ����
        if (Time.time - lastUpdateTime >= updateRate)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // ���� ���� �ȿ� ���� ��� ����
            if (distanceToPlayer <= attackDistance && Time.time >= lastAttackTime + attackCooldown)
            {
                StartCoroutine(Attack());
            }
            else
            {
                pathfinder.SetDestination(player.position); // �÷��̾� ����
            }

            lastUpdateTime = Time.time;
        }

        // �÷��̾ �ٶ󺸰� ����
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0;
        transform.rotation = Quaternion.LookRotation(directionToPlayer);
    }

    private IEnumerator Attack()
    {
        SetState(EnemyState.Attacking);

        // ���� ���� - ���� ����
        skinMaterial.color = Color.red;

        Vector3 originalPosition = transform.position;
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        Vector3 attackPosition = player.position - dirToPlayer * 0.5f;

        float attackSpeed = 3f;
        float percent = 0f;
        bool hasAppliedDamage = false;

        // ���� �ִϸ��̼� ����
        while (percent <= 1f)
        {
            if (percent >= 0.5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                player.GetComponent<Character_B>().TakeDamage(attackDamage);
            }

            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4; // ��� �
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }

        // ���� ���� - ���� ���� ����
        skinMaterial.color = originalColor;

        // ���� ��Ÿ�� ����
        lastAttackTime = Time.time;

        // ���� ���·� ��ȯ
        SetState(EnemyState.Chase);
    }

    private void OnPlayerDeath()
    {
        playerIsAlive = false; // �÷��̾� ��� �� ���� ����
        SetState(EnemyState.Idle);
        Debug.Log("�÷��̾� ���. ���� ������ ����ϴ�.");
    }

    protected override void Die()
    {
        base.Die();      
    }
}
