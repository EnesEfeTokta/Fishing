using UnityEngine;

// This attribute allows the SettingsData class to be created as a ScriptableObject from the Unity Editor.
[CreateAssetMenu(fileName = "SettingsData", menuName = "ScriptableObject/SettingsData")]
public class SettingsData : ScriptableObject
{
    [Header("Selected Language")]
    public OptionLanguages optionLanguages; // Enum to select the language of the game. Adding new languages in the OptionLanguages enum will reflect here.

    [Header("Audio")]
    [Range(0, 100f)] public float soundValue = 50f; // Control for sound effects volume, can be adjusted in the range of 0 (mute) to 100 (full volume).
    [Range(0, 100f)] public float musicValue = 50f; // Control for music volume, similarly adjustable from 0 to 100.

    [Header("Renderer Settings")]
    public bool isPostProcessing = true; // Toggle to enable or disable post-processing effects.
    public RendererQualityOptions rendererQualityOptions = RendererQualityOptions.Medium; // Dropdown to select the quality of the renderer from predefined options.
}

// Enum to store available language options for the game settings.
public enum OptionLanguages
{
    English,
    Turkish,
    French,
    German,
    Spanish
}

// Enum to define renderer quality settings, can be used to adjust visual fidelity for performance optimization.
public enum RendererQualityOptions
{
    VeryLow, // Lowest quality for maximum performance boost.
    Low, // Low quality for improved performance on less capable hardware.
    Medium, // Medium quality for average performance.
    High, // High-quality setting with good balance of detail and performance.
    VeryHigh, // Very high-quality setting with high detail.
    Ultra // Highest quality setting with maximum detail.
}