using UnityEngine;

/// <summary>
/// EquipmentManager: Is a class used to keep track of the player's equipped items, and 
/// consists of methods that can affect the player's equipped items.
/// </summary>
public class EquipmentManager : MonoBehaviour
{

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
    [SerializeField] public EquipmentItem[] equippedItems;

    /* Used as a reference to the inventory
     */
    private InventoryManager inventory;

    [SerializeField] GameObject gunUI;
    [SerializeField] GameObject axeUI;
    [SerializeField] GameObject fpsController; // the FirstPersonCharacter child of the player
    [SerializeField] GameObject gun; //the gun
    GameObject equippedGun;

    //Booleans to check if each type of weapon is equipped
    private bool isGunEquipped = false;
    private bool isAxeEquipped = false;


    /// <summary>
    /// Start: Is a void method used for initialization
    /// </summary>
	void Start()
    {
        fpsController = GameObject.FindGameObjectWithTag("GameController");
        gunUI = GameObject.FindGameObjectWithTag("GunUI");
        axeUI = GameObject.FindGameObjectWithTag("AxeUI");
        int numSlots = System.Enum.GetNames(typeof(equipmentSlot)).Length;
        equippedItems = new EquipmentItem[numSlots];

        inventory = InventoryManager.instance;
    }

    /// <summary>
    /// Update: Is a void method that is called once per frame
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
        if (equippedItems[(int)equipmentSlot.Primaryhand] == null)
        {
            axeUI.GetComponent<Canvas>().enabled = false;
            gunUI.GetComponent<Canvas>().enabled = false;
        }
    }

    /// <summary>
    /// Equip: Is a void method used to equip an EquipmentItem into the 
    /// EquipmentItem[].
    /// </summary>
    /// <param name="newEquipment">The EquipmentItem being equipped</param>
    public void Equip(EquipmentItem newEquipment)
    {
        int slotIndex = (int)newEquipment.equipSlot;
        if (slotIndex == (int)equipmentSlot.Primaryhand)
        {
            if (newEquipment.name == "Axe")
            {
                axeUI.GetComponent<Canvas>().enabled = true;
                gunUI.GetComponent<Canvas>().enabled = false;
                isAxeEquipped = true;
            }
            else
            {
                axeUI.GetComponent<Canvas>().enabled = false;
                gunUI.GetComponent<Canvas>().enabled = true;
                isAxeEquipped = false;
            }
        }

        EquipmentItem oldEquipment = null;

        if (equippedItems[slotIndex] != null)
        {
            oldEquipment = equippedItems[slotIndex];
            inventory.Add(oldEquipment);
            if (oldEquipment.name == "RangeWeapon")
            {
                Destroy(GameObject.FindGameObjectWithTag("Gun").gameObject);
                isGunEquipped = false;
            }
        }

        equippedItems[slotIndex] = newEquipment;

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newEquipment, oldEquipment);
        }

        //Shiv's part
        if (newEquipment.name == "RangeWeapon" && isGunEquipped == false)
        {
            //set to active
            equippedGun = Instantiate(gun, fpsController.transform.position, Quaternion.identity);
            isGunEquipped = true;
            Debug.Log("Gun equipped");
        }
        //End of Shiv's part
    }

    /// <summary>
    /// Unequip: Is a void method used to unequip an EquipmentItem from its slot index 
    /// in the EquipmentItem[].
    /// </summary>
    /// <param name="slotIndex">The slot index of the equipment being uneuipped</param>
    public void Unequip(int slotIndex)
    {
        if (equippedItems[slotIndex] != null)
        {
            EquipmentItem oldEquipment = equippedItems[slotIndex];
            inventory.Add(oldEquipment);

            //Shiv's part
            if (equippedItems[slotIndex].name == "RangeWeapon")
            {
                //set to active
                Destroy(GameObject.FindGameObjectWithTag("Gun").gameObject);
                isGunEquipped = false;
                Debug.Log("Gun unequipped");
            }
            else if (equippedItems[slotIndex].name == "Axe")
            {
                isAxeEquipped = false;
            }
            // End of Shiv's part
            equippedItems[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldEquipment);
            }
        }
    }

    /// <summary>
    /// UnequipAll: A method used to unequip any EquipmentItem currently equipped
    /// </summary>
    public void UnequipAll()
    {
        for (int i = 0; i < equippedItems.Length; i++)
        {
            Unequip(i);
        }
    }

    /// <summary>
    /// IsAxeEquipped: A method used to unequip any EquipmentItem currently equipped
    /// </summary>
    public bool IsAxeEquipped()
    {
        return isAxeEquipped;
    }

    /// <summary>
    /// IsGunEquipped: A method used to unequip any EquipmentItem currently equipped
    /// </summary>
    public bool IsGunEquipped()
    {
        return isGunEquipped;
    }
}
