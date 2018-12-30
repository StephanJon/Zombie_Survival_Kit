using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EquipmentUI: A class used to manage the equipment UI when items are added or removed
/// from the equipment slots
/// </summary>
public class EquipmentUI : MonoBehaviour
{

    /* Used to references the parent of all of the equipment slots
     */
    [SerializeField] private Transform equipmentParent;
    /* Used to reference the Canvas of the equipment UI
     */
    [SerializeField] private Canvas equipmentUI;

    /* Used to references all of the equipment slots
     */
    [SerializeField] private EquipmentSlot[] equipmentSlots;

    /* Used to reference the the EquipmentManager instance
     */
    private EquipmentManager equipment;

    /// <summary>
    /// Start: Is a void method used for initialization
    /// </summary>
    void Start()
    {
        equipment = EquipmentManager.instance;
        // onItemChangedCcallBack triggers UpdateUI
        equipment.onEquipmentChanged += UpdateEquipmentUI;

        equipmentSlots = equipmentParent.GetComponentsInChildren<EquipmentSlot>();
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
            if (!equipmentUI.enabled)
            {
                equipmentUI.enabled = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

            }
            else
            {
                equipmentUI.enabled = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

            }

        }
    }

    /// <summary>
    /// UpdateEquipmentUI: Is a private void method that updates the equipment UI when an Equipment 
    /// item is added or removed from the corresponding inventory slot.
    /// </summary>
    private void UpdateEquipmentUI(EquipmentItem New, EquipmentItem Old)
    {
        Debug.Log("Updating Equipment UI");
        if (New == null)
        {
            equipmentSlots[(int)Old.equipSlot].clearSlot();
        }
        else
        {
            equipmentSlots[(int)New.equipSlot].addItem(New);
        }
    }

}