using System.Collections.Generic;
using UnityEngine;

// This script defines a ScriptableObject that stores the player's progress data.
// ScriptableObjects are assets that allow data to persist independently of the game objects.
[CreateAssetMenu(fileName = "PlayerProgressData", menuName = "ScriptableObject/PlayerProgressData")]
public class PlayerProgressData : ScriptableObject
{
    // Variables to hold player's total score, money, and fish count
    public int totalPlayerScore; // Total score accumulated by the player
    public int totalPlayerMoney; // Total money collected by the player
    public int totalPlayerFish;  // Total fish caught by the player
    public List<PowerUpsData> powerUpsDatas = new List<PowerUpsData>(); // The player's in -game upgrade assets are listed.
}