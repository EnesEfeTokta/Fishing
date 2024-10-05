using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProgressData", menuName = "ScriptableObject/PlayerProgressData")]
public class PlayerProgressData : ScriptableObject
{
    public int totalPlayerScore;
    public int totalPlayerMoney;
    public int totalPlayerFish;
}
