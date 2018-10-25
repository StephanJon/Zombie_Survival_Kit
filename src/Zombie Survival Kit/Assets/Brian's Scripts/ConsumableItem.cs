using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Used to create an asset menu for making consumable items
 */
[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]

/// <summary>
/// ConsumeableItem: A class used to distinguish Item GameObjects as a ConsumableItem
/// and what the player can do with these items
/// </summary>
public class ConsumableItem : Item
{
    /* Determines how much health is restored to the player if the player uses the
     * ConsumableItem
     */
    public int healthModifier;

    /// <summary>
    /// Use(): An override void method used when the player wants to use the 
    /// ConsumableItem
    /// </summary>
    public override void Use()
    {
        base.Use();
        HealthManager.instance.Eat(this);
        RemoveFromInventory();
        
    }
}

