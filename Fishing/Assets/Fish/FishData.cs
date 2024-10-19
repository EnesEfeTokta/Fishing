using System.Collections.Generic;
using UnityEngine;

// This script defines a ScriptableObject to store fish-related data.
[CreateAssetMenu(fileName = "FishData", menuName = "ScriptableObject/FishData")]
public class FishData : ScriptableObject
{
    public string fishName;  // Name of the fish species.
    public float damageUnit; // Damage the fish can deal to other objects/entities.
    public float maxHealth;  // Maximum health value of the fish.
    public FishSpeed fishSpeed;  // Speed category of the fish (Fast, Middle, or Slow).
    public List<GameObject> fishPrefabs = new List<GameObject>();  // List of fish prefabs for instantiation.
}

// Enumeration for categorizing fish speeds.
public enum FishSpeed
{
    Fast,   // Fast-moving fish.
    Middle, // Fish with moderate speed.
    Slow    // Slow-moving fish.
}