using System;
using System.Collections.Generic;
using UnityEngine;

// This script defines a ScriptableObject to store information about each level.
[CreateAssetMenu(fileName = "LevelInformationData", menuName = "ScriptableObject/LevelInformationData")]
public class LevelInformationData : ScriptableObject
{
    public string levelName;  // Name of the level.
    public float levelTime;  // Time limit or stop time for the level.
    public int LevelIndex;  // Index of the level.

    [Space]

    public List<FishTypeAndNumber> fishTypeAndNumbers = new List<FishTypeAndNumber>();  // List of fish types and their numbers for this level.

    [Space]

    public int totalFishCount;
    public int maxMoneyCount;
    public int maxScoreCount;

    [Space]

    public float minWidth = -5; // Minimum width for random point generation.
    public float maxWidth = 5; // Maximum width for random point generation.

    public float minHeight = -5; // Minimum height for random point generation.
    public float maxHeigth = 5; // Maximum height for random point generation.

    [Space]

    public bool IsLevelOver = false;
    public bool IsLevelFinished = false;
}

// A serializable class to store data about fish type and the number of fish to spawn.
[Serializable]
public class FishTypeAndNumber
{
    public FishData fishData;  // Reference to the fish data (e.g., health, speed, prefab).
    public int fishCustom;     // Number of fish to spawn for this type.
}