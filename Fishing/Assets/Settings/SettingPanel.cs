using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [Header("Ambient Music")]
    [SerializeField] private Slider ambientMusicBar; // Slider for controlling the ambient music volume.
    [SerializeField] private TMP_Text ambientMusicPercentage; // Text displaying the percentage of ambient music volume.
    private float ambientMusicValue; // Internal value to store the ambient music volume.

    [Header("Ambient Sounds")]
    [SerializeField] private Slider ambientSoundsBar; // Slider for controlling the ambient sounds volume.
    [SerializeField] private TMP_Text ambientSoundsPercentage; // Text displaying the percentage of ambient sounds volume.
    private float ambientSoundsValue; // Internal value to store the ambient sounds volume.

    [Header("Post-Processing")]
    [SerializeField] private Image postProcessingStatusButton; // UI element that visually represents the post-processing status (on/off).
    [SerializeField] private Color activeColor; // Color to represent active post-processing status.
    [SerializeField] private Color passiveColor; // Color to represent inactive post-processing status.
    [SerializeField] private TMP_Text postProcessingStatus; // Text that displays whether post-processing is "On" or "Off".

    [Header("Languages")]
    [SerializeField] private TMP_Dropdown languagesOptions; // Dropdown menu for language selection.

    [Header("Quality")]
    [SerializeField] private TMP_Dropdown qualityOptions; // Dropdown menu for graphics quality selection.

    [Header("Settings Data")]
    [SerializeField] private SettingsData settingsData; // ScriptableObject that holds the user preferences and settings.

    [Header("Panel")]
    [SerializeField] private GameObject settingsPanel; // GameObject representing the settings panel.

    // Opens or closes the settings panel and applies the settings values when the panel is opened.
    public void PanelOpenClose(bool isOpen)
    {
        Debug.Log($"Panel status: {isOpen}"); // Logs the panel's open/close status.

        settingsPanel.SetActive(isOpen); // Show or hide the settings panel.

        if (isOpen)
        {
            // Fill sliders with values from settingsData and update the text display.
            SliderDataFilling(
                new Slider[] { ambientMusicBar, ambientSoundsBar }, 
                new TMP_Text[] { ambientMusicPercentage, ambientSoundsPercentage }, 
                new float[] { settingsData.musicValue, settingsData.soundValue }
            );
            
            SetDropdowns(); // Initialize the dropdown menus for language and quality.

            SetPostProcessing(); // Set the post-processing button's visual state.
        }
        else
        {
            // Save settings when closing the panel (logic for saving to be implemented).
        }
    }

    // Fills the sliders and percentage text based on provided values.
    void SliderDataFilling(Slider[] slider, TMP_Text[] texts, float[] value)
    {
        for (int i = 0; i < slider.Length; i++)
        {
            value[i] = Mathf.RoundToInt(value[i]); // Round the slider value to the nearest integer.
            slider[i].value = value[i] / 100; // Convert to a normalized value between 0 and 1.
            texts[i].text = $"%{value[i]}"; // Update the percentage text display.
        }
    }

    // Sets up and initializes the dropdown menus for language and quality settings.
    void SetDropdowns()
    {
        // Add listeners to handle dropdown value changes.
        languagesOptions.onValueChanged.AddListener(LanguagesOptionsValueChanged);
        qualityOptions.onValueChanged.AddListener(QualityOptionsValueChanged);

        languagesOptions.ClearOptions(); // Clear any existing options in the language dropdown.
        qualityOptions.ClearOptions(); // Clear any existing options in the quality dropdown.

        // Retrieve available options for languages and quality from enumerations.
        List<string> languagesOptionsList = Enum.GetNames(typeof(OptionLanguages)).ToList();
        List<string> qualityOptionsList = Enum.GetNames(typeof(RendererQualityOptions)).ToList();

        // Add these options to the respective dropdowns.
        languagesOptions.AddOptions(languagesOptionsList);
        qualityOptions.AddOptions(qualityOptionsList);

        // Set the dropdowns to reflect the current settings.
        languagesOptions.value = (int)settingsData.optionLanguages;
        qualityOptions.value = (int)settingsData.rendererQualityOptions;
    }

    // Updates the language setting when the language dropdown value is changed.
    void LanguagesOptionsValueChanged(int index)
    {
        settingsData.optionLanguages = (OptionLanguages)index; // Save the selected language to the settings data.
    }

    // Updates the quality setting when the quality dropdown value is changed.
    void QualityOptionsValueChanged(int index)
    {
        settingsData.rendererQualityOptions = (RendererQualityOptions)index; // Save the selected quality to the settings data.
    }

    // Updates the post-processing button to reflect whether post-processing is active or not.
    void SetPostProcessing()
    {
        if (settingsData.isPostProcessing)
        {
            postProcessingStatusButton.color = activeColor; // Set button color to active state.
            postProcessingStatus.text = "On"; // Set post-processing text to "On".
        }
        else
        {
            postProcessingStatusButton.color = passiveColor; // Set button color to passive state.
            postProcessingStatus.text = "Off"; // Set post-processing text to "Off".
        }
    }

    // Toggles post-processing on/off when the user interacts with the relevant button.
    public void ChangePostProcessing()
    {
        settingsData.isPostProcessing = !settingsData.isPostProcessing; // Toggle the post-processing status in the settings.
    }

    // Placeholder function for changing the game's language.
    public void ApplyTheNewLanguage()
    {
        Debug.Log("Game language is changing..."); // Log the language change (implementation pending).
        // Actual language change logic to be implemented here.
    }
}