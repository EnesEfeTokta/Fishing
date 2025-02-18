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

    public GameObject selectSpearObject; //! Bu kullanıcının seçtiği mızrağı direk oluşturmada kullanılıyor. Onun yerine Mesh sistemi getir. Hali hazırdaki kodlarla.
    //public Material selectSpearMaterial; // The player's selected spear material.

    [Space]

    public OwnedCostumesData ownedCostumesData; // The player's own costumes information in -game inventory is stored.
    public SpearDress spearDress; // The player's selected spear dressing. 

    [Space]

    [Header("Player Datas")]
    public SettingsData settingsData; // The player's settings data is stored.
    public List<LevelInformationData> levelInformationDatas = new List<LevelInformationData>();
    public PastPlaysData pastPlaysData; // The player's past plays data is stored.
}