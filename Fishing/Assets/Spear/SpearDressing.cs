using System.Collections.Generic;
using UnityEngine;

public class SpearDressing : MonoBehaviour
{
    [Header("Spear Dressing")]
    [SerializeField] private GameObject spearObj; // Reference to the spear.

    [SerializeField] private Material spearHead; // Reference to the spear head material.

    public void StartSpearDressing(Mesh mesh, List<Material> materials)
    {
        // Set the spear's mesh.
        spearObj.GetComponent<MeshFilter>().mesh = mesh;

        // Set the spear's materials.
        spearObj.GetComponent<MeshRenderer>().materials = materials.ToArray();
    }

    public void StartSpearSpecialDressing(Mesh mesh, Material material)
    {
        // Set the spear's mesh.
        spearObj.GetComponent<MeshFilter>().mesh = mesh;

        // Get MeshRenderer component
        MeshRenderer meshRenderer = spearObj.GetComponent<MeshRenderer>();
        
        // Ensure the materials array has at least two elements
        Material[] materials = new Material[2];
        materials[0] = material;
        materials[1] = spearHead; // You can set this to another material if needed
        
        meshRenderer.materials = materials;
    }
}