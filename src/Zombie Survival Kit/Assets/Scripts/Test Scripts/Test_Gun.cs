using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test_Gun
{

    //[Test]
    //public void Test_GunSimplePasses() {
    //    // Use the Assert class to test conditions.
    //}

    [UnityTest]
    public IEnumerator Test_ShootandReload()
    {
        //var GunManager = new GameObject().AddComponent<Gun>(); //create a gameobject with thte gun script as a component
        //bullet object
        GameObject bullet = GameObject.Instantiate(Resources.Load<GameObject>("PrefabItems/bullet")); //load a bullet prefab from resources folder
        //player object
        GameObject camera = GameObject.Instantiate(Resources.Load<GameObject>("Prefabplayer/PrimaryPlayer"));
        //GameManager (contains Gun) object
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("Prefabplayer/GameManager"));
        //PlayerUI object
        GameObject PlayerUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/InventoryUI"));
        GameObject GunUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/GunUI"));
        GameObject AxeUI = GameObject.Instantiate(Resources.Load<GameObject>("PrefabUI/AxeUI"));
        EquipmentItem rangeWeapon = Resources.Load<EquipmentItem>("Items/RangeWeapon");
        yield return null;

        //GunManager.Construct(7, 7, bullet, camera, 1000f, 0.8f, 2f); //construct a gun with specified parameter
        GameManager.GetComponent<EquipmentManager>().Equip(rangeWeapon);
        yield return null;

        GameObject gun = GameObject.FindGameObjectWithTag("Gun");
        Assert.IsNotNull(gun);
        Assert.True(gun.GetComponent<Gun>().getAmmoInClip() == 7);

        gun.GetComponent<Gun>().shoot();
        yield return null;
        Assert.True(gun.GetComponent<Gun>().getAmmoInClip() == 6);
        gun.GetComponent<Gun>().reload();
        yield return null;
        Assert.True(gun.GetComponent<Gun>().getAmmoInClip() == 7);

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        Assert.True(bullets.Length == 2);

        yield return null;

    }

    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Object.Destroy(gameobject);
        }
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
