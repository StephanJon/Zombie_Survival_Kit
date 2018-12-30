using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// EquipmentSlot: A class used to manage the slots in the equipment UI, and includes
/// the methods that update these slots.
/// </summary>
public class EquipmentSlot : MonoBehaviour
{
    
    /* A reference to the icon of the equipment item being stored
    */
    [SerializeField] private Image icon;

    /* A reference to the remove button on the equipment slot
     */
    [SerializeField] private Button removeButton;

    /* A reference to the slot number
     */
    [SerializeField] private equipmentSlot slotNumber;

    /* A reference to the item being stored in the equipment UI and list
     */
    private EquipmentItem item;

    /// <summary>
    /// addItem: Is a void method that adds an item to the equipment 
    /// slot
    /// </summary>
    /// <param name="newEquipment">The equipment being added to the equipment UI</param>
    public void addItem(EquipmentItem newEquipment)
    {
        item = newEquipment;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    /// <summary>
    /// clearSlot: A void method used to clear the images on the inventory slot in 
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
    /// onRemoveButton: a private void method that Removes the item from the equipment and moves it to the
    /// the inventory.
    /// </summary>
    private void onRemoveButton()
    {
        EquipmentManager.instance.Unequip((int)slotNumber);
    }
}
