using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test_Stat
{
    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator Test_AddToStat()
    {
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));
        Stat damage = player.GetComponent<PlayerStats>().dmg;

        damage.AddToStat(0);
        yield return null;
        Assert.False(damage.GetStatChanges().Contains(0));

        damage.AddToStat(5);
        yield return null;
        Assert.True(damage.GetStatChanges()[0] == 5);

        damage.AddToStat(3);
        yield return null;
        Assert.True(damage.GetStatChanges()[1] == 3);
    }

    [UnityTest]
    public IEnumerator Test_GetValue()
    {
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));
        Stat damage = player.GetComponent<PlayerStats>().dmg;

        damage.AddToStat(10);
        damage.AddToStat(7);
        yield return null;

        Assert.True(damage.GetValue() == damage.GetInitialValue() + 17);
    }

    [UnityTest]
    public IEnumerator Test_RemoveFromStat()
    {
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));
        Stat damage = player.GetComponent<PlayerStats>().dmg;

        damage.AddToStat(0);
        damage.AddToStat(5);
        damage.AddToStat(3);
        damage.AddToStat(3);

        damage.RemoveFromStat(5);
        yield return null;
        Assert.False(damage.GetStatChanges().Contains(5));

        damage.RemoveFromStat(3);
        yield return null;
        //Assert.True(damage.GetStatChanges().Contains(3));

        damage.RemoveFromStat(3);
        yield return null;
        //Assert.False(damage.GetStatChanges().Contains(3));
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
