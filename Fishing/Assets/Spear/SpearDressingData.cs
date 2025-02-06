using System;
using System.Collections.Generic;
using UnityEngine;

// todo: SpearDerssingData ve SpearDress uygun bir zamanda kaldırılsın ve güncel olan OwnedCostumesData kullanılsın.
[CreateAssetMenu(fileName = "SpearDressingData", menuName = "ScriptableObject/SpearDressingData")]
public class SpearDressingData : ScriptableObject
{
    public List<SpearDress> spearDresses = new List<SpearDress>();
}

[Serializable]
public class SpearDress
{
    public string name;
    public Mesh mesh;
    public List<Material> materials = new List<Material>();
}