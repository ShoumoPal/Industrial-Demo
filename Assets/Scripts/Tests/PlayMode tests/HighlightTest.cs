using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class HighlightTest
{
    private GameObject testObject;
    private HighlightObject highlighter;
    private Material originalMaterial;
    private Material highlightMaterial;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        testObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        highlighter = testObject.AddComponent<HighlightObject>();

        originalMaterial = testObject.GetComponent<MeshRenderer>().material;
        highlightMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/HighlightMaterial.mat");

        highlighter.highlightMaterial = highlightMaterial;

        yield return null; // Let Unity settle
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(testObject);
        yield return null;
    }

    [UnityTest]
    public IEnumerator HighlightBlink_ChangesMaterialToHighlight()
    {
        highlighter.HighlightBlink(1f);

        yield return new WaitForSeconds(0.15f); // Give enough time for switch

        Assert.AreEqual(highlightMaterial, testObject.GetComponent<MeshRenderer>().sharedMaterial);

        highlighter.StopHighlightBlink();
    }

    [UnityTest]
    public IEnumerator StopHighlightBlink_RevertsToOriginalMaterial()
    {
        highlighter.HighlightBlink(1f);

        yield return new WaitForSeconds(0.2f); // Let it blink once

        highlighter.StopHighlightBlink();
        yield return new WaitForSeconds(0.05f); // Let it settle

        Assert.AreEqual(originalMaterial, testObject.GetComponent<MeshRenderer>().sharedMaterial);
    }

}
