using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EquipmentManager: Is a class used to keep track of the player's equipped items, and 
/// consists of methods that can affect the player's equipped items.
/// </summary>
public class EquipmentManager : MonoBehaviour {

    /// <summary>
    /// Singleton: Is a region used to create an instance of the HealthManager
    /// class
    /// </summary>
    #region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    /// <summary>
    /// OnEquipmentChanged: Will be used later to show that an equipment change 
    /// has occured on the character. Does nothing for now.
    /// </summary>
    public delegate void OnEquipmentChanged(EquipmentItem newEquipment, EquipmentItem oldEquippment);
    public OnEquipmentChanged onEquipmentChanged;

    /* A EquipementItem array used to hold all equipped items
     */
    public EquipmentItem[] equippedItems;

    /* Used as a reference to the inventory
     */
    Inventory inventory;

    /// <summary>
    /// Start(): Is a void method used for initialization
    /// </summary>
	void Start () {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        equippedItems = new EquipmentItem[numSlots];

        inventory = Inventory.instance;
	}

    /// <summary>
    /// Update(): Is a void method that is called once per frame
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }

    /// <summary>
    /// Equip(EquipmentItem newEquipment):Is a void method used to equip an
    /// EquipmentItem into the EquipmentItem[].
    /// </summary>
    /// <param name="newEquipment">The EquipmentItem being equipped</param>
    public void Equip(EquipmentItem newEquipment)
    {
        int slotIndex = (int)newEquipment.equipSlot;

        EquipmentItem oldEquipment = null;

        if(equippedItems[slotIndex] != null)
        {
            oldEquipment = equippedItems[slotIndex];
            inventory.Add(oldEquipment);
        }

        equippedItems[slotIndex] = newEquipment;

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newEquipment, oldEquipment);
        }
    }

    /// <summary>
    /// Unequip(int SlotIndex):Is a void method used to unequip an
    /// EquipmentItem from its slot index in the EquipmentItem[].
    /// </summary>
    /// </summary>
    /// <param name="SlotIndex">The slot index of the equipment being uneuipped</param>
    public void Unequip (int SlotIndex)
    {
        if(equippedItems[SlotIndex] != null)
        {
            EquipmentItem oldEquipment = equippedItems[SlotIndex];
            inventory.Add(oldEquipment);

            equippedItems[SlotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldEquipment);
            }
        }
    }

    /// <summary>
    /// UnequipAll(): A method used to unequip any EquipmentItem currently equipped
    /// </summary>
    public void UnequipAll()
    {
        for (int i = 0; i < equippedItems.Length; i++)
        {
            Unequip(i);
        }
    }

}
