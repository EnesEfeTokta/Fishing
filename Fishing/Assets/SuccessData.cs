using UnityEngine;

[CreateAssetMenu(fileName = "SuccessData", menuName = "ScriptableObject/SuccessData")]
public class SuccessData : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite successImage;
}
