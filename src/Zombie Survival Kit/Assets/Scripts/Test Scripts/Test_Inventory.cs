using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;

public class Test_Inventory {

    //[Test]
    //public void Test_InventorySimplePasses() {
    //    // Use the Assert class to test conditions.
    //}

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator Test_Add() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer");
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/HeadArmor");
        GameManager.GetComponent<InventoryManager>().Add(item1);

        yield return null;
        Assert.True(GameManager.GetComponent<InventoryManager>().items.Contains(item1));
    }

    [UnityTest]
    public IEnumerator Test_InventoryEquipmentConsumable(){
        // Use the Assert class to test conditions.
        // yield to skip a frame
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer");
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/HeadArmor");
        GameManager.GetComponent<InventoryManager>().Add(item1);

        yield return null;
        Assert.True(GameManager.GetComponent<InventoryManager>().items.Contains(item1));

        GameManager.GetComponent<InventoryManager>().InventoryEquipmentConsumable(item1);
        yield return null;
        Assert.False(GameManager.GetComponent<InventoryManager>().items.Contains(item1));
        Assert.IsNull(GameObject.FindGameObjectWithTag("Equipment"));

    }

    [UnityTest]
    public IEnumerator Test_RemoveFromInventory()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/HeadArmor");
        GameManager.GetComponent<InventoryManager>().Add(item1);

        yield return null;
        Assert.True(GameManager.GetComponent<InventoryManager>().items.Contains(item1));

        Debug.Log("1");
        GameManager.GetComponent<InventoryManager>().RemoveFromInventory(item1);
        Debug.Log("2");
        yield return null;
        var spawnedItem = GameObject.FindGameObjectWithTag("Equipment");
        var prefabOfSpawnedItem = PrefabUtility.GetCorrespondingObjectFromSource(spawnedItem);
        Assert.False(GameManager.GetComponent<InventoryManager>().items.Contains(item1));
        Assert.AreEqual(prefabOfSpawnedItem, Resources.Load<EquipmentItem>("PrefabItems/HeadArmor"));

        yield return null;
    }

    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("GameManager"))
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("Equipment"))
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("GameController"))
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
