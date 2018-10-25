using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inventory: A class used to manage the player's inventory, and includes methods
/// that updates a player's inventory
/// </summary>
public class Inventory : MonoBehaviour
{
    /// <summary>
    /// Singleton: Is a region used to create an instance of the Inventory class
    /// </summary>
    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    /// <summary>
    /// Keeps track on when an item is added or removed in the inventory.
    /// If a change has happened, trigger an event.
    /// Used for updating the inventory UI later.
    /// </summary>
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    /* The amount of space the inventory can have
     */
    public int space;

    /* The inventory list
     */
    public List<Item> items = new List<Item>();
    
    /// <summary>
    /// Add(Item item): A bool method used to add an item to the inventory
    /// </summary>
    /// <param name="item">The item being added to the inventory</param>
    /// <returns>true or false</returns>
    public bool Add (Item item)
    {
        // Checks to see if the item being picked up is a default item
        if (!item.isDefaultItem)
        {
            // Checks if the inventory does not have enough space
            if (items.Count >= space)
            {
                Debug.Log("Not enough room in the inventory");
                return false;
            }
            // Add the item to the inventory
            items.Add(item);

            // Invoke a change to the inventory UI 
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
            
            
        }

        return true;
    }

    
    /// <summary>
    /// Remove(Item item): A void method used to remove items to the inventory
    /// </summary>
    /// <param name="item">The item being removed from the inventory</param>
    public void Remove (Item item)
    {
        items.Remove(item);

        // Invoke a change to the inventory UI 
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}
