using System.Collections.Generic;
using UnityEngine;

// This script defines a ScriptableObject to store fish-related data.
[CreateAssetMenu(fileName = "FishData", menuName = "ScriptableObject/FishData")]
public class FishData : ScriptableObject
{
    public string fishName;  // Name of the fish species.

    [Space]
    
    public float damageUnit; // Damage the fish can deal to other objects/entities.
    public float maxHealth;  // Maximum health value of the fish.
    public FishSpeed fishSpeed;  // Speed category of the fish (Fast, Middle, or Slow).

    [Space]

    public int defaultScore; // Default score for the fish.
    public int defaultMoney; // Default money for the fish.

    [Space]

    public List<GameObject> fishPrefabs = new List<GameObject>();  // List of fish prefabs for instantiation.

    [Space]
    
    // The minimum and maximum time intervals for the fish's movement repetition.
    public float minRepeatTime = 1f;
    public float maxRepeatTime = 5f;

    [Space]

    public float journeyTime = 2f; // The duration of the movement between two points for the fish.

    [Space]

    // Material exchange will be provided according to damage intake.
    public Material damageMaterial;
    public Material originalMaterial;
}

// Enumeration for categorizing fish speeds.
public enum FishSpeed
{
    Fast,   // Fast-moving fish.
    Middle, // Fish with moderate speed.
    Slow    // Slow-moving fish.
}