using System;
using System.Collections.Generic;
using UnityEngine;

// This script defines a ScriptableObject that holds all money loading data.
[CreateAssetMenu(fileName = "MoneyLoadingData", menuName = "ScriptableObject/MoneyLoadingData")]
public class MoneyLoadingData : ScriptableObject
{
    // List of all available money options.
    public List<MoneyOption> moneyOptions = new List<MoneyOption>();
}

[Serializable]
public class MoneyOption
{
    public Sprite moneyOptionImage; // The image representing the money option.
    public string moneyOptionName;  // The name of the money option.
    public int moneyOptionPrice;    // The price of the money option.

    // List of features associated with the money option.
    public List<MoneyOptionFeature> moneyOptionFeature = new List<MoneyOptionFeature>();
}

[Serializable]
public class MoneyOptionFeature
{
    public Sprite moneyOptionFeatureIcon; // Icon representing the feature.
    public string moneyOptionFeatureName; // Name of the feature.
}