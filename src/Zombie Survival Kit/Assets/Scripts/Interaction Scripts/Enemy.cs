using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy: A class inherited from Interactable used to control the player interacting with the zombie
/// </summary>
[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    //Objects used to hold the stats for the player and the enemy
    private PlayerStats playerManager;
    private CharacterStats enemyStats;

    /// <summary>
    /// Start: Is a void method used for stats initialization
    /// </summary>
    private void Start()
    {
        playerManager = PlayerStats.instance;
        enemyStats = GetComponent<CharacterStats>();
    }

    /// <summary>
    /// Interact: An overwritten Interact method to control what happens when the player clicks on the enemy
    /// </summary>
    public override void Interact()
    {
        //Call the base method and gain a reference to the player's combat methods
        base.Interact();
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();

        if (playerCombat != null)
        {
            //Attack the enemy which reduces its health
            playerCombat.Attack(enemyStats);
        }
    }
}
