using UnityEngine;

/*Used to create an asset menu for making items
 */
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

/// <summary>
/// Item: A class used to distinguish GameObjects as an Item and what the player can 
/// do with these items
/// </summary>
public class Item : ScriptableObject
{
    /* The name of the item
     */
    new public string name = "New Item";
    /* The icon of the item
     */
    public Sprite icon = null;
    /* A bool used to check if it is a default item
     */
    public bool isDefaultItem = false;

    /// <summary>
    /// Use(): Is a virtual void method used when the player wants to use the item.
    /// </summary>
    public virtual void Use ()
    {
        Debug.Log("Using " + name);
    }
	
    /// <summary>
    /// RemoveFromInventory(): Removes the item from the inventory
    /// </summary>
    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
