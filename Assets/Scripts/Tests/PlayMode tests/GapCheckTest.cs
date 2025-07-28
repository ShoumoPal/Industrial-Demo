using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GapCheckTest : MonoBehaviour
{
    private GameObject testTriggerObj;

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Destroy(testTriggerObj);
        yield return null;
    }

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        testTriggerObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        testTriggerObj.GetComponent<BoxCollider>().isTrigger = true;

        testTriggerObj.AddComponent<GapCheck>();
        yield return null;
    }

    [UnityTest]
    public IEnumerator Gapcheck_Close_Test()
    {
        GameObject targetObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // Set check distance threshold
        testTriggerObj.GetComponent<GapCheck>().SetThreshold(1f);

        // Set target object distance
        targetObj.transform.position = testTriggerObj.transform.position + new Vector3(0.5f, 0, 0);

        // Set target to check
        testTriggerObj.GetComponent<GapCheck>().SetTarget(targetObj.transform);

        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(testTriggerObj.GetComponent<GapCheck>().triggerd, "Trigger for distance not met");

        Destroy(targetObj);
    }

    [UnityTest]
    public IEnumerator Gapcheck_Far_Test()
    {
        GameObject targetObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // Set check distance threshold
        testTriggerObj.GetComponent<GapCheck>().SetThreshold(1f);

        // Set target object distance
        targetObj.transform.position = testTriggerObj.transform.position + new Vector3(10f, 0, 0);

        // Set target to check
        testTriggerObj.GetComponent<GapCheck>().SetTarget(targetObj.transform);

        yield return new WaitForSeconds(0.1f);
        Assert.IsFalse(testTriggerObj.GetComponent<GapCheck>().triggerd, "Trigger for distance met");

        Destroy(targetObj);
    }
}
