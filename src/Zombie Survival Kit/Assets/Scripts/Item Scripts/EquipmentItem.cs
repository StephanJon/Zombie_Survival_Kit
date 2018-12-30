﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Used to create an asset menu for making equipment items
 */
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]

/// <summary>
/// EquipmentItem: A class used to distinguish Item GameObjects as a EquipmentItem
/// and what the player can do with these items
/// </summary>
public class EquipmentItem : Item
{
    /* Determines which Equipment slot the EmuipmentItem belongs to when equipped to
     * The player
     */
    [SerializeField] public equipmentSlot equipSlot;

    /* Determines how much attack the equipment can produce to an enemy
     */
    [SerializeField] public int attackModifier;
    /* Determines how much defence the equipment can provide for the player
     */
    [SerializeField] public int defenceModifier;

    /// <summary>
    /// Use: An override void method used when the player wants to use the 
    /// EquipmentItem
    /// </summary>
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        InventoryManager.instance.InventoryEquipmentConsumable(this);

    }

}

/// <summary>
/// EquipmentSlot: An enum that determines the different types of equipment
/// </summary>
public enum equipmentSlot { Head, Chest, Legs, Primaryhand, Offhand, Feet }
