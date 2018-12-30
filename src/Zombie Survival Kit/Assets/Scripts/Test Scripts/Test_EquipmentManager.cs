using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;

public class Test_EquipmentManager {

    //[Test]
    //public void Test_EquipmentManagerSimplePasses() {
    //    // Use the Assert class to test conditions.
    //}

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator Test_Equip() {
        // Use the Assert class to test conditions.
        // yield to skip a frame

        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        GameObject InventoryUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/InventoryUI"));
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));
        yield return null;
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/ChestArmor");
        EquipmentItem item2 = Resources.Load<EquipmentItem>("Items/Cloak");
        EquipmentItem item3 = Resources.Load<EquipmentItem>("Items/RangeWeapon");

        GameManager.GetComponent<EquipmentManager>().Equip(item1);
        yield return null;
        Assert.True(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item1.equipSlot] == item1);

        GameManager.GetComponent<EquipmentManager>().Equip(item2);
        yield return null;
        Assert.True(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item2.equipSlot] == item2);
        Assert.True(GameManager.GetComponent<InventoryManager>().items.Contains(item1));

        Assert.IsNull(GameObject.FindGameObjectWithTag("Gun"));
        GameManager.GetComponent<EquipmentManager>().Equip(item3);
        yield return null;
        Assert.True(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item3.equipSlot] == item3);
        var spawnedItem = GameObject.FindGameObjectWithTag("Gun");
        Assert.IsNotNull(spawnedItem);


        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_Unequip()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame

        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        GameObject InventoryUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/InventoryUI"));
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));
        yield return null;
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/ChestArmor");

        GameManager.GetComponent<EquipmentManager>().Equip(item1);
        yield return null;
        Assert.True(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item1.equipSlot] == item1);

        GameManager.GetComponent<EquipmentManager>().Unequip((int)item1.equipSlot);
        Assert.True(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item1.equipSlot] == null);
        Assert.True(GameManager.GetComponent<InventoryManager>().items.Contains(item1));

        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_UnequipAll()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame

        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        GameObject InventoryUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/InventoryUI"));
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));
        yield return null;
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/ChestArmor");
        EquipmentItem item2 = Resources.Load<EquipmentItem>("Items/HeadArmor");
        EquipmentItem item3 = Resources.Load<EquipmentItem>("Items/Axe");
        EquipmentItem item4 = Resources.Load<EquipmentItem>("Items/FeetArmor");
        EquipmentItem item5 = Resources.Load<EquipmentItem>("Items/OffHand");
        EquipmentItem item6 = Resources.Load<EquipmentItem>("Items/LegArmor");

        GameManager.GetComponent<EquipmentManager>().Equip(item1);
        GameManager.GetComponent<EquipmentManager>().Equip(item2);
        GameManager.GetComponent<EquipmentManager>().Equip(item3);
        GameManager.GetComponent<EquipmentManager>().Equip(item4);
        GameManager.GetComponent<EquipmentManager>().Equip(item5);
        GameManager.GetComponent<EquipmentManager>().Equip(item6);

        yield return null;
        Assert.True(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item1.equipSlot] == item1);
        Assert.True(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item2.equipSlot] == item2);
        Assert.True(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item3.equipSlot] == item3);
        Assert.True(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item4.equipSlot] == item4);
        Assert.True(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item5.equipSlot] == item5);
        Assert.True(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item6.equipSlot] == item6);

        GameManager.GetComponent<EquipmentManager>().UnequipAll();

        Assert.IsNull(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item1.equipSlot]);
        Assert.IsNull(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item2.equipSlot]);
        Assert.IsNull(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item3.equipSlot]);
        Assert.IsNull(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item4.equipSlot]);
        Assert.IsNull(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item5.equipSlot]);
        Assert.IsNull(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item6.equipSlot]);

        Assert.True(GameManager.GetComponent<InventoryManager>().items.Contains(item1));
        Assert.True(GameManager.GetComponent<InventoryManager>().items.Contains(item2));
        Assert.True(GameManager.GetComponent<InventoryManager>().items.Contains(item3));
        Assert.True(GameManager.GetComponent<InventoryManager>().items.Contains(item4));
        Assert.True(GameManager.GetComponent<InventoryManager>().items.Contains(item5));
        Assert.True(GameManager.GetComponent<InventoryManager>().items.Contains(item6));

        yield return null;
    }

    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("GameManager"))
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("GameController"))
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("Gun"))
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("GunUI"))
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("AxeUI"))
        {
            Object.Destroy(gameobject);
        }
    }
}
