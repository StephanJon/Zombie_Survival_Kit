using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inventory: A class used to manage the player's inventory, and includes methods
/// that updates a player's inventory
/// </summary>
public class InventoryManager : MonoBehaviour
{
    /// <summary>
    /// Singleton: Is a region used to create an instance of the Inventory class
    /// </summary>
    #region Singleton

    public static InventoryManager instance;

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
    /// OnItemChanged: A delagate void method that keeps track on when an item is added 
    /// or removed in the inventory. 
    /// If a change has happened, trigger an event.
    /// Used for updating the inventory UI later.
    /// </summary>
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    /* A reference to the player
     */
    [SerializeField] private GameObject player;

    /* The amount of space the inventory can have
     */
    [SerializeField] private int space;

    /* The inventory list
     */
    public List<Item> items = new List<Item>();

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    /// <summary>
    /// Add: A bool method used to add an item to the inventory
    /// </summary>
    /// <param name="item">The item being added to the inventory</param>
    /// <returns>true or false</returns>
    public bool Add(Item item)
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

        return true;
    }


    /// <summary>
    /// RemoveFromInventory: A void method used to remove items from the inventory
    /// </summary>
    /// <param name="item">The item being removed from the inventory</param>
    public void RemoveFromInventory(Item item)
    {
        string itemPath = "PrefabItems/" + item.name;
        GameObject droppedItem = Instantiate(Resources.Load<GameObject>(itemPath)) as GameObject;
        droppedItem.transform.position = player.transform.position + player.transform.forward * 2;
        items.Remove(item);

        // Invoke a change to the inventory UI 
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    /// <summary>
    /// InventoryEquipmentConsumable: A void method used to remove an item from the
    /// inventory when the item is used
    /// </summary>
    /// <param name="item"></param>
    public void InventoryEquipmentConsumable(Item item)
    {
        items.Remove(item);

        // Invoke a change to the inventory UI 
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}
