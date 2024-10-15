using UnityEngine;

public class SettingsFounder : MonoBehaviour
{
    [Header("Settings Data")]
    [SerializeField] private SettingsData settingsData; // Holds a reference to the player's settings data, such as post-processing and renderer quality options.

    [Header("Post-Processing")]
    [SerializeField] private GameObject postProcessingObject; // Reference to the post-processing object to enable or disable based on settings.

    void Start()
    {
        // Initialize post-processing settings based on the player's preferences.
        PostProcessing(settingsData.isPostProcessing);

        // Apply the chosen renderer quality settings.
        ApplyRendererQuality(settingsData.rendererQualityOptions);
    }

    // Enables or disables post-processing effects depending on the provided boolean value.
    void PostProcessing(bool isPostProcessing)
    {
        postProcessingObject.SetActive(isPostProcessing); // Activates or deactivates the post-processing GameObject.
    }

    // Sets the renderer quality level based on the chosen option from the settings.
    void ApplyRendererQuality(RendererQualityOptions rendererQualityOptions)
    {
        QualitySettings.SetQualityLevel((int)rendererQualityOptions); // Adjusts the game's quality level using Unity's built-in QualitySettings.
    }
}