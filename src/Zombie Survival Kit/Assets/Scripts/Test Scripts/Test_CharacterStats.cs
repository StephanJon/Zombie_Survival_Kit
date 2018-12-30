using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test_CharacterStats
{
    [UnityTest]
    public IEnumerator Test_TakeDamage()
    {
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));

        CharacterStats stats = player.GetComponent<CharacterStats>();
        yield return null;

        //Testing TakeDamage from CharacterStats
        Assert.True(stats.curHealth == 100);
        stats.TakeDamage(51);
        yield return null;
        Assert.True(stats.curHealth == 49);
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
