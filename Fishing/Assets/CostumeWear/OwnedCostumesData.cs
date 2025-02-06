using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OwnedCostumesData", menuName = "ScriptableObject/OwnedCostumesData")]
public class OwnedCostumesData : ScriptableObject
{
    public List<OwnedSpear> ownedSpears = new List<OwnedSpear>();
    public List<OwnedMaterial> ownedMaterials = new List<OwnedMaterial>();
}

[Serializable]
public class OwnedSpear
{
    public string spearName;
    public Sprite spearSprite;
    public Mesh spearObject;
}

[Serializable]
public class OwnedMaterial
{
    public string materialName;
    public Sprite materialSprite;
    public Material materialObject;
}