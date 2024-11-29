using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelGroupData", menuName = "ScriptableObject/LevelGroupData")]
public class LevelGroupData : ScriptableObject
{
    public string groupTitle;  // Title of the level group.
    public LevelType levelTypes;  // Types of the level.
    public List<LevelInformationData> levelInformationDatas = new List<LevelInformationData>();
}

[Serializable]
public class LevelType
{
    public LevelDifficultyType levelDifficultyTypes = LevelDifficultyType.Easy; // The level difficulty type.
    public Color color = Color.green; // The color of the level.
}

public enum LevelDifficultyType
{
    Easy,
    Middle,
    Difficult
}