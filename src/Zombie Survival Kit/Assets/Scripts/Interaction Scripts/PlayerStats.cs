using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// PlayerStats: A class used to manage stats specific to the player, inherited from CharacterStats
/// </summary>
public class PlayerStats : CharacterStats
{
    /// <summary>
    /// Singleton: Is a region used to create an instance of the HealthManager
    /// class
    /// </summary>
    #region Singleton
    public static PlayerStats instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    //Reference to the equipment manager
    EquipmentManager equipmentManager;
    

    //Reference to the first person controller
    public GameObject player;

    [SerializeField]
    GameObject playerUI; //A game object used to refer to the UI overlay

    [SerializeField] AudioClip axeSound; //audio clip for axe sound
    [SerializeField] AudioClip punchSound; //audio clip for punch sound

    /// <summary>
    /// Start: Is a void method used for initialization
    /// </summary>
    void Start ()
    {
        //Initializes the health and method that is invoked in EquipmentManager in the equip and dequip methods
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        equipmentManager = EquipmentManager.instance;
        curHealth = maxHealth;
	}

    /// <summary>
    /// OnEquipmentChanged: A method to modify the player stats when items are equipped or dequipepd 
    /// </summary>
    /// <param name="newItem">The new item about to be equippedy</param>
    /// <param name="oldItem">The old item about to be dequipped</param>
    public void OnEquipmentChanged(EquipmentItem newItem, EquipmentItem oldItem)
    {
        if (newItem != null) //Add the new item's modifiers to each respective stat
        {
            armour.AddToStat(newItem.defenceModifier);
            dmg.AddToStat(newItem.attackModifier);
        }

        if (oldItem != null) //Remove the old item's modifiers from each respective stat
        {
            armour.RemoveFromStat(oldItem.defenceModifier);
            dmg.RemoveFromStat(oldItem.attackModifier);
        }

    }

    /// <summary>
    /// Eat(ConsumableItem consumable): Is a void method that, upon use,
    /// enables the player to use the ConsumableItem to restore the player's
    /// health
    /// </summary>
    /// <param name="consumable">The ConsumableItem being used</param>
    public void Eat(ConsumableItem consumable)
    {
        if (curHealth < maxHealth) //Do not do anything if they have maximum health
        {
            if (curHealth + consumable.healthModifier <= maxHealth) //If healing them does not bring them above max health
            {
                curHealth += consumable.healthModifier; //Add the consumabe's health modifier to the current health
            }
            else //Do not give them above maximum health
            {
                curHealth = maxHealth;
            }

        }
    }

    /// <summary>
    /// Die: An overwritten Die method to control what happens when the player dies
    /// </summary>
    public override void Die()
    {
        //Call the base method and reset the scene
        base.Die();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// MeleeSound: A method to play the punch or axe sound depending on if an axe is equipped
    /// </summary>
    public void MeleeSound()
    {
        if (equipmentManager.IsAxeEquipped())
            AudioSource.PlayClipAtPoint(axeSound, transform.position);
        else
            AudioSource.PlayClipAtPoint(punchSound, transform.position);
    }
}
