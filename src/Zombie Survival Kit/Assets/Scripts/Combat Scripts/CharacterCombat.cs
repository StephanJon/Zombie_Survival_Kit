using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CharacterCombat: A class to manage combat between characters
/// </summary>
[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    //Variables to control how fast a character can attack and a timer/delay between attacks
    [SerializeField] float attackSpeed = 0.8f;
    private float attackCooldown = 0f;

    //Reference to the stats of the attacker and player
    private CharacterStats attackerStats;
    private PlayerStats playerManager;

    /// <summary>
    /// Start: Is a void method used for stats initialization
    /// </summary>
    private void Start()
    {
        attackerStats = GetComponent<CharacterStats>();
        playerManager = PlayerStats.instance;
    }

    /// <summary>
    /// Update: Is a void method that is called once per frame
    /// </summary>
    private void Update()
    {
        attackCooldown -= Time.deltaTime; //Lowers the cooldown timer
    }

    /// <summary>
    /// Attack: A method to initiate an attack between 2 characters 
    /// </summary>
    /// <param name="targetStats">The stats of the character being attacked</param>
    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f) //If the cooldown timer has reached 0
        {
            targetStats.TakeDamage(attackerStats.dmg.GetValue());
            attackCooldown = 1f / attackSpeed;

            if (attackerStats.isPlayer)
                playerManager.MeleeSound();
        }
    }
}
