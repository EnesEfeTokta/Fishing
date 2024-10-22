using System.Collections.Generic;
using UnityEngine;

public class SettingsFounder : MonoBehaviour
{
    // Holds the player's settings data (e.g., post-processing, renderer quality, audio settings).
    private SettingsData settingsData;

    // Reference to the GameObject responsible for post-processing effects.
    [Header("Post-Processing")]
    [SerializeField] private GameObject postProcessingObject;

    // Reference to the main music audio source to control its volume.
    [Header("Music Audio Source")]
    [SerializeField] private AudioSource musicAudioSource;

    // List of audio sources for sound effects to control their individual volumes.
    [Header("Sound Audio Source")]
    [SerializeField] private List<AudioSource> soundAudioSources = new List<AudioSource>();

    // Reference to the GameManager to access and manage game-related data.
    private GameManager gameManager;

    // Start is called before the first frame update to initialize settings.
    void Start()
    {
        // Get the GameManager component attached to the same GameObject.
        gameManager = GetComponent<GameManager>();

        // Load the player's settings data from the GameManager.
        SetSettingsDataData();

        // Initialize post-processing settings based on player preferences.
        PostProcessing(settingsData.isPostProcessing);

        // Apply the renderer quality setting selected by the player.
        ApplyRendererQuality(settingsData.rendererQualityOptions);

        // Adjust the volume of the music audio source, if available.
        if (musicAudioSource != null)
        {
            MusicValueEditing(musicAudioSource, settingsData.musicValue / 100);
        }

        // Adjust the volume of all sound effect audio sources, if available.
        if (soundAudioSources != null)
        {
            SoundValueEditing(soundAudioSources, settingsData.soundValue / 100);
        }
    }

    /// <summary>
    /// Reads and assigns the settings data from the GameManager.
    /// </summary>
    void SetSettingsDataData()
    {
        settingsData = gameManager.ReadSettingsData(settingsData);
    }

    /// <summary>
    /// Enables or disables post-processing effects based on player settings.
    /// </summary>
    /// <param name="isPostProcessing">Whether post-processing should be enabled.</param>
    void PostProcessing(bool isPostProcessing)
    {
        postProcessingObject.SetActive(isPostProcessing);
    }

    /// <summary>
    /// Adjusts the volume of the music audio source.
    /// </summary>
    /// <param name="audioSource">The music audio source to modify.</param>
    /// <param name="volume">The new volume value (0.0 to 1.0).</param>
    void MusicValueEditing(AudioSource audioSource, float volume)
    {
        audioSource.volume = volume;
    }

    /// <summary>
    /// Adjusts the volume of all sound effect audio sources.
    /// </summary>
    /// <param name="audioSources">A list of audio sources to adjust.</param>
    /// <param name="volume">The new volume value (0.0 to 1.0).</param>
    void SoundValueEditing(List<AudioSource> audioSources, float volume)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
    }

    /// <summary>
    /// Sets the renderer quality based on the selected option.
    /// </summary>
    /// <param name="rendererQualityOptions">The renderer quality setting to apply.</param>
    void ApplyRendererQuality(RendererQualityOptions rendererQualityOptions)
    {
        // Adjust the game's quality level using Unity's built-in QualitySettings.
        QualitySettings.SetQualityLevel((int)rendererQualityOptions);
    }
}