using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test_PlayerStats
{
    [UnityTest]
    public IEnumerator Test_Eat()
    {
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));
        PlayerStats stats = player.GetComponent<PlayerStats>();
        ConsumableItem item1 = Resources.Load<ConsumableItem>("Items/Apple");
        yield return null;

        //Testing TakeDamage from CharacterStats to reduce hp so the item can be used to test Eat
        Assert.True(stats.curHealth == 100);
        stats.TakeDamage(51);
        yield return null;
        Assert.True(stats.curHealth == 49);

        stats.Eat(item1);
        yield return null;
        Assert.True(stats.curHealth == 49 + item1.healthModifier);
    }

    [UnityTest]
    public IEnumerator Test_OnEquipmentChanged()
    {
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));
        PlayerStats stats = player.GetComponent<PlayerStats>();
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/Axe");
        EquipmentItem item2 = Resources.Load<EquipmentItem>("Items/RangeWeapon");
        yield return null;

        int oldArmour = stats.armour.GetValue();
        int oldDamage = stats.dmg.GetValue();
        stats.OnEquipmentChanged(item1, null);

        yield return null;
        Assert.True(stats.armour.GetValue() == oldArmour + item1.defenceModifier);
        Assert.True(stats.dmg.GetValue() == oldDamage + item1.attackModifier);

        oldArmour = stats.armour.GetValue();
        oldDamage = stats.dmg.GetValue();
        stats.OnEquipmentChanged(item2, item1);

        yield return null;
        Assert.True(stats.armour.GetValue() == oldArmour + item2.defenceModifier - item1.defenceModifier);
        Assert.True(stats.dmg.GetValue() == oldDamage + item2.attackModifier - item1.attackModifier);

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
