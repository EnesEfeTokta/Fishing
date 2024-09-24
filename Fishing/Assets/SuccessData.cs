using UnityEngine;

// Create a new menu option for creating SuccessData ScriptableObjects in the Unity editor.
[CreateAssetMenu(fileName = "SuccessData", menuName = "ScriptableObject/SuccessData")]
public class SuccessData : ScriptableObject
{
    // The name of the success (achievement or icon name).
    public new string name;

    // A brief description of the success.
    public string description;

    // A reference to the success image or icon (for UI display).
    public Sprite successImage;
}