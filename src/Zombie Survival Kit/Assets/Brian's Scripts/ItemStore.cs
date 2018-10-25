using UnityEngine;

public class ItemStore : Interactable {

    /* A reference to the Item being interacted with
     */
    public Item item;

    /// <summary>
    /// Interact(): Is an override void method used when interacting with an Item
    /// GameObject
    /// </summary>
	public override void Interact()
    {
        base.Interact();
        StoreItem();
    }

    /// <summary>
    /// StoreItem(): Is a void method that stores the Item into the inventory
    /// </summary>
    void StoreItem()
    {
        /* Add item to inventory
         */
        bool wasPickedUp = Inventory.instance.Add(item);
        /* If item was picked up successfully, the gameobject of the item is destroyed from scene
         */
        if (wasPickedUp)
        {
            Debug.Log("Picked up " + item.name);
            Destroy(gameObject);
        }
            
    }
}
