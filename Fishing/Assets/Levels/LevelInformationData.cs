using System;
using System.Collections.Generic;
using UnityEngine;

// This script defines a ScriptableObject to store information about each level.
[CreateAssetMenu(fileName = "LevelInformationData", menuName = "ScriptableObject/LevelInformationData")]
public class LevelInformationData : ScriptableObject
{
    public string levelName;  // Name of the level.
    public float levelStopTime;  // Time limit or stop time for the level.
    public List<FishTypeAndNumber> fishTypeAndNumbers = new List<FishTypeAndNumber>();  // List of fish types and their numbers for this level.
}

// A serializable class to store data about fish type and the number of fish to spawn.
[Serializable]
public class FishTypeAndNumber
{
    public FishData fishData;  // Reference to the fish data (e.g., health, speed, prefab).
    public int fishCustom;     // Number of fish to spawn for this type.
}