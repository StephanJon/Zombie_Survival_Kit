using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test_DaylightController
{

    //[Test]
    //public void Test_DaylightControllerSimplePasses() {
    //    // Use the Assert class to test conditions.
    //}

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    //[UnityTest]
    //public IEnumerator Test_DaylightControllerWithEnumeratorPasses() {
    //    // Use the Assert class to test conditions.
    //    // yield to skip a frame
    //    yield return null;
    //}


    [UnityTest]
    public IEnumerator Test_DayLight()
    {
        GameObject daylightobject = GameObject.Instantiate(Resources.Load<GameObject>("PrefabEnvironment/DirectionalLight"));
        yield return null;
        Quaternion initialTransform = daylightobject.transform.rotation;
        Debug.Log(initialTransform);


        yield return new WaitForSeconds(2);
        Quaternion finalTransform = daylightobject.transform.rotation;
        Debug.Log(finalTransform);

        Assert.False(initialTransform == finalTransform);
    }

    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("Daylight"))
        {
            Object.Destroy(gameobject);
        }
    }
}
