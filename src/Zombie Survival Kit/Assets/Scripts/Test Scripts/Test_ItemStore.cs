using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test_ItemStore {

    //[Test]
    //public void Test_ItemStoreSimplePasses() {
    //    // Use the Assert class to test conditions.
    //}

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator Test_Interact() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        GameObject GameItem = GameObject.Instantiate(Resources.Load<GameObject>("PrefabItems/HeadArmor"));
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));
        EquipmentItem item = Resources.Load<EquipmentItem>("Items/HeadArmor");
        yield return null;

        GameItem.GetComponent<ItemStore>().Interact();
        yield return null;
        Assert.True(GameManager.GetComponent<InventoryManager>().items.Contains(item));
        Assert.IsNull(GameObject.FindGameObjectWithTag("Equipment"));

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
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("Equipment"))
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
