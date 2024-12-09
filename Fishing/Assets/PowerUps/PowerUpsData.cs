using UnityEngine;

// Creates a ScriptableObject named "PowerUpsData" that can be created via the Unity editor menu.
// ScriptableObjects are data containers that live in your project assets and can be reused across scenes.
[CreateAssetMenu(fileName = "PowerUpsData", menuName = "ScriptableObject/PowerUpsData")]
public class PowerUpsData : ScriptableObject
{
    // The name of the power-up.
    public string powerUpName;

    // The sprite used to represent the power-up visually.
    public Sprite powerUpImage;
    
    // The type of power-up, selected from the PowerUpType enum.  Defaults to "Speed".
    public PowerUpType powerUpType = PowerUpType.Speed;

    // Spacer in the Inspector for better organization.
    [Space]

    // The duration of the power-up's effect in seconds (likely integer seconds).
    public int powerUpDuration;

    // The numerical value associated with the power-up (e.g., speed increase, damage boost).
    public int powerUpValue;

    // Another spacer in the Inspector.
    [Space]

    // The cost to purchase or acquire the power-up.
    public int powerUpPrice;
}

// Defines the different types of power-ups available in the game.
public enum PowerUpType
{
    Speed,            // Increases player or projectile speed.
    HeavyAttack,      // Increases damage dealt.
    AddTime,          // Adds time to a timer or level clock.
    UnlimitedThrowing // Allows unlimited throwing without a cooldown.
}