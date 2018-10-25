using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// InventorySlot: A class used to manage the slots in the inventory UI, and includes
/// the methods that update these slots.
/// </summary>
public class InventorySlot : MonoBehaviour {

    /* A reference to the icon of the item being stored
     */
    public Image icon;

    /* A reference to the remove button on the inventory slot
     */
    public Button removeButton;

    /* A reference to the item being stored in the inventory UI and list
     */
    Item item;

    /// <summary>
    /// addItem(Item newItem): Is a void method that adds an item to the inventory slot
    /// </summary>
    /// <param name="newItem"></param>
    public void addItem (Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }
    
    /// <summary>
    /// clearSlot(): A void method used to clear the images on the inventory slot in 
    /// the inventory UI
    /// </summary>
    public void clearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }
        
    /// <summary>
    /// onRemoveButton(): a void method that Removes the item in the inventory from the 
    /// inventory list when the remove button is pressed
    /// </summary>
    public void onRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    /// <summary>
    /// useItem(): A void method that uses the item in the inventory from the inventory 
    /// list when the inventory slot button is pressed
    /// </summary>
    public void useItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
