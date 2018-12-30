using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test_ConsumableItem {

    //[Test]
    //public void Test_ConsumableItemSimplePasses() {
    //    // Use the Assert class to test conditions.
    //}

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator Test_Use() {
        // Use the Assert class to test conditions.
        // yield to skip a frame

        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));

        ConsumableItem item1 = Resources.Load<ConsumableItem>("Items/Apple");

        GameManager.GetComponent<InventoryManager>().Add(item1);
        yield return null;
        Assert.True(GameManager.GetComponent<InventoryManager>().items.Contains(item1));

        GameManager.GetComponent<InventoryManager>().items[0].Use();
        yield return null;
        Assert.False(GameManager.GetComponent<InventoryManager>().items.Contains(item1));

        yield return null;
    }

    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("GameController"))
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("GameManager"))
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
