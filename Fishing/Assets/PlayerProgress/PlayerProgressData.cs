using System;
using System.Collections.Generic;
using UnityEngine;

// This script defines a ScriptableObject that stores the player's progress data.
// ScriptableObjects are assets that allow data to persist independently of the game objects.
[CreateAssetMenu(fileName = "PlayerProgressData", menuName = "ScriptableObject/PlayerProgressData")]
public class PlayerProgressData : ScriptableObject
{
    // todo: Burada ki gereksiz kullanımlar temizlenmeli. Materyaller ve mızraklara odaklanılsın.

    // Variables to hold player's total score, money, and fish count
    public int totalPlayerScore; // Total score accumulated by the player
    public int totalPlayerMoney; // Total money collected by the player
    public int totalPlayerFish;  // Total fish caught by the player

    [Space]

    public List<PowerUpsData> powerUpsDatas = new List<PowerUpsData>(); // The player's in -game upgrade assets are listed.
    public List<GiftCaseItemData> giftCaseItemDatas = new List<GiftCaseItemData>();
    public List<Material> materialDatas = new List<Material>(); // The player's in -game material assets are listed.
    public List<GameObject> spearObjectDatas = new List<GameObject>(); // The player's in -game spear assets are listed.

    [Space]
    
    public GameObject selectSpearObject; // The player's selected spear asset.
    //public Material selectSpearMaterial; // The player's selected spear material.

    [Space]

    public List<SpearDress> possessedSpearDresses = new List<SpearDress>(); // The player's in -game spear dressing assets are listed.
    public SpearDress spearDress; // The player's selected spear dressing.

    //[Space]
    // todo: Buraya oyunda kullanılacak tüm ScriptableObject 'ler eklenebilir.
}