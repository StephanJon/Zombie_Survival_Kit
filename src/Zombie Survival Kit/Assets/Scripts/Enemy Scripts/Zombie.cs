using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Zombie: A class used to manage zombie movement and behaviours
/// </summary>
public class Zombie : MonoBehaviour
{
    [SerializeField] bool campType; //Used to determine if the zombie is idle wanders around initially

    //Radiuses to determine when the zombie detects a player and when it walks back to its spawn point
    [SerializeField] float detectRadius = 5f;
    [SerializeField] float returnToSpawnRadius = 25f;

    //Zombie distance from the player and spawn point
    private float distanceToPlayer;
    private float distanceToSpawn;

    //Animator and NavMesh to control zombie animation and movement
    private Animator animator;
    private NavMeshAgent agent;

    //Reference to player position
    private Transform player;

    //Co-ordinates of the spawnlocation and target zombie is walking to
    private Vector3 target;
    private Vector3 spawnLocation;

    //Objects to control the combat between the player and zombie
    private CharacterCombat enemyCombat;
    private CharacterStats playerStats;
    private ZombieStats zombieStats;

    //Enum used to describe different states the zombie can be in
    private enum EnemyStates { Passive, Attack };
    private EnemyStates enemyState = EnemyStates.Passive;
    
    //The random angle zombie uses for wandering around 
    private float randomAngle;

    /// <summary>
    /// Start: Is a void method used for stats initialization
    /// </summary>
    /// 
	void Start ()
    {
        //Initializes zombie spawn location, nav mesh agent & animator, reference to player, and combat/stats components
        spawnLocation = transform.position;
        target = spawnLocation;
        agent = GetComponent<NavMeshAgent>();
        player = PlayerStats.instance.player.transform;
        animator = GetComponent<Animator>();
        enemyCombat = GetComponent<CharacterCombat>();
        zombieStats = GetComponent<ZombieStats>();
    }

    /// <summary>
    /// Update: Is a void method that is called once per frame
    /// </summary>
    void Update ()
    {
        if (!zombieStats.IsDead()) //If the zombie is not dead
        {
            //Calculate its distance to the player and spawn point
            distanceToPlayer = Vector3.Distance(player.position, transform.position);
            distanceToSpawn = Vector3.Distance(spawnLocation, transform.position);

            switch (enemyState)
            {
                case EnemyStates.Passive: //If the enemy is not attacking the player

                        if (!agent.pathPending)
                            if (agent.remainingDistance <= agent.stoppingDistance)
                                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                                    if (campType)
                                        animator.SetBool("idle", true); //If it is a camp type, simply animate the idle animation
                                    else
                                        RandomMovement(); //If it is not a camp type, begin wandering around

                    //If the player gets close enough or the player htis the zombie and the zombie is not walking back
                    if ((distanceToPlayer <= detectRadius || zombieStats.curHealth < 100) && distanceToSpawn < returnToSpawnRadius)
                    {
                        //Switch to attacking state and follow the player
                        animator.SetBool("idle", false);
                        enemyState = EnemyStates.Attack;
                        MoveToTarget();

                    }
                    break;

                case EnemyStates.Attack: //If the enemy is attacking the player

                    MoveToTarget(); //Continously follow the player

                    //If the player is within attacking distance
                    if (distanceToPlayer <= (agent.stoppingDistance + 0.6f))
                    {
                        //play the attack animation
                        animator.SetBool("attack", true);
                        playerStats = player.GetComponent<CharacterStats>();

                        if (playerStats != null)
                            enemyCombat.Attack(playerStats); //Damage the player
                    }
                    else
                        animator.SetBool("attack", false); //Stop the animation if the player moves out of range

                    //If the zombie gets too far from its spawn
                    if (distanceToSpawn >= returnToSpawnRadius)
                    {
                        //Return back to spawn, reset health, and switch to passive state
                        LookAtTarget(spawnLocation);
                        agent.SetDestination(spawnLocation);
                        zombieStats.curHealth = zombieStats.maxHealth;
                        enemyState = EnemyStates.Passive;
                    }
                    break;
            }
        }
	}

    /// <summary>
    /// MoveToTarget: A method that allows the zombie to follow the player
    /// </summary>
    private void MoveToTarget()
    {
        agent.SetDestination(player.position); //Sets position to follow the player

        if (distanceToPlayer <= agent.stoppingDistance)
        {
            LookAtTarget(player.position); //Sets rotation to turn to the player's direction
        }
    }

    /// <summary>
    /// LookAtTarget: A method that allows the zombie to turn its body to face a certain direction
    /// </summary>
    /// /// <param name="destination">The position to look at</param>
    private void LookAtTarget(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    /// <summary>
    /// RandomMovement: A method that allows the zombie to move in random directions, imitating wandering
    /// </summary>
    private void RandomMovement()
    {
        if (!zombieStats.IsDead()) //If the zombie is not dead
        {
            //Pick a random angle in radians and move 5 units towards it
            randomAngle = Random.Range(0f, Mathf.PI * 2f);
            target = new Vector3(spawnLocation.x + 6f * Mathf.Sin(randomAngle), spawnLocation.y, spawnLocation.z + 6f * Mathf.Cos(randomAngle));
            LookAtTarget(target);
            agent.SetDestination(target);
        }
    }

    /// <summary>
    /// OnDrawGizmos: A method that draws on the zombies to visualize certain variables
    /// </summary>
    private void OnDrawGizmos()
    {
        //Draws a blue line from the zombie to where it is currently moving to
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, target);

        //Draws a red sphere around the zombie's detection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);

    }
}
