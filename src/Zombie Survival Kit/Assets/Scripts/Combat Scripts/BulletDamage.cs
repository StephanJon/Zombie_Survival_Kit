using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// BulletDamage: A class used to define bullet behaviour and damage enemies
/// </summary>
public class BulletDamage : MonoBehaviour
{

    [SerializeField] int damage = 5; //the damage done by the gun

    ZombieStats enemyHit; //the enemy hit by the gun

    CharacterStats playerStats;

    int modifier;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("GameController");
        playerStats = player.GetComponent<PlayerStats>();
        modifier = playerStats.dmg.GetValue();

    }

    /// <summary>  
    ///  OnTriggerEnter: This method checks for collisions between a Collider and a Trigger
    ///  and then destroys the bullet. If the object collided with is an enemy, the enemy then takes damage
    /// </summary>  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("HIT A ZOMBIE - trigger");
            enemyHit = other.gameObject.GetComponent<ZombieStats>();
            enemyHit.TakeDamage(damage + modifier);
            Destroy(gameObject);
        }
    }
}
