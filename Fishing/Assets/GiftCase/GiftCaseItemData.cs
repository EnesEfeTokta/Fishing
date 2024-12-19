using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GiftCaseItemData", menuName = "ScriptableObject/GiftCaseItemData")]
public class GiftCaseItemData : ScriptableObject
{   
    [SerializeField] private List<GiftItem> giftItems = new List<GiftItem>();

    public IReadOnlyList<GiftItem> GiftItems => giftItems;
}

[Serializable]
public class GiftItem
{
    [Header("Gift Item")]
    public string name;
    public Sprite sprite;
    public GiftCaseRarenessType giftCaseRarenessType;
    public GiftCaseType giftCaseType;
    public int giftCount = 1;

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