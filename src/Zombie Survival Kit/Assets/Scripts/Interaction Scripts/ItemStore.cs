using UnityEngine;

/// <summary>
/// ItemStore: Is a class that inherates the Interactable class. It is used to 
/// store any gameobject that has this class as a component attatched when the
/// player interactes with the gameobject.
/// </summary>
public class ItemStore : Interactable
{

    /* A reference to the Item being interacted with
     */
    [SerializeField] private Item item;

    /// <summary>
    /// Interact: Is an override void method used when interacting with an Item
    /// GameObject
    /// </summary>
	public override void Interact()
    {
        base.Interact();
        StoreItem();
    }

    /// <summary>
    /// StoreItem: Is a void method that stores the Item into the inventory
    /// </summary>
    private void StoreItem()
    {
        /* Add item to inventory
         */
        bool wasPickedUp = InventoryManager.instance.Add(item);
        /* If item was picked up successfully, the gameobject of the item is destroyed from scene
         */
        if (wasPickedUp)
        {
            Debug.Log("Picked up " + item.name);
            Destroy(gameObject);
        }

    }
}
