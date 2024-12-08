using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpsData", menuName = "ScriptableObject/PowerUpsData")]
public class PowerUpsData : ScriptableObject
{
    public string powerUpName;
    public Sprite powerUpImage;
    public PowerUpType powerUpType = PowerUpType.Speed;

    [Space]

    public int powerUpDuration;
    public int powerUpValue;
    public int powerUpRegenerationTime = 5;

    [Space]

    public int powerUpPrice;
}

public enum PowerUpType
{
    Speed,
    HeavyAttack,
    AddTime,
    UnlimitedThrowing
}