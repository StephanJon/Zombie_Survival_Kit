using UnityEngine;

/// <summary>
/// InventoryUI: A class used to manage the inventory UI when items are added or removed
/// from the inventory
/// </summary>
public class InventoryUI : MonoBehaviour
{

    /* Used to references the parent of all of the inventory slots
     */
    [SerializeField] private Transform itemsParent;
    /* Used to reference the Canvas of the inventoryUI
     */
    //[SerializeField] private Canvas inventoryUI;

    //Shiv's part
    public Canvas inventoryUI;
    //End of Shiv's part

    /* Used to reference the the inventory instance
     */
    private InventoryManager inventory;

    /* Used to references all of the inventory slots
     */
    private InventorySlot[] slots;


    /// <summary>
    /// Start: Is a void method used for initialization
    /// </summary>
    void Start()
    {
        inventory = InventoryManager.instance;
        // onItemChangedCcallBack triggers UpdateUI
        inventory.onItemChangedCallback += UpdateInventoryUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    /// <summary>
    /// Update: Is a void method that is called once per frame
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryUI.enabled)
            {
                inventoryUI.enabled = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

            }
            else
            {
                inventoryUI.enabled = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

            }

        }
    }

    /// <summary>
    /// UpdateInventoryUI: Is a void method that updates the inventory UI when an item is added
    /// or removed from the invenotry.
    /// </summary>
    private void UpdateInventoryUI()
    {
        Debug.Log("Updating UI");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].addItem(inventory.items[i]);
            }
            else
            {
                slots[i].clearSlot();
            }
        }
    }
}
