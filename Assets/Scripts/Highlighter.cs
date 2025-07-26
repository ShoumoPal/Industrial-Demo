using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Highlighter : MonoBehaviour
{
    [SerializeField]
    Material hightlightMaterial;

    private bool blink = false;
    private Renderer mesh;
    private Material originalMaterial;

    private void Start()
    {
        mesh = GetComponent<Renderer>();
        originalMaterial = mesh.material;
    }

    public void HighlightBlink(float interval)
    {
        StartCoroutine(HighlightBlinkCR(interval));
    }
    private IEnumerator HighlightBlinkCR(float interval)
    {
        blink = true;
        while (blink)
        {
            mesh.material = hightlightMaterial;
            yield return new WaitForSeconds(interval);
            mesh.material = originalMaterial;
            yield return new WaitForSeconds(interval);
        }
        
        mesh.material = originalMaterial; // Ensure the original material is set after blinking stops
    }

    public void StopHighlightBlink()
    {
        blink = false;
        mesh.material = originalMaterial; // Reset to original material when stopping
    }
}
