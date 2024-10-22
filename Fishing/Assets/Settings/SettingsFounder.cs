using System.Collections.Generic;
using UnityEngine;

public class SettingsFounder : MonoBehaviour
{
    [Header("Settings Data")]
    [SerializeField] private SettingsData settingsData; // Holds a reference to the player's settings data, such as post-processing and renderer quality options.

    [Header("Post-Processing")]
    [SerializeField] private GameObject postProcessingObject; // Reference to the post-processing object to enable or disable based on settings.

    [Header("Music Audio Source")]
    [SerializeField] private AudioSource musicAudioSource; // Reference to the main music audio source to adjust volume settings.

    [Header("Music Audio Source")]
    [SerializeField] private List<AudioSource> soundAudioSources = new List<AudioSource>(); // List of sound effect audio sources to adjust their volume levels.

    void Start()
    {
        // Initialize post-processing settings based on the player's preferences.
        PostProcessing(settingsData.isPostProcessing);

        // Apply the chosen renderer quality settings.
        ApplyRendererQuality(settingsData.rendererQualityOptions);

        // Adjust the music volume if the music audio source exists.
        if (musicAudioSource != null)
        {
            MusicValueEditing(musicAudioSource, settingsData.musicValue / 100);
        }

        // Adjust the sound effects volume if the sound audio sources list is not null.
        if (soundAudioSources != null)
        {
            SoundValueEditing(soundAudioSources, settingsData.soundValue / 100);
        }
    }

    // Enables or disables post-processing effects depending on the provided boolean value.
    void PostProcessing(bool isPostProcessing)
    {
        postProcessingObject.SetActive(isPostProcessing); // Activates or deactivates the post-processing GameObject.
    }


    // Adjusts the volume of the main music audio source.
    void MusicValueEditing(AudioSource audioSource, float volume)
    {
        audioSource.volume = volume;
    }

    // Adjusts the volume of all sound effect audio sources.
    void SoundValueEditing(List<AudioSource> audioSources, float volume)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
    }

    // Sets the renderer quality level based on the chosen option from the settings.
    void ApplyRendererQuality(RendererQualityOptions rendererQualityOptions)
    {
        QualitySettings.SetQualityLevel((int)rendererQualityOptions); // Adjusts the game's quality level using Unity's built-in QualitySettings.
    }
}