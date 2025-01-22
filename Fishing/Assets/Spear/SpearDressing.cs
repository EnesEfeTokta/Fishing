using System.Collections.Generic;
using UnityEngine;

public class SpearDressing : MonoBehaviour
{
    [Header("Spear Dressing")]
    [SerializeField] private GameObject spearObj; // Reference to the spear.

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

        //Get MeshRenderer component
        MeshRenderer meshRenderer = spearObj.GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;
        materials[0] = material;
        meshRenderer.materials = materials;
    }
}