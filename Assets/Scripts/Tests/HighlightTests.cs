using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using UnityEditor;

public class HighlightTests
{
    private GameObject testObject;
    private HighlightObject highlighter;
    private Material originalMaterial;
    private Material highlightMaterial;
    private Renderer meshRenderer;

    [SetUp]
    public void Setup()
    {
        // Create in editor space (not play mode)
        testObject = new GameObject("HighlighterTestObject");

        // Add components needed for the test
        meshRenderer = testObject.AddComponent<MeshRenderer>();
        originalMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Default.mat"); // Use your material path
        highlightMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Highlight.mat");

        // Fallback if materials don't exist in project
        if (originalMaterial == null)
            originalMaterial = new Material(Shader.Find("Standard"));
        if (highlightMaterial == null)
            highlightMaterial = new Material(Shader.Find("Standard")) { color = Color.yellow };

        meshRenderer.material = originalMaterial;

        // Add your component
        highlighter = testObject.AddComponent<HighlightObject>();
        highlighter.highlightMaterial = highlightMaterial;
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up without needing play mode
        Object.DestroyImmediate(testObject);
    }

    [Test]
    public void Highlighter_ExistsOnGameObject()
    {
        Assert.IsNotNull(highlighter);
    }

    [Test]
    public void Materials_AreAssignedCorrectly()
    {
        Assert.AreEqual(originalMaterial, meshRenderer.material);
        Assert.AreEqual(highlightMaterial, highlighter.highlightMaterial);
    }

    [UnityTest]
    public IEnumerator Blink_CyclesMaterials()
    {
        highlighter.HighlightBlink(0.05f); // Short interval for faster tests

        yield return null; // First frame - should be highlight
        Assert.AreEqual(highlightMaterial, meshRenderer.material);

        yield return new WaitForSeconds(0.05f); // Wait interval
        Assert.AreEqual(originalMaterial, meshRenderer.material);

        yield return new WaitForSeconds(0.05f); // Next cycle
        Assert.AreEqual(highlightMaterial, meshRenderer.material);
    }

    //[Test]
    //public void StopBlink_ResetsMaterial()
    //{
    //    // Need to run coroutine manually in EditMode
    //    var blinkCoroutine = highlighter.HighlightBlink(0.1f);
    //    while (blinkCoroutine.MoveNext()) { } // Force complete one cycle

    //    highlighter.StopHighlightBlink();
    //    Assert.AreEqual(originalMaterial, meshRenderer.material);
    //}
}
