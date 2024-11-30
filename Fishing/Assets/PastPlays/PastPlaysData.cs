using System;
using System.Collections.Generic;
using UnityEngine;

// Create a new menu option for creating PastPlaysData ScriptableObjects in the Unity editor.
[CreateAssetMenu(fileName = "PastPlaysData", menuName = "ScriptableObject/PastPlaysData")]
public class PastPlaysData : ScriptableObject
{
    // List to store data about past played levels.
    public List<PastPlayData> pastPlayDatas = new List<PastPlayData>();
}

[Serializable]
public class PastPlayData
{
    // Icon, name and level number to represent the level.
    public Sprite icon;
    public string name;
    public int levelIndex;

    [Space]

    // Achievements gained from playing the level.
    public float scoreValue;
    public float fishValue;
    public float moneyValue;

    [Space]

    // Maximum possible values for score, fish collected, and money earned in the level.
    // These represent the highest possible achievements for the given level.
    public float maxScoreValue;
    public float maxFishValue;
    public float maxMoneyValue;
}