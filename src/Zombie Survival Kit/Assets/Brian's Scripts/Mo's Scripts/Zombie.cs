using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public float health;
    public float damage;
    public bool campType;
    public int level;

    public float detectRadius = 5f;
    public float returnToSpawnRadius = 25f;
    private Animator animator; 

    private Transform player;
    private NavMeshAgent agent;
    private Vector3 target;
    private Vector3 spawnLocation;

    private CharacterCombat combat;
    private CharacterStats targetStats;

    private enum EnemyStates { Passive, Attack, Hurt, Dead };
    private EnemyStates enemyState = EnemyStates.Passive;

    private float distanceToPlayer;
    private float distanceToSpawn;
    private float randomAngle;

	// Use this for initialization
	void Start ()
    {
        spawnLocation = transform.position;
        target = spawnLocation;
        agent = GetComponent<NavMeshAgent>();
        player = HealthManager.instance.player.transform;
        animator = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        distanceToPlayer = Vector3.Distance(player.position, transform.position);
        distanceToSpawn = Vector3.Distance(spawnLocation, transform.position);

        switch (enemyState)
        {
            case EnemyStates.Passive:

                if (!agent.pathPending)
                    if (agent.remainingDistance <= agent.stoppingDistance)
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                            RandomMovement();

                if (distanceToPlayer <= detectRadius && distanceToSpawn < returnToSpawnRadius)
                {
                    enemyState = EnemyStates.Attack;
                    MoveToTarget();

                }
                break;

            case EnemyStates.Attack:

                MoveToTarget();

                if (distanceToPlayer <= (agent.stoppingDistance + 0.6f))
                {
                    animator.SetBool("attack", true);
                    targetStats = player.GetComponent<CharacterStats>();
                    if (targetStats != null)
                        combat.Attack(targetStats);
                }   
                else
                    animator.SetBool("attack", false);

                if (distanceToSpawn >= returnToSpawnRadius)
                {
                    LookAtTarget(spawnLocation);
                    agent.SetDestination(spawnLocation);
                    enemyState = EnemyStates.Passive;
                }
                break;
        }   
	}

    void MoveToTarget()
    {
        agent.SetDestination(player.position);

        if (distanceToPlayer <= agent.stoppingDistance)
        {
            LookAtTarget(player.position);
        }
    }

    void LookAtTarget(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void RandomMovement()
    {
        randomAngle = Random.Range(0f, Mathf.PI * 2f);
        target = new Vector3(spawnLocation.x + 5f*Mathf.Sin(randomAngle), spawnLocation.y, spawnLocation.z + 5f*Mathf.Cos(randomAngle));
        LookAtTarget(target);
        agent.SetDestination(target);
    }

    public void AttackEnd()
    {
        HealthManager.instance.TakeDamage(4);
    }
    public float ReturnHealth()
    {
        return health * level;
    }

    public float ReturnDamage()
    {
        return damage * level;
    }

    public int ReturnLevel()
    {
        return level;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, target);

    }
}
