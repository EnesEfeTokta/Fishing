using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MoneyLoadingData", menuName = "ScriptableObject/MoneyLoadingData")]
public class MoneyLoadingData : ScriptableObject
{
    public List<MoneyOption> moneyOptions = new List<MoneyOption>();
}

[Serializable]
public class MoneyOption
{
    public Sprite moneyOptionImage;
    public string moneyOptionName;
    public int moneyOptionPrice;
    public List<MoneyOptionFeature> moneyOptionFeature = new List<MoneyOptionFeature>();
}

[Serializable]
public class MoneyOptionFeature
{
    public Sprite moneyOptionFeatureIcon;
    public string moneyOptionFeatureName;
}