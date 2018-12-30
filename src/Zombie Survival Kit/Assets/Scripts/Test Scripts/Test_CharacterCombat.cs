using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test_CharacterCombat
{
    [UnityTest]
    public IEnumerator Test_Attack()
    {
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject character = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        GameObject character2 = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));
        CharacterCombat attacker = character.GetComponent<CharacterCombat>();
        CharacterStats attackerStats = character.GetComponent<CharacterStats>();
        CharacterStats defenderStats = character2.GetComponent<CharacterStats>();
        yield return null;

        int oldHealth = defenderStats.curHealth;
        attacker.Attack(defenderStats);
        yield return new WaitForSeconds(2);
        Assert.True(defenderStats.curHealth == defenderStats.maxHealth - attackerStats.dmg.GetValue());

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
