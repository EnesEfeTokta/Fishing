using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GiftCaseItemData", menuName = "ScriptableObject/GiftCaseItemData")]
public class GiftCaseItemData : ScriptableObject
{   
    [Header("Case Item")]
    public new string name;
    public Sprite sprite;
    public List<GiftItem> giftItems = new List<GiftItem>();
}

[Serializable]
public class GiftItem
{
    [Header("Gift Item")]
    public string name;
    public Sprite sprite;
    public GiftCaseRarenessType giftCaseRarenessType;
    public GiftCaseType giftCaseType;

    [Header("Gift Item Data")]
    public int moneyAmount;
    public PowerUpsData powerUpsData;
    public Material spearCoatingMaterial;
    public GameObject spearObject;
}

public enum GiftCaseRarenessType
{
    Basic,
    Rare,
    Epic,
    Legendary
}

public enum GiftCaseType
{
    Money,
    PowerUps,
    SpearCoating,
    SpearObject
}